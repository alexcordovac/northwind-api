namespace NorthWind.Domain.Entities;

public class OrderDetail
{
    protected OrderDetail()
    {
    }

    public OrderDetail(int orderId, int productId, decimal unitPrice, short quantity, float discount)
    {
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
    }

    public int OrderId { get; private set; }

    public int ProductId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public short Quantity { get; private set; }

    public float Discount { get; private set; }

    public Order? Order { get; private set; }

    public Product? Product { get; private set; }
}
