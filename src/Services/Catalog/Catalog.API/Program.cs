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
}).UseLightweightSessions(); //Crud operasyonlar� i�in lightweightsession kullanaca��z

var app = builder.Build();

//Configure http req pipeline
app.MapCarter(); //ICarterModule implementasyonlar�n� bizim i�in bulur. Ve minimal api'leri kullan�l�r hale getirecek
app.Run();
