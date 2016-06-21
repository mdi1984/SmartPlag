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
      return View("About");
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
