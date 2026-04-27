using Microsoft.AspNetCore.Mvc;
using VehiclePartsApi.Application.DTOs.Invoice;
using VehiclePartsApi.Application.Interfaces;

namespace VehiclePartsApi.Api.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceEmailService _invoiceEmailService;

    public InvoicesController(IInvoiceEmailService invoiceEmailService) => _invoiceEmailService = invoiceEmailService;

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail(SendInvoiceEmailRequestDto dto)
    {
        await _invoiceEmailService.SendInvoiceEmailAsync(dto);
        return Ok(new { message = "Invoice email sent" });
    }
}