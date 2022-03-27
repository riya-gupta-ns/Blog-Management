using NS.BLOGMGMT.Model;
using System.Diagnostics;
using NS.BLOGMGMT.Business;
using NS.BLOGMGMT.Web.Models;
using NS.BLOGMGMT.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace NS.BLOGMGMT.Web.Controllers;

public class AccountController : Controller
{
    private readonly IAccountBusiness _iAccountBuisness;
    //private readonly LoginLogout _loginLogout;

    public AccountController(IAccountBusiness iAccountBuisness)
    {
      _iAccountBuisness = iAccountBuisness; 
      //_loginLogout = loginLogout;
    }

    [HttpPost]  
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Login userObject) {  
      if(ModelState.IsValid) {
        var user = _iAccountBuisness.Login(userObject); 
        if ( user == null )
        {
          TempData["message"] = Constants.InValidCredentials; 
          return RedirectToAction("Index", "Blog");
        } else {
          LoginLogout _loginLogout = new LoginLogout(HttpContext);
          await _loginLogout.CreateClaims(user);
          return RedirectToAction("Index", "Blog");
        }
      }
      return RedirectToAction("Index", "Blog");
    }
   
    // LogOut
    public async Task <IActionResult> LogOut() {  
      try {
        LoginLogout _loginLogout = new LoginLogout(HttpContext);
        await _loginLogout.LogOut();
        return LocalRedirect("/Blog");  
      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }

    // SignUp
    [HttpGet]
    public IActionResult SignUp()
    {
      try {
        return View();
      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SignUp(UserModel userModel)
    {
      if ( ModelState.IsValid )
      {
        var IsUserExist = _iAccountBuisness.IsUserExist(userModel.UserName);
        if ( !IsUserExist )
        {
          var res = _iAccountBuisness.AddUser(userModel);
          return RedirectToAction("Login");
        }
        else
        {
          ViewBag.Message = ("Error: UserName already Exists");
        }
      }
      return View(userModel);
    }

    [Route("Account/Error/{error?}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string error)
    {
        return View(new ErrorViewModel { ErrorMessage = error, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
