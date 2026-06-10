using Microsoft.EntityFrameworkCore;

namespace lab13;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
}