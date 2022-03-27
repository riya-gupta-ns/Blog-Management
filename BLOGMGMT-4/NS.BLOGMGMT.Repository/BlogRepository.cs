using System.Text;
using System.Net.Mail;
using NS.BLOGMGMT.Model;
using Microsoft.Data.SqlClient;
using NS.BLOGMGMT.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace NS.BLOGMGMT.Repository
{
  public class BlogRepository: IBlogRepository
  {

    // Get All Blogs
    public BlogAndLoginModel GetAllBlogs()
    {
      using ( var context = new BlogDBContext())
      {
        var blogAndUsers = context.BlogAndUsers.FromSqlRaw("UspUserBlog").ToList();
        BlogAndLoginModel blogAndLoginModel = new BlogAndLoginModel();
        blogAndLoginModel.blogAndUsers = blogAndUsers;
        return blogAndLoginModel;
      }
    }

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
        //var blogModel = context.Blogs.Where(x => x.UserId == long.Parse(userId)).ToList();
        var commentModel = context.Comments.Where( x => x.BlogId == blogId).ToList();
        return commentModel;
      }
      
    }

    // Edit Blog
    public bool EditBlog(Blog blog)
    {
      try {
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
      } catch ( Exception ex)
      {
        return false;
      }
    }

    // Create Blog
    public bool CreateBlog(BlogModel blogModel, string userId)
    {
      using (var context = new BlogDBContext())
      {
          var paraamList = new List<SqlParameter>();
          paraamList.Add(new SqlParameter("@UserId", userId));
          paraamList.Add(new SqlParameter("@BlogTitle", blogModel.BlogTitle));
          paraamList.Add(new SqlParameter("@BlogContent", blogModel.BlogContent));
          paraamList.Add(new SqlParameter("@BlogTypeId", blogModel.BlogTypeId));
          paraamList.Add(new SqlParameter("@CreatedOn", DateTime.Now));
          paraamList.Add(new SqlParameter("@LastModifiedOn", DateTime.Now));
          context.Database.ExecuteSqlRaw("UspInsertIntoBlogs @UserId, @BlogTitle, @BlogContent, @BlogTypeId, @CreatedOn, @LastModifiedOn", paraamList);

          string from = "webseries09876@gmail.com"; //From address
          MailMessage message = new MailMessage();
          message.From = new MailAddress(from);
         // message.To.Add(res.Email);
          message.To.Add(from);
          string mailbody = "New Blog Posted Check it out !!!";
          message.Subject = "New Blog Post";
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

    public bool DeleteBlog(int id)
    {
      try {
        using (var context = new BlogDBContext())
        {
          var paraamList = new List<SqlParameter>();
          paraamList.Add(new SqlParameter("@BlogId", id));
          paraamList.Add(new SqlParameter("@DeletedOn", DateTime.Now));     
          context.Database.ExecuteSqlRaw("UspDeleteBlog @BlogId, @DeletedOn", paraamList);
        }
        return true;
      } catch ( Exception ex)
      {
        return false;
      }
    }

    public bool Comment(CommentModel commentModel)
    {
      using (var context = new BlogDBContext()) {
         var paraamList = new List<SqlParameter>();
          paraamList.Add(new SqlParameter("@BlogId", commentModel.BlogId));
          paraamList.Add(new SqlParameter("@CommentContent", commentModel.CommentContent));
          paraamList.Add(new SqlParameter("@CreatedOn", DateTime.Now));
          paraamList.Add(new SqlParameter("@LastModifiedOn", DateTime.Now));
          paraamList.Add(new SqlParameter("@Name", commentModel.Name));
          context.Database.ExecuteSqlRaw("UspInsertIntoComments @BlogId, @CommentContent, @CreatedOn, @LastModifiedOn, @Name", paraamList);

          var blog = context.Blogs.Where(x => x.BlogId == commentModel.BlogId ).FirstOrDefault();
          var user = context.Users.Where( x => x.UserId == blog.UserId ).FirstOrDefault();
          
      }
      return true;
    }
  
    public List<BlogType> BlogTypes()
    {
      using ( var context = new BlogDBContext() )
      {
        var blogType = from bt in context.BlogTypes
                       where bt.IsDeleted == false
                       select bt;
        return blogType.ToList();
      }

    }
  }
}
