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
        var quotesBuilder = modelBuilder.Entity<Quote>().ToTable("Quote");
        quotesBuilder.HasKey(q => q.Id);
        quotesBuilder.Property(q => q.Text);
        quotesBuilder.Property(q => q.DateOfQuote);
        quotesBuilder.Property(q => q.DeletedAt);
        quotesBuilder.HasOne(q => q.Author)
            .WithMany(a => a.Quotes)
            .HasForeignKey(q => q.AuthorId);

        var authorBuilder = modelBuilder.Entity<Author>().ToTable("Author");
        authorBuilder.HasKey(q => q.Id);
        authorBuilder.Property(a => a.Name);

        base.OnModelCreating(modelBuilder);
    }
}