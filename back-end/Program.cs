using back_end.Areas.Identity;
using back_end.Data;
using back_end.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using back_end.Crypt;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var connectionString = builder.Configuration.GetConnectionString("sqliteConnection") ?? throw new InvalidOperationException("Connection string not found.");
var connectionString = builder.Configuration.GetConnectionString("mysqlconnection") ?? throw new InvalidOperationException("Connection string not found.");
connectionString = DecAES.Dec(connectionString);
//var connectionString = builder.Configuration.GetConnectionString("devMysql") ?? throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseMySQL(connectionString));
builder.Services.AddDbContext<BookDbContext>(options =>
        options.UseMySQL(connectionString));
builder.Services.AddDbContext<rentallistContext>(options =>
        options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<UserDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
