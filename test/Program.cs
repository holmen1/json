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
        
        // read json to list of assumptions where each assumption has 3 UNIQUE levels and a value
        var assumptions = LoadAssumptionsFromJson(configuration);
        var assumptionsDictionary = ConstructAssumptionsDictionary(assumptions);

        // Example usage
        foreach (var level1 in assumptionsDictionary)
        {
            Console.WriteLine($"Level1: {level1.Key}");
            foreach (var level2 in level1.Value)
            {
                Console.WriteLine($"\tLevel2: {level2.Key}");
                foreach (var level3 in level2.Value)
                {
                    Console.WriteLine($"\t\tLevel3: {level3.Key}, Value: {level3.Value}");
                }
            }
        }
        

        //UpdateDatabase(configuration);
    }
    
    private static void UpdateDatabase(IConfiguration configuration)
    {
        using (var db = new ApplicationDbContext(configuration))
        {
            //if (!TableExists(db, "Assumptions")) db.Database.Migrate();

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
        var jsonDocument = JsonDocument.Parse(json);
        var assumptionsElement = jsonDocument.RootElement.GetProperty(jsonPropertyName);
        
        var assumptions = JsonSerializer.Deserialize<List<Assumption>>(assumptionsElement.GetRawText());

        return assumptions;
    }
    
    private static Dictionary<Assumption1, Dictionary<Assumption2, Dictionary<Assumption3, double>>> ConstructAssumptionsDictionary(List<Assumption> assumptions)
    {
        var dictionary = new Dictionary<Assumption1, Dictionary<Assumption2, Dictionary<Assumption3, double>>>();

        foreach (var assumption in assumptions)
        {
            if (!dictionary.ContainsKey(assumption.Level1))
            {
                dictionary[assumption.Level1] = new Dictionary<Assumption2, Dictionary<Assumption3, double>>();
            }

            if (!dictionary[assumption.Level1].ContainsKey(assumption.Level2))
            {
                dictionary[assumption.Level1][assumption.Level2] = new Dictionary<Assumption3, double>();
            }

            dictionary[assumption.Level1][assumption.Level2][assumption.Level3] = assumption.Value;
        }

        return dictionary;
    }
}

