namespace NorthWind.Domain.Entities;

public partial class Order
{
    protected Order()
    {
    }

    public int OrderId { get; private set; }

    public string? CustomerId { get; private set; }

    public int? EmployeeId { get; private set; }

    public DateTime? OrderDate { get; private set; }

    public DateTime? RequiredDate { get; private set; }

    public DateTime? ShippedDate { get; private set; }

    public int? ShipVia { get; private set; }

    public decimal? Freight { get; private set; }

    public string? ShipName { get; private set; }

    public string? ShipAddress { get; private set; }

    public string? ShipCity { get; private set; }

    public string? ShipRegion { get; private set; }

    public string? ShipPostalCode { get; private set; }

    public string? ShipCountry { get; private set; }

    public Customer? Customer { get; private set; }

    public Employee? Employee { get; private set; }

    public Shipper? Shipper { get; private set; }

    public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
