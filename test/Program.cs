using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json;
using test.Data;
using test.Models;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        
        using (var db = new ApplicationDbContext(configuration))
        {

            if (!db.Assumptions.Any())
            {
                var jsonFilePath = configuration["JsonSettings:FilePath"];
                var jsonPropertyName = configuration["JsonSettings:PropertyName"];

                var json = System.IO.File.ReadAllText(jsonFilePath);
                var jsonData = JsonDocument.Parse(json).RootElement.GetProperty(jsonPropertyName);

                var assumptions = new List<Assumption>();

                foreach (var level in jsonData.EnumerateObject())
                {
                    foreach (var subLevel in level.Value.EnumerateObject())
                    {
                        foreach (var category in subLevel.Value.EnumerateObject())
                        {
                            assumptions.Add(new Assumption
                            {
                                Level = level.Name,
                                SubLevel = subLevel.Name,
                                Kategory = category.Name,
                                Value = category.Value.GetDouble()
                            });
                        }
                    }
                }

                db.Assumptions.AddRange(assumptions);
                db.SaveChanges();
            }
        }
    }
}