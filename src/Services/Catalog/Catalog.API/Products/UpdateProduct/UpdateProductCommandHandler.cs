
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Product Product) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}", command);

        var productOld = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);

        if (productOld is null)
        {
            throw new ProductNotFoundException();
        }

        productOld = command.Product;

        session.Update<Product>(productOld);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);

    }
}
