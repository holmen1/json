using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using test.Data;
using test.Models;

internal class Program
{
    private static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        using (var db = new ApplicationDbContext(configuration))
        {
            //if (!TableExists(db, "Assumptions")) db.Database.Migrate();

            if (db.Assumptions.Any())
            {
                var assumptions = LoadAssumptionsFromJson(configuration);
                db.Assumptions.AddRange(assumptions);
                db.SaveChanges();
            }
        }
    }


    private static List<Assumption> LoadAssumptionsFromJson(IConfiguration configuration)
    {
        var jsonFilePath = configuration["JsonSettings:FilePath"];
        var jsonPropertyName = configuration["JsonSettings:PropertyName"];

        var json = File.ReadAllText(jsonFilePath);
        var jsonDocument = JsonDocument.Parse(json);
        var assumptionsElement = jsonDocument.RootElement.GetProperty(jsonPropertyName);
        
        var assumptions = JsonSerializer.Deserialize<List<Assumption>>(assumptionsElement.GetRawText());

        return assumptions;
    }
}