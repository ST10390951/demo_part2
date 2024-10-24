using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Show detailed error pages in development.
    app.UseDeveloperExceptionPage();
}
else
{
    // Handle exceptions in production.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTP Strict Transport Security
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve static files

app.UseRouting(); // Enable routing

app.UseAuthorization(); // Enable authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run(); // Start the application
