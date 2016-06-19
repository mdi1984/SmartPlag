using System.Collections;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace SmartPlag.Identity.Model
{
  public class Clients
  {
    public static IEnumerable<Client> Get()
    {
      return new List<Client>
      {
        new Client
        {
          ClientId = "spmanager",
          ClientName = "SmartPlag Manager",
          AllowedGrantTypes = GrantTypes.Hybrid,

          RedirectUris = new List<string>
          {
              "http://localhost:5003/signin-oidc"
          },
          ClientSecrets = new List<Secret>
          {
            new Secret("managerSecret".Sha256())
          },
          AllowedScopes = new List<string>
          {
            StandardScopes.OpenId.Name,
            StandardScopes.Profile.Name,
            "manager",
            "comparison",
            "tokenizer",
            "fullaccess"
          },
        },
        new Client
        {
          ClientId = "postman",
          ClientName = "Postman",
          AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
          ClientSecrets = new List<Secret>
          {
            new Secret("postman".Sha256())
          },
          AllowedScopes = new List<string>
          {
            StandardScopes.OpenId.Name,
            StandardScopes.Profile.Name,
            "manager",
            "comparison",
            "tokenizer",
            "fullaccess"
          },
        }

      };
    }
  }
}