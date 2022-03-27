using NS.BLOGMGMT.Repository;
using NS.BLOGMGMT.Model;
using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Business;

public class AccountBusiness : IAccountBusiness
{
  private readonly IAccountRepository _iAccountRepository;

  public AccountBusiness(IAccountRepository iAccountRepository)
  {
    _iAccountRepository = iAccountRepository;
  }

  public bool AddUser(UserModel userModel) {
    return _iAccountRepository.AddUser(userModel);
  }

  public User Login(Login userObject) {
    return _iAccountRepository.Login(userObject); 
  }

  public bool IsUserExist(string userName)
  {
      var context = new BlogDBContext();
      var userDetail = context.Users.Where(x => x.UserName == userName).FirstOrDefault();
      if (userDetail != null)
      {
        return true;
      }
      else
      {
        return false;
      }
  }
}
