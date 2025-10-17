namespace NorthWind.API.Security;

internal static class AuthorizationPolicyNames
{
    internal const string ProductView = "ProductView";
    internal const string ProductCreate = "ProductCreate";
    internal const string CustomerView = "CustomerView";
    internal const string OrderView = "OrderView";
    internal const string OrderCreate = "OrderCreate";
    internal const string OrderUpdateStatus = "OrderUpdateStatus";
    internal const string OrderDelete = "OrderDelete";
    internal const string OrderUpdate = "OrderUpdate";
    internal const string EmployeeView = "EmployeeView";
}

internal static class RoleNames
{
    internal const string ProductManager = "product_manager";
    internal const string SalesRepresentative = "sales_representative";
    internal const string WarehouseClerk = "warehouse_clerk";
}
