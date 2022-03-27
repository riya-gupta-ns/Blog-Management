using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Business;

public interface IUserBusiness
{
  public List<Blog> GetAllBlogs();
  public User GetUserProfile(string userId);
  public List<Blog> GetUserBlogs(string userId);
  public bool EditUser(User user);
  public bool DeleteUser(int id);
}
