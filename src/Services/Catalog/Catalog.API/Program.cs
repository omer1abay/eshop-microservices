var builder = WebApplication.CreateBuilder(args);

//DI Container

var app = builder.Build();

//Configure http req pipeline

app.Run();
