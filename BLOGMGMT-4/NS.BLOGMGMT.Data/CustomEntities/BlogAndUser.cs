using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Data.CustomEntities;

public partial class BlogAndUser
{
    public BlogAndUser()
    {
      Comments = new HashSet<Comment>();
    }

    public long BlogId { get; set; }
    public long? UserId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogContent { get; set; }
    public int? BlogTypeId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool? IsDeleted { get; set; }
    public int? IsPublish { get; set; }
    public DateTime? DeletedOn { get; set; }
    
    public virtual User? User { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }


    public long Id { get; set; }
    public string? UserFullName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? Gender { get; set; }
    public string? Email { get; set; }
    public string? UsersPassword { get; set; }
    public int? UserRole { get; set; }
    public DateTime? UserCreatedOn { get; set; }
    public bool? UserIsDeleted { get; set; }
    public DateTime? UserLastModifiedOn { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}
