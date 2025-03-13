using Dapper;
using Microsoft.Data.SqlClient;
using System;
using CarsDB;
    
public static class AddCarToDatabase
{
    public static void AddCar(string brand, string model, int year, decimal price, string connectionString)
    {
        var commandString = "INSERT INTO Cars (Brand, Model, Year, Price) " +
                            "VALUES (@Brand, @Model, @Year, @Price);" +
                            "SELECT CAST(SCOPE_IDENTITY() AS INT)";  

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            
            var newCarId = connection.Execute(commandString, new { Brand = brand, Model = model, Year = year, Price = price });

            Console.WriteLine($"New Car Added with ID: {newCarId}");
        }
    }
}