using System.ComponentModel.DataAnnotations;

namespace NS.BLOGMGMT.Model;

public class BlogModel
{
  [Key]
  public long BlogId { get; set; }

  public long? UserId { get; set; }

  [Display(Name="Blog Title")]
  [Required(ErrorMessage = "Please enter Blog Title")]
  [StringLength(99, MinimumLength = 4, ErrorMessage = "Title should be between 4 and 99 characters")]
  public string? BlogTitle { get; set; }

  [Display(Name = "Write Your Blog Content")]
  [Required(ErrorMessage = "Please enter Blog Content")]
  public string? BlogContent { get; set;}

  [Display(Name = "Blog Subject")]
  [Required(ErrorMessage = "Please Select Blog Subject")]
  public int? BlogTypeId { get; set; }

  public long? CommentId { get; set; }

  public DateTime? CreatedOn { get; set; }

  public DateTime? LastModifiedOn { get; set; }

  public bool? IsDeleted { get; set; }
  
  public bool? IsPublish { get; set; }
}
