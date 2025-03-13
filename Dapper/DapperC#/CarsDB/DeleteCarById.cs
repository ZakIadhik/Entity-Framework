using Dapper;
using Microsoft.Data.SqlClient;

public static class DeleteCarById
{
    public static void DeleteCarById1(int carId, string connectionString)
    {
        var commandString = "DELETE FROM Cars WHERE Id = @CarId";


        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
                
            var rowsAffected = connection.Execute(commandString, new {CarId = carId});

            if (rowsAffected > 0)
            {
                Console.WriteLine("Car deleted");
            }
            else
            {
                Console.WriteLine("Car not found");
            }
        }
    }
}