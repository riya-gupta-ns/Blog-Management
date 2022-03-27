using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Data.CustomEntities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Repository;

public interface IAdminRepository
{
  public User Login(Login userObject);
  public List<BlogAndUser> GetBlogAndUser();
  public bool PublishBlog(int id);
  public bool RejectBlog(int id);
  public List<BlogType> BlogTypes();
  public Blog GetBlogById(int id);
  public List<Comment> GetComment(int blogId);
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
