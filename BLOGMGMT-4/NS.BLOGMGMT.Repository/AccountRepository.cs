using NS.BLOGMGMT.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Repository;

public class AccountRepository : IAccountRepository 
{
  //Add User
  private string EncryptPassword(string password)
  {
      string pswstr = string.Empty;
      byte[] pswEncode = new byte[password.Length];
      pswEncode = System.Text.Encoding.UTF8.GetBytes(password);
      pswstr = Convert.ToBase64String(pswEncode);
      return pswstr;
  }

  public bool AddUser(UserModel userModel)
  {
      using (var context = new BlogDBContext())
      {
          var name = new SqlParameter("@FullName", userModel.UserFullName);
          var phone = new SqlParameter("@PhoneNumber", userModel.PhoneNumber);
          var gender = new SqlParameter("@Gender", userModel.gender);
          var mail = new SqlParameter("@Email", userModel.Email);
          var username = new SqlParameter("@UserName", userModel.UserName);
          var password = new SqlParameter("@Password", EncryptPassword(userModel.Password));
          var createdOn = new SqlParameter("@CreatedOn", DateTime.Now);
          context.Database.ExecuteSqlRaw("UspInsertUsers @FullName, @PhoneNumber, @Gender, @Email, @UserName, @Password, @CreatedOn", name, phone,gender,mail,username, password, createdOn);
      }
      return true;
  }

  public User Login(Login userObject) 
  {
    using (var context = new BlogDBContext()) {
      var  user = context.Users.Where(x => x.UserName == userObject.UserName && x.Password == EncryptPassword(userObject.Password) && x.IsDeleted == false).FirstOrDefault();
      if ( user == null )
        return null;
      return user;
    }  
  }

}
