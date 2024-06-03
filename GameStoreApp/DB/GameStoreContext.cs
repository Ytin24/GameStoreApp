using Microsoft.EntityFrameworkCore;

public class GameStoreContext : DbContext {
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<GameCategory> GameCategories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<LibraryGame> LibraryGames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlServer("data source=ytin-pc;initial catalog=SteamDiplom;trusted_connection=true;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<GameCategory>()
            .HasKey(gc => new { gc.IDGame, gc.IDCategory });
        modelBuilder.Entity<GameCategory>()
            .HasOne(gc => gc.Game)
            .WithMany(g => g.GameCategories)
            .HasForeignKey(gc => gc.IDGame);
        modelBuilder.Entity<GameCategory>()
            .HasOne(gc => gc.Category)
            .WithMany(c => c.GameCategories)
            .HasForeignKey(gc => gc.IDCategory);

        modelBuilder.Entity<Profile>()
            .HasOne(p => p.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<Profile>(p => p.IDUser);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Library)
            .WithOne(l => l.User)
            .HasForeignKey<Library>(l => l.IDUser);

        modelBuilder.Entity<LibraryGame>()
            .HasKey(lg => new { lg.LibraryID, lg.GameID });
        modelBuilder.Entity<LibraryGame>()
            .HasOne(lg => lg.Library)
            .WithMany(l => l.LibraryGames)
            .HasForeignKey(lg => lg.LibraryID);
        modelBuilder.Entity<LibraryGame>()
            .HasOne(lg => lg.Game)
            .WithMany(g => g.LibraryGames)
            .HasForeignKey(lg => lg.GameID);
    }
}
