using Invoices.Api;
using Invoices.Api.Converters;
using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Api.Seeding;
using Invoices.Data;
using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Interfaces;
using Invoices.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Swagger = nástroj pro dokumentaci a testování API
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // JSON vlastnosti budou camelCase (např. "firstName" místo "FirstName")
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Enumy budou serializovány jako řetězce
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // Přidání konvertoru pro DateOnly
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });


builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();
builder.Services.AddScoped<IStatisticsManager, StatisticsManager>();


// CORS policy - povolení přístupu z Blazor klienta + Vercel klienta
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllClients",
        policy =>
        {
            policy.WithOrigins(
                         "https://localhost:7024",
                         "https://invoice-client-starter-nu.vercel.app",
                         "https://invoice-client-starter-nu.vercel.app/"
                             )

             .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// Připojení k databázi, viz soubor appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Seed na sample data
// builder.Services.AddTransient<SampleDataSeeder>();


var app = builder.Build();

// Pokude je aplikace ve vývojovém režimu, povolíme Swagger a vytvoříme vzorová data
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    /*
        // Vytvoření vzorových dat
        using (var scope = app.Services.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<SampleDataSeeder>();
            seeder.SeedPersons();
        }
    */
}
// Zajistíme, že databáze je vytvořena
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllClients");
app.UseAuthorization();
app.MapControllers();

app.Run();
