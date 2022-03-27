using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Repository;

public interface IUserRepository 
{
  public List<Blog> GetAllBlogs();
  public User GetUserProfile(string userId);
  public List<Blog> GetUserBlogs(string userId);
  public bool EditUser(User user);
  public bool DeleteUser(int id);

}
