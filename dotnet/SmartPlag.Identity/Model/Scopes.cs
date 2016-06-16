using System.Collections.Generic;
using IdentityServer4.Models;

namespace SmartPlag.Identity.Model
{
  public class Scopes
  {
    public static IEnumerable<Scope> Get()
    {
      return new[]
      {
        StandardScopes.OpenId,
        StandardScopes.Profile,

        new Scope
        {
            Name = "manager",
            DisplayName = "Smartplag Manager",
            Description = "-add description-",
            Type = ScopeType.Resource
        },
        new Scope
        {
            Name = "tokenizer",
            DisplayName = "Smartplag Tokenizer",
            Description = "-add description-",
            Type = ScopeType.Resource
        }
      };
    }
  }
}