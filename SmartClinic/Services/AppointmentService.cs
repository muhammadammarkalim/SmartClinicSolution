using Microsoft.EntityFrameworkCore;
using SmartClinic.Data;
using SmartClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartClinic.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetUpcomingAppointmentsAsync(string patientUserId);
        Task<Dictionary<string, int>> GetPatientDashboardMetricsAsync(string patientUserId);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetUpcomingAppointmentsAsync(string patientUserId)
        {
            // FIXED: Using AppointmentDateTime to sort and filter your database query
            return await _context.Appointments
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.User)
                .Where(a => a.PatientID == patientUserId && a.AppointmentDateTime >= DateTime.Today)
                .OrderBy(a => a.AppointmentDateTime)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetPatientDashboardMetricsAsync(string patientUserId)
        {
            var today = DateTime.Today;

            // 1. Calculate Upcoming Appointments
            int upcomingCount = await _context.Appointments
                .CountAsync(a => a.PatientID == patientUserId && a.AppointmentDateTime >= today);

            // 2. Calculate Completed Visits
            int completedCount = await _context.Appointments
                .CountAsync(a => a.PatientID == patientUserId && a.AppointmentDateTime < today);

            // 3. Calculate Active Prescriptions
            int prescriptionCount = await _context.Prescriptions
                .CountAsync(p => p.Appointment != null && p.Appointment.PatientID == patientUserId);

            return new Dictionary<string, int>
            {
                { "Upcoming", upcomingCount },
                { "Completed", completedCount },
                { "Prescriptions", prescriptionCount }
            };
        }
    }
}