// using Microsoft.Data.SqlClient;
// using Dapper;
// using Microsoft.Extensions.Configuration;
//
//
// namespace CarsDB
// {
//     
//     
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             
//              // TASK-1. Вставка данных в таблицу
//              var connectionString = "Data Source=localhost;Initial Catalog=CarsDB;Integrated Security=true;TrustServerCertificate=True";
//             
//             
//            AddCarToDatabase.AddCar("BMW" , "BMW I7", 2020, 50000, connectionString);
//
//              
//              
//               TASK-2. Обновление данных
//           
//               int carId = 2;
//               decimal newPrice = 26000; 
//               UpdateCarPrice.UpdateCarPrice1(carId, newPrice, connectionString);
//              
//              
//              TASK-3. Удаление данных
//
//               int carId = 3;
//               
//               DeleteCarById.DeleteCarById1(carId, connectionString);
//              
//              TASK-4. Выборка всех автомобилей
//              
//               var cars = GetAllCars.GetAllCars1(connectionString);
//               
//               foreach (var AddCar in cars)
//               {
//                   Console.WriteLine(AddCar.ToString());
//               }
//              
//              
//              TASK-5. Фильтрация данных
//
//               string brandName = "BMW";
//               var cars = GetCarsByBrand.GetCarsByBrand1(connectionString, brandName);
//               
//               foreach (var car in cars)
//               {
//                   Console.WriteLine(car.ToString());
//               }
//
//          
//          }
//      }
//  }