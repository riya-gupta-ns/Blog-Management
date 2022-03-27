using NS.BLOGMGMT.Model;
using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Business;

public interface IAccountBusiness
{
  public bool AddUser(UserModel userModel);
  public User Login(Login userObject);
  public bool IsUserExist(string userName);
}
