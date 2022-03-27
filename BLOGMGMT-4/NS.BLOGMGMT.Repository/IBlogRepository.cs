using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Repository
{
  public interface IBlogRepository
  {
    public BlogAndLoginModel GetAllBlogs();

    public Blog GetBlogById(int id);

    public bool CreateBlog(BlogModel blogModel, string userId);

    public bool EditBlog(Blog blog);
    public bool DeleteBlog(int id);
    public bool Comment(CommentModel commentModel);
    public List<BlogType> BlogTypes();
  }
}
