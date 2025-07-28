using BusinessLogic;
using BusinessLogic.IServices;
using BusinessObject.Models;
using DataAccess;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddRazorPages();
builder.Services.AddSession();

var app = builder.Build();


// Register Repository
//builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Register services
//builder.Services.AddScoped<IPaymentService, PaymentService>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Home/Index");
    return Task.CompletedTask;
});

app.Run();
