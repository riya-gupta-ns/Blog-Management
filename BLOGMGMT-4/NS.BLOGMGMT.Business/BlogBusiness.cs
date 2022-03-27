using NS.BLOGMGMT.Repository;
using NS.BLOGMGMT.Data.Entities;
using NS.BLOGMGMT.Model;

namespace NS.BLOGMGMT.Business
{
  public class BlogBusiness: IBlogBusiness
  {
    private readonly IBlogRepository _iBlogRepository;

    public BlogBusiness(IBlogRepository iBlogRepository)
    {
       _iBlogRepository = iBlogRepository;
    }

    public BlogAndLoginModel GetAllBlogs() 
    {
      return _iBlogRepository.GetAllBlogs();
    }

    public Blog GetBlogById(int id)
    {
      return _iBlogRepository.GetBlogById(id);
    }

    public bool EditBlog(Blog blog)
    {
      return _iBlogRepository.EditBlog(blog);
    }

    public bool CreateBlog(BlogModel blogModel, string userId)
    {
        return _iBlogRepository.CreateBlog(blogModel, userId);
    }

    public bool DeleteBlog(int id)
    {
      return _iBlogRepository.DeleteBlog(id);
    }

    public bool Comment(CommentModel commentModel)
    {
      return _iBlogRepository.Comment(commentModel);
    }

    public List<BlogType> BlogTypes()
    {
      return _iBlogRepository.BlogTypes();
    }

  }
}
