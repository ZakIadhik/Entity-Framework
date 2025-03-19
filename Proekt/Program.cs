using Microsoft.EntityFrameworkCore;
using Proekt.Models; 
using Proekt.Services;  
using Proekt.DataBase; 



namespace Proekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            
            optionsBuilder
                .UseSqlServer("Data Source=localhost; Initial Catalog=GameStoreDB; User Id=sa; Password=admin; Trust Server Certificate=true;")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            var context = new GameStoreContext(optionsBuilder.Options);
            var userService = new UserService(context);
            var gameService = new GameService(context);
            var orderService = new OrderService(context);
            
            
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Зарегистрироваться");
                Console.WriteLine("2 - Войти");
                Console.WriteLine("3 - Выйти из программы");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Введите имя пользователя: ");
                    string name = Console.ReadLine();
                    
                    Console.WriteLine("Введите email пользователя: ");
                    string email = Console.ReadLine();
                    
                    Console.WriteLine("Введите пароль: ");
                    string password = Console.ReadLine();
                    
                    Console.WriteLine("Выберите роль: 1 - Администратор, 2 - Пользователь");
                    string roleChoice = Console.ReadLine();
                    Roles role = roleChoice == "1" ? Roles.Admin : Roles.User;
                    
                    var newUser = userService.RegisterUser(name, email, password, role);
                    Console.WriteLine($"Пользователь {newUser.Name} успешно зарегистрирован!");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Введите email: ");
                    string email = Console.ReadLine();
                    
                    Console.WriteLine("Введите пароль: ");
                    string password = Console.ReadLine();
                    
                    User currentUser = userService.LoginUser(email, password);
                    if (currentUser is null)
                    {
                        Console.WriteLine("Неверный email или пароль.");
                        continue;
                    }

                    Console.WriteLine($"Привет, {currentUser.Name}! Вы вошли как {(currentUser.Role == Roles.Admin ? "Администратор" : "Пользователь")}.");
                    
                    var gameStoreManager = new GameStoreManager(userService, gameService, orderService);
                    gameStoreManager.Run(currentUser);
                    break;
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Выход из программы...");
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
        }
    }
}