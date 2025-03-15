using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using test.Data;
using test.Models;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        using (var db = new ApplicationDbContext())
        {

            if (!db.Assumptions.Any())
            {
                var json = System.IO.File.ReadAllText("/home/holmen1/repos/json/test/assumptions.json");
                var jsonData = JsonDocument.Parse(json).RootElement.GetProperty("Assumptions");

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