using System.ComponentModel.DataAnnotations;

namespace NS.BLOGMGMT.Data.Entities;

public partial class Blog
{
    public Blog()
    {
        Comments = new HashSet<Comment>();
    }

    public long BlogId { get; set; }
    public long? UserId { get; set; }

    [Display(Name = "Blog Title")]
    [Required(ErrorMessage = "Field is Required")]
    [StringLength(99, MinimumLength = 4, ErrorMessage = "Title should be between 4 and 99 characters")]
    public string? BlogTitle { get; set; }

    [Display(Name = "Write Your Blog Content")]
    [Required(ErrorMessage = "Field is Required")]
    public string? BlogContent { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public bool? IsDeleted { get; set; }

    public int? IsPublish { get; set; }

    public DateTime? DeletedOn { get; set; }

    [Display(Name = "Blog Subject")]
    [Required(ErrorMessage = "Please Select Blog Subject")]
    public int? BlogTypeId { get; set; }

    public virtual BlogType? BlogType { get; set; }
    public virtual User? User { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}
