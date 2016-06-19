using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartPlag.Manager.SimpleManager.Controllers
{
  public class AccountController : Controller
  {
    public async Task Logout()
    {
      await HttpContext.Authentication.SignOutAsync("Cookies");
      await HttpContext.Authentication.SignOutAsync("oidc");
    }
  }
}