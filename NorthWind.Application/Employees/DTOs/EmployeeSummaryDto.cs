namespace NorthWind.Application.Employees.DTOs;

public sealed record EmployeeSummaryDto(
    int EmployeeId,
    string FirstName,
    string LastName,
    string? Title,
    string? TitleOfCourtesy,
    DateTime? BirthDate,
    DateTime? HireDate,
    string? City,
    string? Country,
    string? HomePhone,
    int? ReportsTo);
