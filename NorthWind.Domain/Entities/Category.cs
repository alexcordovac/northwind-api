namespace NorthWind.Domain.Entities;

public class Category
{
    protected Category()
    {
    }

    public Category(string categoryName)
    {
        CategoryName = categoryName;
    }

    public int CategoryId { get; private set; }

    public string CategoryName { get; private set; } = null!;

    public string? Description { get; private set; }

    public byte[]? Picture { get; private set; }

    public ICollection<Product> Products { get; } = new List<Product>();
}
