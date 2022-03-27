using NS.BLOGMGMT.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace NS.BLOGMGMT.Repository;

public class UserRepository : IUserRepository 
{

  // Get All Blogs
  public List<Blog> GetAllBlogs()
  {
    using ( var context = new BlogDBContext())
    {
      var res = context.Blogs.FromSqlRaw("UspGetAllBlogs").ToList();
      return res;
    }
  }

  // Get User Profile
  public User GetUserProfile(string userId)
  {
    using ( var context = new BlogDBContext())
    {
      var user = context.Users.Where(x => x.UserId == long.Parse(userId)).FirstOrDefault();
      return user;
    }
  }

  public List<Blog> GetUserBlogs(string userId)
  {
    using ( var context = new BlogDBContext())
    {
      var blogs = context.Blogs.FromSqlRaw("UspGetBlogsByUserId @UserId",new SqlParameter("@UserId", userId)).ToList();
      return blogs;
    }
  }

  public bool EditUser(User user)
  {
      using (var context = new BlogDBContext())
      {
          var paraamList = new List<SqlParameter>();
          paraamList.Add(new SqlParameter("@UserId", user.UserId));
          //paraamList.Add(new SqlParameter("@UserFullName", user.UserFullName));
          paraamList.Add(new SqlParameter("@UserName", user.UserName));
         // paraamList.Add(new SqlParameter("@Gender", user.Gender));
          paraamList.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));
          paraamList.Add(new SqlParameter("@Email", user.Email));
          paraamList.Add(new SqlParameter("@LastModifiedOn", DateTime.Now));
          context.Database.ExecuteSqlRaw("UspUpdateUser @UserId, @UserName, @PhoneNumber, @Email, @LastModifiedOn", paraamList);
      }
      return true;
  }

  public bool DeleteUser(int id)
  {
      using (var context = new BlogDBContext())
      {
          var paraamList = new List<SqlParameter>();
          paraamList.Add(new SqlParameter("@UserId", id));
          paraamList.Add(new SqlParameter("@DeletedOn", DateTime.Now));
          context.Database.ExecuteSqlRaw("UspDeleteUser @UserId, @DeletedOn", paraamList);
      }
      return true;
  }
}
