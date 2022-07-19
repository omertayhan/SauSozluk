using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace BlazorSozluk.Infrastructure.Persistence.Context;
public class BlazorSozlukContext: DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public BlazorSozlukContext()
    {

    }
    public BlazorSozlukContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Entry> entries { get; set; }
    public DbSet<EntryVote> entryVotes { get; set; }
    public DbSet<EntryFavorite> entryFavorites { get; set; }
    public DbSet<EntryComment> entryComments { get; set; }
    public DbSet<EntryCommentFavorite> entryCommentFavorites { get; set; }
    public DbSet<EntryCommentVote> entryCommentVotes { get; set; }
    public DbSet<EmailConfirmation> emailConfirmations { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess); 
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    private void OnBeforeSave()
    {
        var addedEntities = ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (BaseEntity)i.Entity);
        PrepareAddedEntities(addedEntities);
    }
    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreatedDate == DateTime.MinValue)
                entity.CreatedDate = DateTime.Now;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {   
        if(optionsBuilder.IsConfigured)
        {
            var connStr = "Data Source=localhost;Initial Catalog=blazorsozluk;Persist Security Info=True;User ID=postgres;Password=postgres";
            optionsBuilder.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }
}
