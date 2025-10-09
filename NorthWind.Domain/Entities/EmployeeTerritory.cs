namespace NorthWind.Domain.Entities;

public class EmployeeTerritory
{
    protected EmployeeTerritory()
    {
    }

    public EmployeeTerritory(int employeeId, string territoryId)
    {
        EmployeeId = employeeId;
        TerritoryId = territoryId;
    }

    public int EmployeeId { get; private set; }

    public string TerritoryId { get; private set; } = null!;

    public Employee? Employee { get; private set; }

    public Territory? Territory { get; private set; }
}
