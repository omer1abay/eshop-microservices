var builder = WebApplication.CreateBuilder(args);

//DI Container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure http req pipeline
app.MapCarter(); //ICarterModule implementasyonlarýný bizim için bulur. Ve minimal api'leri kullanýlýr hale getirecek
app.Run();
