namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateProductResponse(Guid Id); //mapster'da property isimleri büyük harfle başlamadığında mapleme işlemini yapmıyor

public class CreateProductEndpoint : ICarterModule
{
    //minimal apis
    public void AddRoutes(IEndpointRouteBuilder app) //minimal apilerimizi oluşturacağız
    {

        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>(); //mapping to command object

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}",response);

        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");

        
    }
}
