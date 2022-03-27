using NS.BLOGMGMT.Repository;
using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Business;

public class UserBusiness : IUserBusiness
{
  private readonly IUserRepository _iUserRepository;

  public UserBusiness(IUserRepository iUserRepository )
  {
    _iUserRepository = iUserRepository;
  }

  public List<Blog> GetAllBlogs()
  {
    return _iUserRepository.GetAllBlogs();
  }

  public User GetUserProfile(string userId)
  {
    return _iUserRepository.GetUserProfile(userId);
  }

  public List<Blog> GetUserBlogs(string userId){
    return _iUserRepository.GetUserBlogs(userId);
  }

  public bool EditUser(User user)
  {
      return _iUserRepository.EditUser(user);
 }
  public bool DeleteUser(int id)
  {
      return _iUserRepository.DeleteUser(id);
  }
}
