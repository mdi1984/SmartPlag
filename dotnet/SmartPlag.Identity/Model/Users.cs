using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Services.InMemory;

namespace SmartPlag.Identity.Model
{
  public class Users
  {
    public static List<InMemoryUser> Get()
    {
      return new List<InMemoryUser>
        {
            new InMemoryUser
            {
                Subject = "mdi1984@gmail.com",
                Username = "mdi1984@gmail.com",
                Password = "superPass",
                Claims = new []
                {
                    new Claim(JwtClaimTypes.Name, "Markus Dietrich"),
                    // TODO: UserId or something like that... 
                }
            }
        };
    }

  }
}