using Microsoft.EntityFrameworkCore;
using Inovasys.Data;
using Inovasys.Data.Dapper;
using Inovasys.Data.Interfaces;
using Inovasys.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connString)
);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IUserUserRepository, UserRepository>();
builder.Services.AddScoped<IApiUserRepository, ApiRepository>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();

        app.Logger.LogInformation("Database created/migrated successfully.");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while creating/migrating the database.");
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
