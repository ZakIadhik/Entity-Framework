namespace Proekt.Models;

public class Game
{
    public int GameId { get; set; }
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PlatformId { get; set; }
    public int Price { get; set; }
    
    public Genre Genre { get; set; }
    public Platform Platform { get; set; }
}