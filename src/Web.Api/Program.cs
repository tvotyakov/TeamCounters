using Serilog;

using TeamCounters.Application;
using TeamCounters.DataAccess.InMemory;
using TeamCounters.Web.Api.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSerilog((services, opts) => opts
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.WithProperty("Application", builder.Environment.ApplicationName)
    .Enrich.WithProperty("Env", builder.Environment.EnvironmentName)
);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{builder.Environment.ApplicationName}.xml"));
});

builder.Services
    .AddApplicationLayer()
    .AddInMemoryDataAccess();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(opts => { });

app.UseAuthorization();

app.MapControllers();

app.Run();
