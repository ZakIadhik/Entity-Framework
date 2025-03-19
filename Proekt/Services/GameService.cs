using Proekt.DataBase;
using Proekt.Models;
using Microsoft.EntityFrameworkCore;

namespace Proekt.Services;

public class GameService
{
    private readonly GameStoreContext _context;


    public GameService(GameStoreContext context)
    {
        _context = context;
    }
    
    public void AddGame(string title, int genreId, int platformId, int price)
    {
        var game = new Game
        {
            Title = title,
            GenreId = genreId,
            PlatformId = platformId,
            Price = price
        };
        _context.Games.Add(game);
        _context.SaveChanges();
    }

    public void RemoveGame(int gameId)
    {
        var game = _context.Games.Find(gameId);
        if (game is not null)
        {
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
    }

    public void EditGame(int gameId, string title, int genreId, int platformId, int price)
    {
        var game = _context.Games.Find(gameId);
        if (game is not null)
        {
            game.Title = title;
            game.GenreId = genreId;
            game.PlatformId = platformId;
            game.Price = price;
            _context.SaveChanges();
                
        }
    }
    
    public List<Game> GetGames()
    {
        return _context.Games.Include(g => g.Platform).ToList();
    }
    
    public List<Genre> GetGenres()
    {
        return _context.Genres.ToList();
    }

  
    public List<Platform> GetPlatforms()
    {
        return _context.Platforms.ToList();
    }
}