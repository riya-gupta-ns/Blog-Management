using NS.BLOGMGMT.Repository;
using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Data.CustomEntities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Business;

public class AdminBusiness : IAdminBusiness
{
  private readonly IAdminRepository _iAdminRepository;
  public AdminBusiness(IAdminRepository iAdminRepository)
  {
    _iAdminRepository = iAdminRepository; 
  }

  public Blog GetBlogById(int id)
  {
    return _iAdminRepository.GetBlogById(id);
  }

  public User Login(Login userObject)
  {
    return _iAdminRepository.Login(userObject);
  }

  public List<BlogAndUser> GetBlogAndUser()
  {
    return _iAdminRepository.GetBlogAndUser();
  }

  public bool PublishBlog(int id)
  {
    return _iAdminRepository.PublishBlog(id);
  }

  public bool RejectBlog(int id) {
    return _iAdminRepository.RejectBlog(id);
  }

  public List<BlogType> BlogTypes()
  {
    return _iAdminRepository.BlogTypes();
  }

  public bool EditBlog(Blog blog){
    return _iAdminRepository.EditBlog(blog);
  }
  
  public bool DeleteBlog(int id){
    return _iAdminRepository.DeleteBlog(id);
  }

  public List<User> GetAllUsers()
  {
    return _iAdminRepository.GetAllUsers();
  }

  public bool AddBlogType(BlogType blogType)
  {
    return _iAdminRepository.AddBlogType(blogType);
  }

  public List<BlogType> GetBlogTypes()
  {
      return _iAdminRepository.GetBlogTypes();
  }

  public bool DeleteBlogType(int id)
  {
      return _iAdminRepository.DeleteBlogType(id);
  }

  public bool ActivateBlogType(int id)
  {
    return _iAdminRepository.ActivateBlogType(id);
  }

  public User ViewUser(string userId)
  {
    return _iAdminRepository.ViewUser(userId);
  }

  public List<StaticContent> ContactUs()
  {
    return _iAdminRepository.ContactUs();
  }

  public bool EditContactUs(List<StaticContent> staticContents)
  {
    return _iAdminRepository.EditContactUs(staticContents);
  }

  public bool ActivateUser(int id) {
    return _iAdminRepository.ActivateUser(id);
  }

  public bool DeActivateUser(int id) {
    return _iAdminRepository.DeActivateUser(id);
  }

}
