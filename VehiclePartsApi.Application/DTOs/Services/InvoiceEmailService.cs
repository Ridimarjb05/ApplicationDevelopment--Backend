using VehiclePartsApi.Application.DTOs.Invoice;
using VehiclePartsApi.Application.Interfaces;
using VehiclePartsApi.Domain.Interfaces;

namespace VehiclePartsApi.Application.Services;

public class InvoiceEmailService : IInvoiceEmailService
{
    private readonly IEmailService _email;

    public InvoiceEmailService(IEmailService email)
    {
        _email = email;
    }

    public async Task SendInvoiceEmailAsync(SendInvoiceEmailRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.ToEmail))
            throw new Exception("ToEmail is required");

        var subject = $"Invoice {dto.InvoiceNumber}";
        var html = $@"
            <h2>Invoice: {dto.InvoiceNumber}</h2>
            <p>Dear {dto.CustomerName},</p>
            <p>Amount: <b>{dto.Amount}</b></p>
            <p>Thank you for your purchase.</p>
        ";

        await _email.SendAsync(dto.ToEmail.Trim(), subject, html);
    }
}