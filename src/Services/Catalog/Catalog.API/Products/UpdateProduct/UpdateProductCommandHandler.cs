
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Product.Name).NotEmpty().Length(2,150).WithMessage("Name is required and must be between 2 and 150 characters");
    }
}
internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

        var productOld = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);

        if (productOld is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        productOld = command.Product;

        session.Update<Product>(productOld);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);

    }
}
