using janine_aviation.Models;
using janine_aviation.Services;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddScoped<janine_aviation.Services.IAviationServices,janine_aviation.Services.AviationServices>();
builder.Services.AddScoped<IAviationServices, AviationServices>();

builder.Services.AddControllersWithViews();


// 1. Hole den Connection String (muss exakt wie in appsettings.json heiﬂen)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=Aviation.db";

// 2. Registriere den DbContext (NUR EINMAL!)
builder.Services.AddDbContext<AviationContext>(options =>
    options.UseSqlite(connectionString));

// 3. Den Rest so lassen (AddControllersWithViews etc.)


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider; try
    {
        var context = services.GetRequiredService<AviationContext>();
        //Erzeugt aviation.db im Projekt

        context.Database.EnsureCreated();
        Console.WriteLine(">>> SUCCESS: Aviation.db wurde in Eupen angelegt.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(">>> ERROR beim Anlegen der DB: " + ex.Message);
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();




app.Run();
