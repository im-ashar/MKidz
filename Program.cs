using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using MKidz.Models.Database;

var builder = WebApplication.CreateBuilder(args);
string connString = @"Server=containers-us-west-143.railway.app;Port=6853;Database=railway;Uid=root;Pwd=3tS09fgkoEbDy2nT1jqb;";
//string connString = @"Data Source=(localdb)\MSSQLLocalDB;Database=MkidzRecords;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
var migrationAssembly = typeof(Program).Assembly.GetName().Name;

// Add services to the container.
builder.Services.AddDbContext<RecordsDBContext>(options => options.UseMySQL(connString, sql => sql.MigrationsAssembly(migrationAssembly)));
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
