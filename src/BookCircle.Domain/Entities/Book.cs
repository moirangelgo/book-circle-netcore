namespace BookCircle.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Votes { get; set; } = 0;
    public int Progress { get; set; } = 0;

    public Club Club { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
