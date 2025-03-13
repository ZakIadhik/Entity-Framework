using CarsDB;
using Dapper;
using Microsoft.Data.SqlClient;

public static class GetAllCars
{
    public static List<ObjectCar> GetAllCars1(string connectionString)
    {
        var commandString = "SELECT * FROM Cars";


        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
                
            var cars = connection.Query<ObjectCar>(commandString).AsList();
            return cars;
        }
            
    }
}