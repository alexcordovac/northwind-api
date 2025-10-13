namespace NorthWind.Application.Customers.DTOs;

public sealed record CustomerSummaryDto(
    string CustomerId,
    string CompanyName,
    string? ContactName,
    string? ContactTitle,
    string? City,
    string? Country,
    string? Phone,
    string? Fax);
