namespace lab13;

public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public DateOnly? ReleaseDate { get; set; }
}