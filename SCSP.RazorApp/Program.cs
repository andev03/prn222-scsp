using Microsoft.EntityFrameworkCore;
using SCSP.BusinessLogic.Extensions;
using SCSP.DataAccess.DatabaseContexts;
using SCSP.DataAccess.Extensions;

namespace SCSP.RazorApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddDataAccess();
        builder.Services.AddBusinessLogic();
        
        var connectionString =
            builder.Configuration.GetConnectionString("QuitSmokingAppDB_SQLServer")
            ?? throw new InvalidOperationException("Connection string"
                                                   + "'QuitSmokingAppDB_SQLServer' not found.");
        builder.Services.AddDbContext<QuitSmokingAppDbContext>(options => options.UseSqlServer(connectionString));
        
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
            .WithStaticAssets();

        app.Run();
    }
}