using NS.BLOGMGMT.Model;
using System.Diagnostics;
using NS.BLOGMGMT.Business;
using System.Security.Claims;
using NS.BLOGMGMT.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NS.BLOGMGMT.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NS.BLOGMGMT.Web.Controllers;

public class BlogController : Controller
{
    private readonly ILogger<BlogController> _logger;
    private readonly IBlogBusiness _iBlogBuisness;
    private  Int64 BlogId { get; set; }

    public BlogController(ILogger<BlogController> logger, IBlogBusiness iBlogBuisness)
    {
      _logger = logger;
      _iBlogBuisness = iBlogBuisness;
    }

    public IActionResult Index(string searchData, int? page)
    {
        if (searchData != null)
        {
          ViewBag.s = searchData;
          var resSearch = _iBlogBuisness.GetAllBlogs();
          var resS = resSearch.blogAndUsers;
          var ues = resS.Where(search => search.BlogTitle.ToUpper().Contains(searchData.ToUpper()));
          resSearch.blogAndUsers = ues.ToList();
          ViewBag.BlogTypes = _iBlogBuisness.BlogTypes();
            var pagerSearch = new Pager(resSearch.blogAndUsers.Count(), page);
            var tSearch = resSearch.blogAndUsers.ToList();
            var mSearch = tSearch.Take(5);
            resSearch.blogAndUsers = mSearch.ToList();
            if (page != null)
            {
                var io = (page - 1) * 5;
                var u = tSearch.Skip(Convert.ToInt32(io)).Take(5);
                resSearch.blogAndUsers = u.ToList();
            }
            var viewModel1 = new BlogAndLoginModel
            {
                Pager = pagerSearch
            };
            ViewBag.lk = viewModel1;
            return View(resSearch);
        }
        
        ViewBag.s = null;
        ViewBag.BlogTypes = _iBlogBuisness.BlogTypes();
        var res = _iBlogBuisness.GetAllBlogs();
        var pager = new Pager(res.blogAndUsers.Count(), page);
        var t = res.blogAndUsers.ToList();
        var m = t.Take(5);
        res.blogAndUsers = m.ToList();
        if (page != null)
        {
            var io = (page - 1) * 5;
            var u=t.Skip(Convert.ToInt32(io)).Take(5);
            res.blogAndUsers = u.ToList();
        }
        var viewModel = new BlogAndLoginModel
        {
            Pager = pager
        };
        ViewBag.lk = viewModel;
        return View(res);

    }


    // Create A Blog
    [Authorize]
    public IActionResult Create()
    {
      try {
        ViewBag.BlogTypes = _iBlogBuisness.BlogTypes();
        return View();
      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BlogModel blogModel)
    {
      try {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var blog = _iBlogBuisness.CreateBlog(blogModel,userId);
        return RedirectToAction("Blogs", "User");
      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }

    // Edit
    [Authorize]
    public IActionResult Edit(int id)
    {
      try {
        ViewBag.BlogTypes = _iBlogBuisness.BlogTypes();
        return View(_iBlogBuisness.GetBlogById(id));
      } catch ( Exception ex )
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
      
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Blog blog)
    { 
        var res = _iBlogBuisness.EditBlog(blog);
        var result = _iBlogBuisness.GetAllBlogs();
        return View("Index", result);
    }

    // Delete A Blog
    [Authorize]
    public IActionResult Delete(int id)
    {
      try {
        var res = _iBlogBuisness.DeleteBlog(id);
        return RedirectToAction("Blogs","User");
      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }

    [Authorize]
    // Get Blog By Id
    public IActionResult ShowBlog(int id)
    {
      try {
          var blog = _iBlogBuisness.GetBlogById(id);
          return View(blog);

      } catch ( Exception ex)
      {
        return RedirectToAction("Error", new { error = ex.Message });
      }

    }

    public IActionResult ViewBlog(int id)
    {
      try {
        var blog = _iBlogBuisness.GetBlogById(id);
        return View(blog);
      } catch ( Exception ex) {
        return RedirectToAction("Error", new { error = ex.Message });
      }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ViewBlog (CommentModel commentModel)
    {  
      try {
        var res = _iBlogBuisness.Comment(commentModel);
        return RedirectToAction("ViewBlog");
      } catch ( Exception ex) {
        return RedirectToAction("Error", new { error = ex.Message });
      }
    }


    [Route("Blog/Error/{error?}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string error)
    {
        return View(new ErrorViewModel { ErrorMessage = error, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
