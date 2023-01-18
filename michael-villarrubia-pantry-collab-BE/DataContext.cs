
namespace michael_villarrubia_pantry_collab_BE
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
    }
}
