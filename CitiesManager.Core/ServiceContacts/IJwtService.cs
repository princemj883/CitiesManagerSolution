using CitiesManager.Core.DTO;
using CitiesManager.Core.Identity;

namespace CitiesManager.Core.ServiceContacts;

public interface IJwtService
{
    AuthenticationResponse CreateJwtToken(ApplicationUser user);
}