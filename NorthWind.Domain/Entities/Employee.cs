namespace NorthWind.Domain.Entities;

public class Employee
{
    protected Employee()
    {
    }

    public Employee(string lastName, string firstName)
    {
        LastName = lastName;
        FirstName = firstName;
    }

    public int EmployeeId { get; private set; }

    public string LastName { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string? Title { get; private set; }

    public string? TitleOfCourtesy { get; private set; }

    public DateTime? BirthDate { get; private set; }

    public DateTime? HireDate { get; private set; }

    public string? Address { get; private set; }

    public string? City { get; private set; }

    public string? Region { get; private set; }

    public string? PostalCode { get; private set; }

    public string? Country { get; private set; }

    public string? HomePhone { get; private set; }

    public string? Extension { get; private set; }

    public byte[]? Photo { get; private set; }

    public string? Notes { get; private set; }

    public int? ReportsTo { get; private set; }

    public string? PhotoPath { get; private set; }

    public Employee? Manager { get; private set; }

    public ICollection<Employee> DirectReports { get; } = new List<Employee>();

    public ICollection<Order> Orders { get; } = new List<Order>();

    public ICollection<EmployeeTerritory> EmployeeTerritories { get; } = new List<EmployeeTerritory>();
}
