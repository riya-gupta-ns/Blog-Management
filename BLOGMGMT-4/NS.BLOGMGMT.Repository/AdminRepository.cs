using System.Text;
using System.Net.Mail;
using NS.BLOGMGMT.Model;
using Microsoft.Data.SqlClient;
using NS.BLOGMGMT.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NS.BLOGMGMT.Data.CustomEntities;

namespace NS.BLOGMGMT.Repository;

public class AdminRepository : IAdminRepository 
{

  // Get Blog By Id
  public Blog GetBlogById(int id)
  {
    var context = new BlogDBContext();
    var result = context.Blogs.FromSqlRaw("UspGetBlogsById @Id", new SqlParameter("@Id", id)).ToList().FirstOrDefault();
    result.Comments = GetComment(id);
    return result;
  }

  // Get comments
  public List<Comment> GetComment(int blogId)
  {
    using (var context = new BlogDBContext())
    {
      var commentModel = context.Comments.Where( x => x.BlogId == blogId).ToList();
      return commentModel;
    }
    
  }

  private string EncryptPassword(string password)
  {
      string pswstr = string.Empty;
      byte[] pswEncode = new byte[password.Length];
      pswEncode = System.Text.Encoding.UTF8.GetBytes(password);
      pswstr = Convert.ToBase64String(pswEncode);
      return pswstr;
  }

  public User Login(Login userObject) 
  {
    using (var context = new BlogDBContext())
    {
      var  user = context.Users.Where(x => x.UserName == userObject.UserName && x.Password == userObject.Password && x.Role == 0).FirstOrDefault();
      if ( user == null )
        return null;
      return user;
    }  
  }

  public List<BlogAndUser> GetBlogAndUser() 
  {
    using ( var context = new BlogDBContext()) {
      var blogAndUser = context.BlogAndUsers.FromSqlRaw("UspAdminUserBlog").ToList();
      return blogAndUser;
    }
  }

  // Edit Blog
  public bool EditBlog(Blog blog)
  {
    using (var context = new BlogDBContext())
    {
      var paraamList = new List<SqlParameter>();
      paraamList.Add(new SqlParameter("@BlogId", blog.BlogId));
      paraamList.Add(new SqlParameter("@BlogTitle", blog.BlogTitle));
      paraamList.Add(new SqlParameter("@BlogTypeId", blog.BlogTypeId));
      paraamList.Add(new SqlParameter("@BlogContent", blog.BlogContent));
      paraamList.Add(new SqlParameter("@LastModifiedOn", DateTime.Now));
      context.Database.ExecuteSqlRaw("UspUpdateBlog @BlogId,@BlogTitle, @BlogTypeId, @BlogContent, @LastModifiedOn", paraamList);
    }
    return true;
  }

  // Publish Blog
  public bool PublishBlog(int id)
  {
    using ( var context = new BlogDBContext() )
    {
      var paraamList = new List<SqlParameter>();
      paraamList.Add(new SqlParameter("@BlogId", id));
      context.Database.ExecuteSqlRaw("UspPublishBlog @BlogId",paraamList);

      var blog = context.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
      var user = context.Users.Where(x => x.UserId == blog.UserId).FirstOrDefault();

      string from = "webseries09876@gmail.com"; //From address
      MailMessage message = new MailMessage();
      message.From = new MailAddress(from);
      // message.To.Add(res.Email);
      message.To.Add(user.Email);
      string mailbody = blog.BlogTitle + " " + ":-" + " " + "Blog Approved Check it out !!!";
      message.Subject = "Blog Approved";
      message.Body = mailbody;
      message.BodyEncoding = Encoding.UTF8;
      message.IsBodyHtml = true;
      SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
      System.Net.NetworkCredential basicCredential1 = new
      System.Net.NetworkCredential("webseries09876@gmail.com", "Riya@1999");
      client.EnableSsl = true;
      client.UseDefaultCredentials = false;
      client.Credentials = basicCredential1;
      try
      {
          client.Send(message);
      }
      catch (Exception ex)
      {
          throw ex;
      }
    }
    return true;
  }

  // Reject Blog
  public bool RejectBlog(int id)
  {
    using ( var context = new BlogDBContext() )
    {
      var paraamList = new List<SqlParameter>();
      paraamList.Add(new SqlParameter("@BlogId", id));
      context.Database.ExecuteSqlRaw("UspRejectBlog @BlogId",paraamList);
            var blog = context.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var user = context.Users.Where(x => x.UserId == blog.UserId).FirstOrDefault();
            string from = "webseries09876@gmail.com"; //From address
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            // message.To.Add(res.Email);
            message.To.Add(user.Email);
            string mailbody = blog.BlogTitle +" " + ":-" + " " + "Blog Rejected Sorry Better luck next time !!!";
            message.Subject = "Blog Rejected";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("webseries09876@gmail.com", "Riya@1999");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    return true;
  }

  //public bool RejectBlog(int id)
  //{
  //  using ( var context = new BlogDBContext() )
  //  {
  //    var paraamList = new List<SqlParameter>();
  //    paraamList.Add(new SqlParameter("@BlogId", id));
  //    context.Database.ExecuteSqlRaw("UspRejectBlog @BlogId",paraamList);
  //  }
  //  return true;
  //}

  public List<BlogType> BlogTypes()
  {
    var context = new BlogDBContext();
    return context.BlogTypes.ToList();
  }

  public bool DeleteBlog(int id)
  {
    using (var context = new BlogDBContext())
    {
      var paraamList = new List<SqlParameter>();
      paraamList.Add(new SqlParameter("@BlogId", id));
      paraamList.Add(new SqlParameter("@DeletedOn", DateTime.Now));     
      context.Database.ExecuteSqlRaw("UspDeleteBlog @BlogId, @DeletedOn", paraamList);
    }
    return true;
  }

  public List<User> GetAllUsers()
  {
    using ( var context = new BlogDBContext())
    {
      var users = from u in context.Users
                  where u.Role == 1 
                  select u;
      return users.ToList();
    }
  }

  public bool AddBlogType(BlogType blogType)
  {
    using( var context = new BlogDBContext())
    {
      var paraamList = new List<SqlParameter>();
      paraamList.Add(new SqlParameter("@TypeName", blogType.TypeName));
      context.Database.ExecuteSqlRaw("UspInsertIntoBlogType @TypeName", paraamList);
    }
    return true;
  }

  public List<BlogType> GetBlogTypes()
  {
    using ( var context = new BlogDBContext() )
    {
      var blogTypes = from bt in context.BlogTypes
                      select bt;
      return blogTypes.ToList();
    }
  }

  public bool DeleteBlogType(int id)
  {
    using(var context = new BlogDBContext())
    {
      var blogTypes = (from bt in context.BlogTypes
                      where bt.TypeId == id 
                      select bt).FirstOrDefault();
      blogTypes.IsDeleted = true; 

      var blogs = ( from b in context.Blogs
                   where b.BlogTypeId == id
                   select b
                 ).ToList();

      foreach( Blog blog in blogs )
      {
        blog.IsDeleted = true;
      }

      context.SaveChanges();
    }
    return true;
  }

  public bool ActivateBlogType(int id)
  {
    using(var context = new BlogDBContext())
    {
      var blogTypes = (from bt in context.BlogTypes
                      where bt.TypeId == id 
                      select bt).FirstOrDefault();
      blogTypes.IsDeleted = false; 

      var blogs = ( from b in context.Blogs
                   where b.BlogTypeId == id
                   select b
                 ).ToList();
      foreach( Blog blog in blogs )
      {
        blog.IsDeleted = false;
      }

      context.SaveChanges();
    }
    return true;

  }

  public User ViewUser(string userId)
  {
    using ( var context = new BlogDBContext())
    {
      var user = context.Users.Where(x => x.UserId == long.Parse(userId)).FirstOrDefault();
      return user;
    }
  }

  public List<StaticContent> ContactUs()
  {
    using ( var context = new BlogDBContext() )
    {
      var staticContent = (from s in context.StaticContents
                          select s).ToList();
      return staticContent;
    }
  }

  public bool EditContactUs(List<StaticContent> staticContents)
  {
    using ( var context = new BlogDBContext())
    {
      var staticContent = ( from s in context.StaticContents 
                            select s).ToList();
      foreach( StaticContent item in staticContents )
      {
        context.Database.ExecuteSqlRaw("UspUpdateStaticContent @Content", new SqlParameter("@Content", item.Content) );
      }
    }
    return true;
  }

  public bool ActivateUser(int id)
  {
    using(var context = new BlogDBContext())
    {
      var user = ( from u in context.Users
                   where u.UserId == id
                   select u
                 ).FirstOrDefault();
      user.IsDeleted = false;
      context.SaveChanges();
    }
    return true;
  }

  public bool DeActivateUser(int id)
  {
    using(var context = new BlogDBContext())
    {
      var user = ( from u in context.Users
                   where u.UserId == id
                   select u
                 ).FirstOrDefault();
      user.IsDeleted = true;
      context.SaveChanges();
    }
    return true;
  }
}
