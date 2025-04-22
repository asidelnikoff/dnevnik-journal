using Dnevnik.Journal.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configure();

var app = builder.Build();
app.Configure(builder.Services);

app.Run();