using VehicleParts.Application.DTOs;
using VehicleParts.Application.Interface.IRepository;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Appointment> BookAppointmentAsync(CreateAppointmentDto dto)
    {
        var appointment = new Appointment
        {
            CustomerID = dto.CustomerId,
            VehicleID = dto.VehicleId,
            StaffID = dto.StaffId,
            AppointmentDate = dto.AppointmentDate,
            TimeSlot = dto.TimeSlot,
            ServiceDescription = dto.ServiceDescription,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        return await _appointmentRepository.CreateAppointmentAsync(appointment);
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId)
    {
        return await _appointmentRepository.GetAppointmentsByCustomerIdAsync(customerId);
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(int id)
    {
        return await _appointmentRepository.GetAppointmentByIdAsync(id);
    }
}