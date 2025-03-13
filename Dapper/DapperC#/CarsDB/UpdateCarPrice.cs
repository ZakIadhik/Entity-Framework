using Dapper;
using Microsoft.Data.SqlClient;

public static class UpdateCarPrice
{
    public static void UpdateCarPrice1(int carId, decimal newPrice, string connectionString)
    {
        var commandString = "UPDATE Cars SET Price = @NewPrice WHERE Id = @carId";

        using (var connection = new SqlConnection(connectionString))
        {

            connection.Open();
                
            var rowsAffected = connection.Execute(commandString, new {NewPrice = newPrice, CarId = carId});

            if (rowsAffected > 0)
            {
                Console.WriteLine("Car Price Updated");
            }
            else
            {
                Console.WriteLine("Car Price NOT Updated");
            }
        }
    }
}