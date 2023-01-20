
namespace michael_villarrubia_pantry_collab_BE
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Pantry> Pantries { get; set; }
        public DbSet<PantryItem> PantryItems { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithMany(i => i.Recipes)
                .UsingEntity<RecipeIngredient>(
                    j => j
                        .HasOne(ri => ri.Ingredient)
                        .WithMany(i => i.RecipeIngredients)
                        .HasForeignKey(ri => ri.IngredientId),
                    j => j
                        .HasOne(ri => ri.Recipe)
                        .WithMany(r => r.RecipeIngredients)
                        .HasForeignKey(ri => ri.RecipeId),
                    j =>
                    {
                        j.HasKey(t => new { t.RecipeId, t.IngredientId });
                    });
        }
    }
}
