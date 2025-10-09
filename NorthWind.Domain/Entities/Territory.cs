namespace NorthWind.Domain.Entities;

public class Territory
{
    protected Territory()
    {
    }

    public Territory(string territoryId, string territoryDescription, int regionId)
    {
        TerritoryId = territoryId;
        TerritoryDescription = territoryDescription;
        RegionId = regionId;
    }

    public string TerritoryId { get; private set; } = null!;

    public string TerritoryDescription { get; private set; } = null!;

    public int RegionId { get; private set; }

    public Region? Region { get; private set; }

    public ICollection<EmployeeTerritory> EmployeeTerritories { get; } = new List<EmployeeTerritory>();
}
