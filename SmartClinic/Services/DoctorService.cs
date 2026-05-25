using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;

namespace SmartClinic.Services
{
    public interface IDoctorService
    {
        Task<List<Doctor>> GetDoctorsAsync(string? specialty = null, string? searchQuery = null);
        Task<List<Specialty>> GetSpecialtiesAsync();
    }

    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context) { 
             _context = context;
        }

        public async Task<List<Doctor>> GetDoctorsAsync(string? specialty = null, string? searchQuery = null)
        {
            // Include the related AppUser data to access the doctor's real Name
            var query = _context.Doctors.Include(d => d.User).AsQueryable();

            // 1. Filter by Specialty if selected
            if (!string.IsNullOrEmpty(specialty) && specialty != "All Specialties")
            {
                query = query.Where(d => d.Specialty == specialty);
            }

            // 2. Filter by Search Query (checks name, specialty, or qualifications)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchLower = searchQuery.ToLower();
                query = query.Where(d =>
                    (d.User != null && d.User.Name.ToLower().Contains(searchLower)) ||
                    d.Specialty.ToLower().Contains(searchLower) ||
                    d.Qualification.ToLower().Contains(searchLower));
            }

            return await query.ToListAsync();
        }

        public async Task<List<Specialty>> GetSpecialtiesAsync()
        {
            return await _context.Specialties.ToListAsync();
        }
    }
}