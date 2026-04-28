using Microsoft.AspNetCore.Mvc;
using VehicleParts.Application.DTOs;
using VehicleParts.Application.Interface.IServices;

namespace VehicleParts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class F13Controller : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IPartRequestService _partRequestService;
    private readonly IServiceReviewService _serviceReviewService;

    public F13Controller(
        IAppointmentService appointmentService,
        IPartRequestService partRequestService,
        IServiceReviewService serviceReviewService)
    {
        _appointmentService = appointmentService;
        _partRequestService = partRequestService;
        _serviceReviewService = serviceReviewService;
    }

    // Book Appointment
    [HttpPost("appointments")]
    public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDto dto)
    {
        var result = await _appointmentService.BookAppointmentAsync(dto);
        return Ok(result);
    }

    // Get Appointments by Customer
    [HttpGet("appointments/{customerId}")]
    public async Task<IActionResult> GetAppointments(int customerId)
    {
        var result = await _appointmentService.GetAppointmentsByCustomerIdAsync(customerId);
        return Ok(result);
    }

    // Request Unavailable Part
    [HttpPost("part-requests")]
    public async Task<IActionResult> RequestPart([FromBody] CreatePartRequestDto dto)
    {
        var result = await _partRequestService.CreatePartRequestAsync(dto);
        return Ok(result);
    }

    // Get Part Requests by Customer
    [HttpGet("part-requests/{customerId}")]
    public async Task<IActionResult> GetPartRequests(int customerId)
    {
        var result = await _partRequestService.GetPartRequestsByCustomerIdAsync(customerId);
        return Ok(result);
    }

    // Submit Service Review
    [HttpPost("reviews")]
    public async Task<IActionResult> SubmitReview([FromBody] CreateServiceReviewDto dto)
    {
        var result = await _serviceReviewService.SubmitReviewAsync(dto);
        return Ok(result);
    }

    // Get Reviews by Customer
    [HttpGet("reviews/{customerId}")]
    public async Task<IActionResult> GetReviews(int customerId)
    {
        var result = await _serviceReviewService.GetReviewsByCustomerIdAsync(customerId);
        return Ok(result);
    }
}