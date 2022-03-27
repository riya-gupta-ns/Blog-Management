using System.ComponentModel.DataAnnotations;

namespace NS.BLOGMGMT.Model;

public class Login {

  [Display(Name = "User Name")]
  [Required(ErrorMessage = "Please enter name")]
  [StringLength(50, MinimumLength = 4)]
  public string? UserName { get; set; }

  [Required(ErrorMessage = "Please enter password")]
  public string? Password { get; set; }
}
