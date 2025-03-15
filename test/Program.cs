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
            if (!TableExists(db, "Assumptions")) db.Database.Migrate();

            if (!db.Assumptions.Any())
            {
                var assumptions = LoadAssumptionsFromJson(configuration);
                db.Assumptions.AddRange(assumptions);
                db.SaveChanges();
            }
        }
    }

    private static bool TableExists(ApplicationDbContext context, string tableName)
    {
        var connection = context.Database.GetDbConnection();
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"SELECT 1 FROM information_schema.tables WHERE table_name = '{tableName}'";
            return command.ExecuteScalar() != null;
        }
    }


    private static List<Assumption> LoadAssumptionsFromJson(IConfiguration configuration)
    {
        var jsonFilePath = configuration["JsonSettings:FilePath"];
        var jsonPropertyName = configuration["JsonSettings:PropertyName"];

        var json = File.ReadAllText(jsonFilePath);
        var jsonData = JsonDocument.Parse(json).RootElement.GetProperty(jsonPropertyName);

        var assumptions = new List<Assumption>();

        foreach (var level in jsonData.EnumerateObject())
        foreach (var subLevel in level.Value.EnumerateObject())
        foreach (var category in subLevel.Value.EnumerateObject())
            assumptions.Add(new Assumption
            {
                Level = level.Name,
                SubLevel = subLevel.Name,
                Kategory = category.Name,
                Value = category.Value.GetDouble()
            });

        return assumptions;
    }
}