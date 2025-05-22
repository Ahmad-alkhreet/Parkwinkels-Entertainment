using DataAccess;
using DataAccess.Repositories;
using Service;

var builder = WebApplication.CreateBuilder(args);

//  Connection string ophalen uit appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//  Dependency Injection
builder.Services.AddSingleton(new DatabaseHelper(connectionString));
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ActiviteitRepository>();
builder.Services.AddScoped<MedewerkerRepository>();
builder.Services.AddScoped<NieuwsberichtRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ActiviteitService>();
builder.Services.AddScoped<MedewerkerService>();
builder.Services.AddScoped<NieuwsberichtService>();

// Razor Pages toevoegen
builder.Services.AddRazorPages();

// Build & pipeline configuratie
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
