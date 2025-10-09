namespace NorthWind.Domain.Entities;

public class Customer
{
    protected Customer()
    {
    }

    public Customer(string customerId, string companyName)
    {
        CustomerId = customerId;
        CompanyName = companyName;
    }

    public string CustomerId { get; private set; } = null!;

    public string CompanyName { get; private set; } = null!;

    public string? ContactName { get; private set; }

    public string? ContactTitle { get; private set; }

    public string? Address { get; private set; }

    public string? City { get; private set; }

    public string? Region { get; private set; }

    public string? PostalCode { get; private set; }

    public string? Country { get; private set; }

    public string? Phone { get; private set; }

    public string? Fax { get; private set; }

    public ICollection<Order> Orders { get; } = new List<Order>();

    public ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; } = new List<CustomerCustomerDemo>();
}
