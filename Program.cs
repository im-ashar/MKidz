using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using MKidz.Models.Database;

var builder = WebApplication.CreateBuilder(args);
//string connString = @"Data Source=SQL5110.site4now.net;Initial Catalog=db_a9840a_admin;User Id=db_a9840a_admin_admin;Password=admin@mkidz123";
string connString = @"Data Source=SQL8005.site4now.net;Initial Catalog=db_a98a37_admin;User Id=db_a98a37_admin_admin;Password=admin@mkidz123";

var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.
builder.Services.AddDbContext<RecordsDBContext>(options => options.UseSqlServer(connString, sql => sql.MigrationsAssembly(migrationAssembly)));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRecordsFunctions, RecordsFunctions>();
var app = builder.Build();

var scope = app.Services.CreateScope();
var migRecordsContext = scope.ServiceProvider.GetRequiredService<RecordsDBContext>();
migRecordsContext.Database.MigrateAsync().Wait();

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
