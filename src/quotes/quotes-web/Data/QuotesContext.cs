using Microsoft.EntityFrameworkCore;

namespace quotes_web.Data;

public class QuotesContext : DbContext
{
    public QuotesContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Quote> Quotes { get; set; } = default!;

    public DbSet<Author> Authors { get; set; } = default!;

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
        authorBuilder.HasKey(a => a.Id);
        authorBuilder.Property(a => a.Name);
        authorBuilder.HasOne(a => a.File)
          .WithOne()
          .HasForeignKey<Author>(a => a.FileId);

        var fileBuilder = modelBuilder.Entity<File>().ToTable("File");
        fileBuilder.HasKey(f => f.Id);
        fileBuilder.Property(f => f.Name);
        fileBuilder.Property(f => f.Base64Data);
        fileBuilder.Property(f => f.FileType);

        base.OnModelCreating(modelBuilder);
    }
}