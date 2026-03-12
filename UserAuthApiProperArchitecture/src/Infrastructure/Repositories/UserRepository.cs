

using Microsoft.EntityFrameworkCore;
using UserAuthApiProperArchitecture.Application.Interfaces;
using UserAuthApiProperArchitecture.Domain.Entities;
using UserAuthApiProperArchitecture.Infrastructure.Data;

namespace UserAuthApiProperArchitecture.Infrastructure.Repositories
{
    // UserRepository IMPLEMENTS IUserRepository (defined in Application) 
    // Infrastructure knows about Application, but Application does NOT know about Infrastructure 
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<User?> GetByEmailAsync(string email)=> await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<bool> ExistsByEmailAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);
        public async Task<bool> ExistsByUsernameAsync(string username) => await _context.Users.AnyAsync(u => u.Username == username);
        
        // Add tracks the new entity — EF Core will INSERT on SaveChanges 
        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);

        // SaveChangesAsync actually sends all pending SQL to the database 
        public async Task SaveChangeAsync() => await _context.SaveChangesAsync();
    }
}
