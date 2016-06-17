using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    [Authorize]
    public async Task<IActionResult> Contact()
    {
      // use accesstoken to query other apis
      ViewBag.IdentityToken = await HttpContext.Authentication.GetTokenAsync("id_token");
      ViewBag.AccessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
      ViewBag.TokenizerToken = await HttpContext.Authentication.GetTokenAsync("tokenizer");

      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
