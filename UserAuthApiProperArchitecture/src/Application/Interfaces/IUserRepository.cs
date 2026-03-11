
using UserAuthApiProperArchitecture.Domain.Entities;

namespace UserAuthApiProperArchitecture.Application.Interfaces
{

    // This is the JOB DESCRIPTION for whoever stores/retrieves Users. 
    // The Application layer only knows THIS interface, not SQL Server. 
    public class IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task AddAsync(User user);
        Task SaveChangeAsync();
    }
}
