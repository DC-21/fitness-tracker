using Fit.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;

public partial class App : Application
{
    private FitDbContext _context;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Setup DbContext using the connection string from App.config
        var optionsBuilder = new DbContextOptionsBuilder<FitDbContext>();
        var connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        optionsBuilder.UseNpgsql(connectionString);

        // Create an instance of the DbContext
        _context = new FitDbContext(optionsBuilder.Options);

        // Now you can use _context for your data operations
    }
}
