namespace NorthWind.Domain.Entities;

public class Shipper
{
    protected Shipper()
    {
    }

    public Shipper(string companyName)
    {
        CompanyName = companyName;
    }

    public int ShipperId { get; private set; }

    public string CompanyName { get; private set; } = null!;

    public string? Phone { get; private set; }

    public ICollection<Order> Orders { get; } = new List<Order>();
}
