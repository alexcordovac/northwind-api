namespace NorthWind.API.Endpoints;

internal interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
