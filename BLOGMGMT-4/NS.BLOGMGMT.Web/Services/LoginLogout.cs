using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NS.BLOGMGMT.Data.Entities;

namespace NS.BLOGMGMT.Web.Services;

public class LoginLogout 
{
  private readonly HttpContext _httpContext;

  public LoginLogout(HttpContext httpContext)
  {
    _httpContext = httpContext;
  }

  public async Task<bool> CreateClaims(User user)
  {
    if ( user != null ) {
      var claims = new List <Claim> () {  
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),  
        new Claim(ClaimTypes.Name, user.UserName),  
        new Claim(ClaimTypes.Role, user.Role.ToString()),  
      };  

      //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);  

      //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
      var principal = new ClaimsPrincipal(identity);  

      await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);  
      return true;
    }
    return false;

  }

  public async Task <bool> LogOut() {  
    await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  
    return true;
  }

}
