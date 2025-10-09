namespace NorthWind.Domain.Entities;

public class Region
{
    protected Region()
    {
    }

    public Region(int regionId, string regionDescription)
    {
        RegionId = regionId;
        RegionDescription = regionDescription;
    }

    public int RegionId { get; private set; }

    public string RegionDescription { get; private set; } = null!;

    public ICollection<Territory> Territories { get; } = new List<Territory>();
}
