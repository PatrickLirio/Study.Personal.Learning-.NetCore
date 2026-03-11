
namespace UserAuthApiProperArchitecture.Domain.Enums
{

        // An enum gives us a fixed list of allowed roles. 
        // The number values (1, 2) are stored in the database. 
        public enum UserRole
        {
            User = 1, // Regular user — can access basic endpoints
            Admin = 2 // Administrator — can access protected admin endpoints 
        }
    
}
