namespace Proekt.Services;
using Proekt.Models;



public class GameStoreManager
{
    private readonly UserService _userService;
    private readonly GameService _gameService;
    private readonly OrderService _orderService;

    public GameStoreManager(UserService userService, GameService gameService , OrderService orderService)
    {
        _userService = userService;
        _gameService = gameService;
        _orderService = orderService;
    }

    public void Run(User currentUser)
    {
        while (true)
        {
            if (currentUser.Role == Roles.Admin)
            {
                AdminMenu(currentUser);
            }
            else
            {
                UserMenu(currentUser);
            }
        }
    }

    private void AdminMenu(User currentUser)
    {
        Console.WriteLine("\nВы администратор. Выберите действие:");
        Console.WriteLine("1. Добавить игру");
        Console.WriteLine("2. Удалить игру");
        Console.WriteLine("3. Редактировать игру");
        Console.WriteLine("4. Просмотреть каталог игр");
        Console.WriteLine("5. Выйти");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddGame();
                break;
            case "2":
                RemoveGame();
                break;
            case "3":
                EditGame();
                break;
            case "4":
                ViewGames();
                break;
            case "5":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

    private void UserMenu(User currentUser)
    {
        Console.WriteLine("\nВы пользователь. Выберите действие:");
        Console.WriteLine("1. Просмотреть каталог игр");
        Console.WriteLine("2. Пополнить баланс");
        Console.WriteLine("3. Купить игру");
        Console.WriteLine("4. История покупок");
        Console.WriteLine("5. Выйти");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ViewGames();
                break;
            case "2":
                TopUpBalance(currentUser);
                break;
            case "3":
                BuyGame(currentUser);
                break;
            case "4":
                ViewOrderHistory(currentUser);
                break;
            case "5":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }

    private void AddGame()
    {
        Console.WriteLine("Введите название игры:");
        var title = Console.ReadLine();
        Console.WriteLine("Введите цену игры:");
        var price = int.Parse(Console.ReadLine());
        var genre = _gameService.GetGenres().FirstOrDefault(); 
        var platform = _gameService.GetPlatforms().FirstOrDefault();

        _gameService.AddGame(title, genre.GenreId, platform.Id, price);
    }

    private void RemoveGame()
    {
        Console.WriteLine("Введите ID игры для удаления:");
        var gameId = int.Parse(Console.ReadLine());
        _gameService.RemoveGame(gameId);
    }

    private void EditGame()
    {
        Console.WriteLine("Введите ID игры для редактирования:");
        var gameId = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите новое название игры:");
        var title = Console.ReadLine();
        Console.WriteLine("Введите новую цену игры:");
        var price = int.Parse(Console.ReadLine());

        var genre = _gameService.GetGenres().FirstOrDefault(); 
        var platform = _gameService.GetPlatforms().FirstOrDefault(); 

        _gameService.EditGame(gameId, title, genre.GenreId, platform.Id, price);
    }

    private void ViewGames()
    {
        var games = _gameService.GetGames();
        foreach (var game in games)
        {
            Console.WriteLine($"{game.Title} - {game.Price} USD");
        }
    }

    private void TopUpBalance(User currentUser)
    {
        Console.WriteLine("Введите сумму для пополнения баланса:");
        var amount = int.Parse(Console.ReadLine());
        _userService.TopUpBalance(currentUser.UserId, amount);
    }

    private void BuyGame(User currentUser)
    {
        Console.WriteLine("\nДоступные игры для покупки:");
        var games = _gameService.GetGames();

        if (games == null || !games.Any())
        {
            Console.WriteLine("❌ Ошибка: В магазине нет доступных игр.");
            return;
        }

        foreach (var game in games)
        {
            Console.WriteLine($"ID: {game.GameId} | {game.Title} | {game.Genre?.Title ?? "Жанр неизвестен"} | {game.Platform?.Title ?? "Платформа неизвестна"} | Цена: {game.Price}");
        }

        Console.Write("\nВведите ID игры, которую хотите купить (или несколько через запятую): ");
        string input = Console.ReadLine();

        List<int> gameIds;
        try
        {
            gameIds = input.Split(',').Select(int.Parse).ToList();
        }
        catch
        {
            Console.WriteLine("❌ Ошибка: Некорректный ввод.");
            return;
        }

        if (_orderService == null)
        {
            Console.WriteLine("❌ Ошибка: _orderService не инициализирован.");
            return;
        }

        try
        {
            var order = _orderService.CreateOrder(currentUser.UserId, gameIds);

            if (order == null)
            {
                Console.WriteLine("❌ Ошибка: Не удалось создать заказ.");
                return;
            }

            Console.WriteLine($"✅ Покупка успешна! Ваш заказ {order.OrderId} создан на сумму {order.TotalAmount}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка: {ex.Message}");
        }
    }



    private void ViewOrderHistory(User currentUser)
    {
        var orders = _userService.GetOrderHistory(currentUser.UserId);
        foreach (var order in orders)
        {
            Console.WriteLine($"Order {order.OrderId} - Total: {order.TotalAmount} - Date: {order.Date}");
        }
    }
}
