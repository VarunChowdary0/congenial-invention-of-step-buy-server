using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using step_buy_server.data;

var builder = WebApplication.CreateBuilder(args);

// Configure logging (disable SQL query logs)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestMethod |
                            HttpLoggingFields.RequestPath |
                            HttpLoggingFields.ResponseStatusCode;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});


// Configure DbContext without SQL logging
builder.Services.AddDbContext<AppDBConfig>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
            .EnableSensitiveDataLogging(false) // Disable detailed SQL logs
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning) // Log only warnings/errors
);

builder.Services.AddControllers()
    .AddJsonOptions(op =>
    {
        op.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        op.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseApiRouteLogging(); // Add this line to use the custom middleware

app.UseHttpLogging(); // Enable API route logging
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();