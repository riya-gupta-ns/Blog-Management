using System.Diagnostics;
using NS.BLOGMGMT.Business;
using System.Security.Claims;
using NS.BLOGMGMT.Web.Models;
using NS.BLOGMGMT.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NS.BLOGMGMT.Web.Controllers;

public class UserController : Controller
{
  private readonly IUserBusiness _iUserBuisness;

  public UserController(IUserBusiness iUserBuisness)
  {
    _iUserBuisness = iUserBuisness;
  }

  [Authorize]
  public IActionResult Index()
  {
    try {
      var res = _iUserBuisness.GetAllBlogs();
      return View(res);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }
  
  [Authorize]
  public IActionResult Profile()
  {
    try {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var model = _iUserBuisness.GetUserProfile(userId);
      return View(model);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize]
  public IActionResult Blogs()
  {
    try {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var blogs = _iUserBuisness.GetUserBlogs(userId);
      return View(blogs);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize]
  public IActionResult Edit()
  {
    try {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      return View(_iUserBuisness.GetUserProfile(userId));
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(User user)
  {
    try {
      var res = _iUserBuisness.EditUser(user);
      var id = user.UserId;
      return View("Profile", _iUserBuisness.GetUserProfile(id.ToString()));
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize]
  public IActionResult DeleteUser(int id)
  {
    try {
      var res = _iUserBuisness.DeleteUser(id);
      return RedirectToAction("SignUp", "Account");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Route("User/Error/{error?}")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error(string error)
  {
      return View(new ErrorViewModel { ErrorMessage = error, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

}
