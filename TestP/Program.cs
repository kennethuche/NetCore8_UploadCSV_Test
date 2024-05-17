using Microsoft.EntityFrameworkCore;
using TestP.Data.Abstract;
using TestP.Data.Context;
using TestP.Data.Interface;
using TestP.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure SQL connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the application DB context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
// Custom error handling middleware
app.UseMiddleware<ErrorHandler>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
