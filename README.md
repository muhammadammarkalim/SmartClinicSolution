# SmartClinic ??

**SmartClinic** is a modern, AI-powered healthcare platform built with .NET 9 and Blazor. It connects patients with doctors, provides intelligent symptom checking, manages appointments, and enables seamless healthcare delivery through a user-friendly web application.

## ?? Features

### Core Functionality
- **Smart Symptom Checker** - AI-powered symptom analysis to help patients find the right specialist
- **Doctor Directory** - Browse and search doctors by specialty
- **Appointment Booking** - Easy online appointment scheduling system
- **Doctor Profiles** - Comprehensive doctor information and specialty details
- **Prescription Management** - Digital prescription handling and medicine tracking
- **Patient Reviews** - Rate and review doctor experiences
- **Doctor Schedules** - Flexible scheduling system for availability management

### User Roles & Authentication
- **Patient Role** - Book appointments, check symptoms, view history
- **Doctor Role** - Manage appointments, write prescriptions, handle schedules
- **Admin Role** - Manage users, doctors, and system settings
- **Role-Based Access Control** - Secure, permission-based navigation

### Platform Features
- **Responsive Design** - Works seamlessly on desktop and mobile
- **Real-time Notifications** - Stay updated with appointment status
- **Secure Authentication** - Identity-based authorization with ASP.NET Core Identity
- **Video Content Support** - Hero videos and multimedia content
- **Database-Backed** - SQL Server with Entity Framework Core

## ??? Tech Stack

- **Framework**: .NET 9
- **Frontend**: Blazor (Interactive Server Components)
- **Database**: SQL Server with Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity with custom AppUser
- **Language**: C# 12+
- **JSON Support**: Newtonsoft.Json 13.0.4

## ?? Project Structure

```
SmartClinic/
??? Components/              # Blazor components and pages
?   ??? Pages/              # Application pages (Home, etc.)
?   ??? Layout/             # Layout components
?   ??? Account/            # Authentication-related components
??? Models/                 # Domain models
?   ??? AppUser.cs         # Custom user model
?   ??? Appointment.cs     # Appointment entity
?   ??? Doctor.cs          # Doctor entity
?   ??? Specialty.cs       # Specialty entity
?   ??? Prescription.cs    # Prescription entity
?   ??? PrescriptionMedicine.cs  # Medicine details
?   ??? Review.cs          # Patient reviews
?   ??? SymptomResult.cs   # Symptom checker results
??? Services/               # Business logic services
?   ??? AppointmentService.cs
?   ??? DoctorService.cs
?   ??? Placeholder.cs
??? Data/                   # Database context and migrations
?   ??? ApplicationDbContext.cs
?   ??? Migrations/         # EF Core migrations
??? wwwroot/                # Static assets
?   ??? css/               # Stylesheets
?   ??? js/                # JavaScript files
?   ??? videos/            # Video content
??? Program.cs             # Application startup configuration
??? appsettings.json       # Configuration settings
??? SmartClinic.csproj     # Project file
```

## ?? Getting Started

### Prerequisites
- .NET 9 SDK or higher
- SQL Server (local or remote)
- Visual Studio 2022+ (Community, Professional, or Enterprise)
- Git

### Installation

1. **Clone the Repository**
   ```bash
   git clone https://github.com/muhammadammarkalim/SmartClinicSolution.git
   cd SmartClinicSolution
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure Database Connection**
   - Update `appsettings.json` with your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=SmartClinicDB;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

4. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the Application**
   ```bash
   dotnet run
   ```

   The application will be available at `https://localhost:7000` (or the configured port).

## ?? NuGet Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 9.0.0 | Identity management |
| Microsoft.EntityFrameworkCore.SqlServer | 9.0.0 | Database provider |
| Microsoft.AspNetCore.Identity.UI | 9.0.0 | Pre-built identity pages |
| Newtonsoft.Json | 13.0.4 | JSON serialization |
| System.Net.Http.Json | 10.0.8 | HTTP JSON handling |

## ?? Authentication & Authorization

### User Registration & Login
- Email-based authentication
- Email confirmation disabled for demo/testing purposes
- Password requirements: minimum 6 characters, digits/uppercase/special chars optional

### Role Management
SmartClinic supports three main roles:
- **Patient** - Browse doctors, book appointments, check symptoms
- **Doctor** - Manage appointments, create prescriptions
- **Admin** - System administration and user management

## ?? Key Pages

- **Home (`/`)** - Landing page with hero section, symptom checker, and doctor listings
- **Login/Register** - User authentication pages
- **Doctor Profiles** - Detailed doctor information and reviews
- **Appointment Booking** - Schedule appointments with doctors
- **My Appointments** - View and manage patient appointments
- **Prescriptions** - Access and track prescriptions
- **Admin Dashboard** - Manage users, doctors, and system settings
- **Doctor Dashboard** - Manage appointments and schedules

## ??? Database Schema

### Core Entities
- **AppUser** - Extended user with patient/doctor specific fields
- **Doctor** - Doctor information and specialties
- **Appointment** - Appointment records with status tracking
- **Prescription** - Medical prescriptions
- **Review** - Patient reviews for doctors
- **Specialty** - Medical specialties (Cardiology, Neurology, etc.)
- **DoctorSchedule** - Doctor availability and shifts
- **DoctorUnavailability** - Doctor blackout dates/times

## ?? Development

### Building the Project
```bash
dotnet build
```

### Running Tests (if applicable)
```bash
dotnet test
```

### Database Migrations
Create a new migration:
```bash
dotnet ef migrations add MigrationName
```

Apply migrations:
```bash
dotnet ef database update
```

## ?? Configuration

### appsettings.json
Key configuration sections:
- `ConnectionStrings` - Database connection
- `Logging` - Log levels and providers
- Application-specific settings

### User Secrets (Development)
Sensitive data (database passwords, API keys) should be stored using User Secrets:
```bash
dotnet user-secrets init
dotnet user-secrets set "key" "value"
```

## ?? Contributing

1. Create a feature branch (`git checkout -b feature/YourFeature`)
2. Commit your changes (`git commit -m 'Add YourFeature'`)
3. Push to the branch (`git push origin feature/YourFeature`)
4. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the LICENSE file for details.

## ?? Team

**SmartClinic** is developed as part of an academic project for semester-based curriculum.

## ?? Known Issues & Roadmap

### Current Phase
- Phase 2: Enhanced features and stability improvements
- Real-time appointment notifications
- Advanced symptom checker with ML integration

### Future Enhancements
- Mobile app (iOS/Android)
- Telemedicine capabilities (video consultations)
- Advanced analytics dashboard
- Payment integration
- Multi-language support

## ?? Support & Contact

For questions or issues:
- Open an issue on GitHub
- Contact the development team through the repository

## ?? Acknowledgments

- .NET 9 documentation
- Blazor community resources
- SQL Server and Entity Framework documentation
- Healthcare domain advisors

---

**Last Updated**: December 2024  
**Version**: Phase 2  
**Status**: Active Development
