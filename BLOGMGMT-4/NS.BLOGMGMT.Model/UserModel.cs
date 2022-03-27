using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NS.BLOGMGMT.Model;

public partial class UserModel
{
  [Key]
  public long UserId { get; set; }

  [Display(Name = "Full Name")]
  [Required(ErrorMessage = "Please enter your Full Name")]
  [StringLength(50, MinimumLength = 4, ErrorMessage = "Name should be between 4 and 50 characters")]
  [RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -)") ]
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

  [Display(Name = "Gender")]
  [Required(ErrorMessage = "Please select one")]
  public Gender gender { get; set; }

  [EmailAddress]
  [Display(Name = "Email Address")]
  [Required(ErrorMessage = "Please enter email address")]
  public string? Email { get; set; }

  [Display(Name = "Enter Password")]
  [Required(ErrorMessage = "Please enter password")]
  public string? Password { get; set; }

  public int? Role { get; set; }

  public DateTime? CreatedOn { get; set; }

  public bool? IsDeleted { get; set; }

  public enum Gender
  {
    [Description("Male")]
    Male = 0,
    Female = 1,
  }
}
