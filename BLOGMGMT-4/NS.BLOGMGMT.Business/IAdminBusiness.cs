using NS.BLOGMGMT.Model;
using NS.BLOGMGMT.Data.Entities;
using System.Security.Claims;
using NS.BLOGMGMT.Data.CustomEntities;

namespace NS.BLOGMGMT.Business;


public interface IAdminBusiness
{
  public Blog GetBlogById(int id);
  public User Login(Login userObject);
  public List<BlogAndUser> GetBlogAndUser();
  public bool PublishBlog(int id);
  public bool RejectBlog(int id);
  public List<BlogType> BlogTypes();
  public bool EditBlog(Blog blog);
  public bool DeleteBlog(int id);
  public List<User> GetAllUsers();
  public bool AddBlogType(BlogType blogType);
  public List<BlogType> GetBlogTypes();
  public bool DeleteBlogType(int id);
  public bool ActivateBlogType(int id);
  public User ViewUser(string userId);
  public List<StaticContent> ContactUs();
  public bool EditContactUs(List<StaticContent> staticContents);
  public bool ActivateUser(int id);
  public bool DeActivateUser(int id);
}
