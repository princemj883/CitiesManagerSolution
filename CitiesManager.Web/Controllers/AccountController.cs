using CitiesManager.Core.DTO;
using CitiesManager.Core.Enums;
using CitiesManager.Core.Identity;
using CitiesManager.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CitiesManager.Web.Controllers;

[Route("[controller]/[action]")]
[AllowAnonymous]
public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                RoleManager<ApplicationRole> roleManager, JwtService jwtService) : ControllerBase 
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        if(ModelState.IsValid == false)
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        
        ApplicationUser user = new ApplicationUser
        {
            UserName = registerDTO.Email,
            Email = registerDTO.Email,
            PhoneNumber =  registerDTO.Phone,
            PersonName = registerDTO.PersonName
        };
        IdentityResult result = await userManager.CreateAsync(user, registerDTO.Password);
        if (result.Succeeded)
        {
            if (registerDTO.UserType == UserTypeOptions.Admin)
            {
                //Create Admin Role
                if (await roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole()
                    {
                        Name = UserTypeOptions.Admin.ToString() 
                    };
                    await roleManager.CreateAsync(applicationRole);
                }
                // Add new user to Admin Role
                await userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
            }   
            
            else
            {
                // Add new user to User Role
                await userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
            }
            await signInManager.SignInAsync(user, isPersistent: false);
            var authenticationResponse = jwtService.CreateJwtToken(user);
            return Ok(authenticationResponse);
        }

        foreach (IdentityError error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(result.Errors.Select(e => e.Description));
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

        var result = await signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false,
            lockoutOnFailure: false);
        if (result.Succeeded)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return NoContent();
        } 
        return Ok("Login successful");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok("Logged out successfully");
    }
}