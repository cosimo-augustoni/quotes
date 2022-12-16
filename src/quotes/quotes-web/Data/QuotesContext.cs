using Microsoft.EntityFrameworkCore;

namespace quotes_web.Data;

public class QuotesContext : DbContext
{
    public QuotesContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Quote>? Quotes { get; set; }

    public DbSet<Author>? Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var quotesBuilder = modelBuilder.Entity<Quote>();
        quotesBuilder.HasKey(q => q.Id);

        var authorBuilder = modelBuilder.Entity<Author>();
        authorBuilder.HasKey(q => q.Id);

        base.OnModelCreating(modelBuilder);
    }
}