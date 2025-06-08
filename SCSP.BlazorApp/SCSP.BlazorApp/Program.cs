using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using SCSP.BlazorApp.Components;
using SCSP.BusinessLogic.Extensions;
using SCSP.BusinessLogic.Services.Implements;
using SCSP.BusinessLogic.Services.Interfaces;
using SCSP.DataAccess.Extensions;
using SCSP.DataAccess.Models;

namespace SCSP.BlazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("firebase.json")
        });
        
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        
        builder.Services.AddDataAccess();
        builder.Services.AddBusinessLogic();
        
        var connectionString =
            builder.Configuration.GetConnectionString("ScspSqlServer")
            ?? throw new InvalidOperationException("Connection string"
                                                   + "'ScspSqlServer' not found.");

        builder.Services.AddDbContext<ScspContext>(options =>
            options.UseSqlServer(connectionString));
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}