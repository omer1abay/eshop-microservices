

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
}).UseLightweightSessions(); //Crud operasyonlarý için lightweightsession kullanacaðýz

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly); //fluent validation

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure http req pipeline
app.MapCarter(); //ICarterModule implementasyonlarýný bizim için bulur. Ve minimal api'leri kullanýlýr hale getirecek

app.UseExceptionHandler(options => { }); //di'da eklediðimiz customexception'a güvendiðimizi belirtir bu kullaným

app.Run();
