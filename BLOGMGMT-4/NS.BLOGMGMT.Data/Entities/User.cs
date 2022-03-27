using System.ComponentModel.DataAnnotations;

namespace NS.BLOGMGMT.Data.Entities;

public partial class User
{
    public User()
    {
        Blogs = new HashSet<Blog>();
    }

    [Key]
    public long UserId { get; set; }

    public string? UserFullName { get; set; }

    [Display(Name = "User Name")]
    [Required(ErrorMessage = "Please enter your User Name")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "User Name should be between 4 and 50 characters")]
    public string? UserName { get; set; }

    [Phone]
    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Please enter phone number")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter without any country code")]
    public string? PhoneNumber { get; set; }

    public bool? Gender { get; set; }

    [EmailAddress]
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Please enter email address")]
    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Role { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; }
}

