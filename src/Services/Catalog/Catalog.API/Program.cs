

var builder = WebApplication.CreateBuilder(args);

//DI Container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //validation middleware
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions(); //Crud operasyonlar� i�in lightweightsession kullanaca��z

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly); //fluent validation

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure http req pipeline
app.MapCarter(); //ICarterModule implementasyonlar�n� bizim i�in bulur. Ve minimal api'leri kullan�l�r hale getirecek

app.UseExceptionHandler(options => { }); //di'da ekledi�imiz customexception'a g�vendi�imizi belirtir bu kullan�m

app.Run();
