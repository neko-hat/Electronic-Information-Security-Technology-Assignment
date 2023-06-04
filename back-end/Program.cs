using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using MySql.EntityFrameworkCore;
using back_end.Crypt;
using back_end.Data;
using Microsoft.AspNetCore.Identity;
using back_end.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("mysqlconnection") ?? throw new InvalidOperationException("Connection string  not found.");
connectionString = DecAES.Dec(connectionString);

builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDataContext>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseDefaultFiles(); // serve index.html
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
