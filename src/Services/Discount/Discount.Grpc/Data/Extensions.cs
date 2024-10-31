using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Discount.Grpc.Data;

//Automating migration
public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();

        //auto migrate
        dbContext.Database.MigrateAsync();

        return app;
    }
}
