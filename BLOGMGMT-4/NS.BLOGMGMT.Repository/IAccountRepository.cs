using NS.BLOGMGMT.Model;
using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Repository;

public interface IAccountRepository 
{
  public bool AddUser(UserModel userModel);
  public User Login(Login userObject);
}
