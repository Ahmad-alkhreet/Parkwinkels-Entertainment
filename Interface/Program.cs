using DataAccess;
using DataAccess.Repositories;
using Domain;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Connection string uit appsettings.json
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Dependency Injection
builder.Services.AddSingleton(new DatabaseHelper(connectionString));

// Repositories 
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ActiviteitRepository>();
builder.Services.AddScoped<NieuwsberichtRepository>();

// Repositories (interface)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IActiviteitRepository, ActiviteitRepository>();
builder.Services.AddScoped<INieuwsberichtRepository, NieuwsberichtRepository>();

// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ActiviteitService>();
builder.Services.AddScoped<NieuwsberichtService>();

// Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware pipeline
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
