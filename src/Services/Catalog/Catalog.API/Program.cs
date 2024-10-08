var builder = WebApplication.CreateBuilder(args);

//DI Container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions(); //Crud operasyonları için lightweightsession kullanacağız

var app = builder.Build();

//Configure http req pipeline
app.MapCarter(); //ICarterModule implementasyonlarını bizim için bulur. Ve minimal api'leri kullanılır hale getirecek
app.Run();
