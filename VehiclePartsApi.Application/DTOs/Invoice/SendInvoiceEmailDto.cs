namespace VehiclePartsApi.Application.DTOs.Invoice;

public class SendInvoiceEmailRequestDto
{
    public string ToEmail { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string InvoiceNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}