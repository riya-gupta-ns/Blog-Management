using NS.BLOGMGMT.Model;
using System.Diagnostics;
using NS.BLOGMGMT.Business;
using NS.BLOGMGMT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace NS.BLOGMGMT.Web.Controllers;

public class AdminController : Controller
{

  private readonly IAdminBusiness _iAdminBusiness;
  private readonly IUserBusiness _iUserBusiness;
  //private readonly LoginLogout _loginLogout;
  private readonly IAccountBusiness _iAccountBusiness;

  public AdminController(IAdminBusiness iAdminBusiness, IUserBusiness iUserBusiness,IAccountBusiness iAccountBusiness)
  {
    _iAdminBusiness = iAdminBusiness; 
    _iUserBusiness = iUserBusiness; 
    //_loginLogout = loginLogout;
    _iAccountBusiness = iAccountBusiness;
  }

  // Get
  [Authorize(Roles = "0")]
  public IActionResult Index(int? page)
  {
    try {
      var res = _iAdminBusiness.GetBlogAndUser();
      return View(res);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  // Login
  [HttpGet]
  public IActionResult Login()
  {
    try {
      return View();
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [HttpPost]  
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(Login userObject) {  
    if ( ModelState.IsValid )
    {
      var user = _iAccountBusiness.Login(userObject);
      //var user = _iAdminBusiness.Login(userObject); 
      if ( user == null )
      {
        ViewBag.Message = Constants.InValidCredentials; 
      } else {
        LoginLogout _loginLogout = new LoginLogout(HttpContext);
        await _loginLogout.CreateClaims(user);
        return RedirectToAction("Index");
      }
    }
    return View(userObject);

  }


  // Reject Blog
  [Authorize(Roles = "0")]
  public IActionResult RejectBlog(int id)
  {
    try {
      var res = _iAdminBusiness.RejectBlog(id);
      return RedirectToAction("Index");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  // Publish Blog
  [Authorize(Roles = "0")]
  public IActionResult PublishBlog(int id)
  {
    try {
      var res = _iAdminBusiness.PublishBlog(id);
      return RedirectToAction("Index");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  // Edit
  [Authorize(Roles = "0")]
  public IActionResult EditBlog(int id)
  {
    try {
      ViewBag.BlogTypes = _iAdminBusiness.BlogTypes();
      return View(_iAdminBusiness.GetBlogById(id));
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult EditBlog(Blog blog)
  {
    try {
      var res = _iAdminBusiness.EditBlog(blog);
      return RedirectToAction("Index");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  // Delete A Blog
  [Authorize(Roles = "0")]
  public IActionResult Delete(int id)
  {
    try {
      var res = _iAdminBusiness.DeleteBlog(id);
      return RedirectToAction("Index");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }
  
  [Authorize(Roles = "0")]
  public IActionResult AllUsers()
  {
    try {
      var res = _iAdminBusiness.GetAllUsers();
      return View(res);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult AddBlogType(BlogType blogType)
  {
    try {
      var res = _iAdminBusiness.AddBlogType(blogType);
      return RedirectToAction("Index");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult ViewBlogType()
  {
    try {
      var res = _iAdminBusiness.GetBlogTypes();
      return View(res);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult DeleteBlogType(int id)
  {
    try {
      var res = _iAdminBusiness.DeleteBlogType(id);
      return RedirectToAction("ViewBlogType");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult ActivateBlogType(int id)
  {
    try {
      var res = _iAdminBusiness.ActivateBlogType(id);
      return RedirectToAction("ViewBlogType");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult ViewUser(string userId)
  {
    try {
      var user = _iAdminBusiness.ViewUser(userId);
      return View(user);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult AllBlogs()
  {
    try {
      var res = _iAdminBusiness.GetBlogAndUser();
      return View(res);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [HttpGet]
  [Authorize(Roles = "0")]
  public IActionResult StaticContent()
  {
    var model = _iAdminBusiness.ContactUs();
    return View(model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult StaticContent(List<StaticContent> staticContents)
  {
    var model = _iAdminBusiness.EditContactUs(staticContents);
    return RedirectToAction("Index");
  }

  public IActionResult ContactUs()
  {
    try {
      var modal = _iAdminBusiness.ContactUs();
      return View(modal);
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult ActivateUser(int id)
  {
    try {
      var res = _iAdminBusiness.ActivateUser(id);
      return RedirectToAction("AllUsers");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Authorize(Roles = "0")]
  public IActionResult DeActivateUser(int id)
  {
    try {
      _iAdminBusiness.DeActivateUser(id);
      return RedirectToAction("AllUsers");
    } catch ( Exception ex) {
      return RedirectToAction("Error", new { error = ex.Message });
    }
  }

  [Route("Admin/Error/{error?}")]
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error(string error)
  {
      return View(new ErrorViewModel { ErrorMessage = error, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }

}
