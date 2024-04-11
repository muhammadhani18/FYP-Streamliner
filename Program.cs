using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SE_Project.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySQL(connectionString));

builder.Services.AddRazorPages();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
