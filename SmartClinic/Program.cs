using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Components;
using SmartClinic.Components.Account;
using SmartClinic.Data;
using SmartClinic.Models; // Added to find our custom AppUser class
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartClinic.Services; // Added for our business services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
// Updated to use AppUser instead of ApplicationUser
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// UPDATED: Changed AppUser configuration, turned off email confirmation, and added AddRoles<IdentityRole>()
builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to false for easy university demo testing
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole>() // CRITICAL: This enables Patient, Doctor, and Admin role checking
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// 2. Register Scoped Services Interfaces/Implementations (Phase 4 operational logic)
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();


// Updated to use AppUser
builder.Services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();

// 1. Register HttpClient as a singleton so we can call the Gemini AI API safely
builder.Services.AddHttpClient();

// 2. Register Scoped Services Interfaces/Implementations (Phase 4 operational logic)
builder.Services.AddScoped<IDoctorService, DoctorService>();
/*
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();
*/

var app = builder.Build();

// ============================================================================
// AUTOMATIC DATABASE SEEDER (FORCE INJECTION MODE)
// Forces data sync directly into empty tables to clear out UI errors
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 1. Force Roles Setup
    string[] roleNames = { "Admin", "Doctor", "Patient" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // 2. Clear old broken data profiles to prevent key errors if doctors table is empty
    if (!dbContext.Doctors.Any())
    {
        // Wipe old unmapped specialties completely to guarantee a fresh link
        dbContext.Specialties.RemoveRange(dbContext.Specialties);
        await dbContext.SaveChangesAsync();

        // 3. Force Specialty Re-insertion
        var mockSpecialties = new List<Specialty>
        {
            new() { Name = "Cardiology", Icon = "🫀" },
            new() { Name = "Neurology", Icon = "🧠" },
            new() { Name = "Dentistry", Icon = "🦷" },
            new() { Name = "Eye Care", Icon = "👁️" },
            new() { Name = "Orthopedics", Icon = "🦴" },
            new() { Name = "Pediatrics", Icon = "👶" }
        };
        await dbContext.Specialties.AddRangeAsync(mockSpecialties);
        await dbContext.SaveChangesAsync();

        // 4. Force Doctors Insertion
        var doctorList = new List<(string Name, string Email, string Specialty, string Qualification, double Rating, decimal Fee)>
        {
            ("Dr. Sarah Ahmed", "sarah.ahmed@medibook.com", "Cardiology", "MBBS, FCPS", 4.9, 2000),
            ("Dr. Usman Khan", "usman.khan@medibook.com", "Neurology", "MBBS, MD", 4.8, 3000),
            ("Dr. Ayesha Raza", "ayesha.raza@medibook.com", "Dentistry", "BDS, FCPS", 4.9, 1500),
            ("Dr. Bilal Hassan", "bilal.hassan@medibook.com", "Orthopedics", "MBBS, MS", 4.7, 2500),
            ("Dr. Nida Tariq", "nida.tariq@medibook.com", "Pediatrics", "MBBS, DCH", 4.8, 1800),
            ("Dr. Kamran Malik", "kamran.malik@medibook.com", "ENT Specialist", "MBBS, FCPS", 4.6, 2200)
        };

        foreach (var doc in doctorList)
        {
            // Verify if user account exists to avoid identity clashes
            var existingUser = await userManager.FindByEmailAsync(doc.Email);
            if (existingUser != null)
            {
                await userManager.DeleteAsync(existingUser);
            }

            var docUser = new AppUser
            {
                UserName = doc.Email,
                Email = doc.Email,
                Name = doc.Name,
                Role = "Doctor",
                CreatedAt = DateTime.Now,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(docUser, "Doctor123!");
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(docUser, "Doctor");

                var doctorProfile = new Doctor
                {
                    UserID = docUser.Id,
                    Specialty = doc.Specialty,
                    Qualification = doc.Qualification,
                    Rating = doc.Rating,
                    ConsultationFee = doc.Fee,
                    Bio = "Senior medical consultant specialized in complex physiological operations and clinical oversight workflows.",
                    IsAvailable = true
                };

                await dbContext.Doctors.AddAsync(doctorProfile);
            }
        }
        await dbContext.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// CRITICAL: Added standard authentication & authorization middleware in correct processing sequence
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// ============================================================================
// REGISTRATION SECURITY BRIDGE ENDPOINT FOR BLAZOR CORE
// Signs in newly registered users outside the SignalR circuit to prevent UI freezing!
// ============================================================================
app.MapGet("/api/auth/register-login", async (
    string userId,
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager) =>
{
    var user = await userManager.FindByIdAsync(userId);
    if (user != null)
    {
        // Issue cookies on a clean HTTP channel context
        await signInManager.SignInAsync(user, isPersistent: false);
    }

    // Redirect hard-coded browser target to dashboard view
    return Results.Redirect("/dashboard");
});

// ============================================================================
// LOGIN SECURITY BRIDGE ENDPOINT FOR BLAZOR CORE
// This bypasses Blazor interactive SignalR constraints to drop secure cookies!
// ============================================================================
app.MapGet("/api/auth/login", async (
    string email,
    string password,
    bool rememberMe,
    string? returnUrl,
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager) =>
{
    var user = await userManager.FindByEmailAsync(email);
    if (user != null)
    {
        var result = await signInManager.PasswordSignInAsync(user.UserName!, password, rememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Results.Redirect(returnUrl);

            return user.Role switch
            {
                "Admin" => Results.Redirect("/admin/dashboard"),
                "Doctor" => Results.Redirect("/doctor/dashboard"),
                _ => Results.Redirect("/dashboard")
            };
        }
    }
    return Results.Redirect("/login?error=InvalidAttempt");
});

app.Run();