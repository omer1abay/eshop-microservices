namespace Ordering.API;

public static class DependencyInjection
{
    //DI container
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddCarter();

        return services;
    }

    //Middleware
    public static WebApplication UseApiServices(this WebApplication app) 
    { 
        //app.MapCarter();
        

        return app;  
    }

}
