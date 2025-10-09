namespace NorthWind.Application.Orders;

public sealed record GetOrdersInput(
    int Page,
    int Rows,
    int Offset,
    string? Query)
{
    public string? NormalizedQuery => string.IsNullOrWhiteSpace(Query) ? null : Query.Trim();
}
