namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductResponse(IEnumerable<Product> Products);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}
