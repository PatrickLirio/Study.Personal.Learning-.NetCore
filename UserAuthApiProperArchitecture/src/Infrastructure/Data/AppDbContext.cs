using Microsoft.EntityFrameworkCore;


namespace UserAuthApiProperArchitecture.Infrastructure.Data
{
    // AppDbContext inherits from DbContext — this gives it all EF Core powers 
    public class AppDbContext : DbContext
    {
        // Constructor: receives configuration options (injected by DI) 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        // DbSet<User> = a table called 'Users' in the database 
        // You query this like: _context.Users.Where(u => u.Email == email) 
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply all IEntityTypeConfiguration<T> classes automatically

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
