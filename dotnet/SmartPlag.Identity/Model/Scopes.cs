﻿using System.Collections.Generic;
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
            DisplayName = "Smartplag Manager Service",
            Description = "-add description-",
            Type = ScopeType.Resource,
        },
        new Scope
        {
            Name = "tokenizer",
            DisplayName = "Smartplag Tokenizer Service",
            Description = "-add description-",
            Type = ScopeType.Resource
        },
        new Scope
        {
            Name = "comparison",
            DisplayName = "Smartplag Comparison Service",
            Description = "-add description-",
            Type = ScopeType.Resource
        },
        new Scope
        {
          Name = "fullaccess",
          DisplayName = "Full Service Access",
          Description = "-add description",
          Type = ScopeType.Resource
        }
      };
    }
  }
}