using CarsDB;
using Dapper;
using Microsoft.Data.SqlClient;

public static class GetCarsByBrand
{
    public static List<ObjectCar> GetCarsByBrand1(string connectionString, string brandName)
    {
        var commandString = "SELECT * FROM Cars WHERE Brand = @BrandName";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
                
            var cars = connection.Query<ObjectCar>(commandString, new { BrandName = brandName }).AsList();

            return cars;
        }
    }
}