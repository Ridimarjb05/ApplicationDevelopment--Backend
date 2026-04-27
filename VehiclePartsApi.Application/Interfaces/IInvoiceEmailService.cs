using VehiclePartsApi.Application.DTOs.Invoice;

namespace VehiclePartsApi.Application.Interfaces;

public interface IInvoiceEmailService
{
    Task SendInvoiceEmailAsync(SendInvoiceEmailRequestDto dto);
}