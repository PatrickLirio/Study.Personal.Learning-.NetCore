
using UserAuthApiProperArchitecture.Domain.Entities;

namespace UserAuthApiProperArchitecture.Application.Interfaces
{
    // Job description for whoever generates JWT tokens 
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
