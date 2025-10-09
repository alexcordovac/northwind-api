namespace NorthWind.Domain.Entities;

public class CustomerCustomerDemo
{
    protected CustomerCustomerDemo()
    {
    }

    public CustomerCustomerDemo(string customerId, string customerTypeId)
    {
        CustomerId = customerId;
        CustomerTypeId = customerTypeId;
    }

    public string CustomerId { get; private set; } = null!;

    public string CustomerTypeId { get; private set; } = null!;

    public Customer? Customer { get; private set; }

    public CustomerDemographic? CustomerDemographic { get; private set; }
}
