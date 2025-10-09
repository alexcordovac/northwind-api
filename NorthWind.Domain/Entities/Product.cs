namespace NorthWind.Domain.Entities;

public class Product
{
    protected Product()
    {
    }

    public Product(string productName)
    {
        ProductName = productName;
    }

    public int ProductId { get; private set; }

    public string ProductName { get; private set; } = null!;

    public int? SupplierId { get; private set; }

    public int? CategoryId { get; private set; }

    public string? QuantityPerUnit { get; private set; }

    public decimal? UnitPrice { get; private set; }

    public short? UnitsInStock { get; private set; }

    public short? UnitsOnOrder { get; private set; }

    public short? ReorderLevel { get; private set; }

    public bool Discontinued { get; private set; }

    public Supplier? Supplier { get; private set; }

    public Category? Category { get; private set; }

    public ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
