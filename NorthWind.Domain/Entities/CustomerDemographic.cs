namespace NorthWind.Domain.Entities;

public class CustomerDemographic
{
    protected CustomerDemographic()
    {
    }

    public CustomerDemographic(string customerTypeId)
    {
        CustomerTypeId = customerTypeId;
    }

    public string CustomerTypeId { get; private set; } = null!;

    public string? CustomerDesc { get; private set; }

    public ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; } = new List<CustomerCustomerDemo>();
}
