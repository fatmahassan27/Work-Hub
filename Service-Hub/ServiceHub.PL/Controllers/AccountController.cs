using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTO;
using ServiceHub.DAL.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceHub.PL.Controllers
{//
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        //public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = new ApplicationUser { UserName = model.FullName, Email = model.Email, DistrictId = model.DistrictId };

        //            var result = await userManager.CreateAsync(user, model.Password);

        //            if (result.Succeeded)
        //            {
        //                var roleExists = await roleManager.RoleExistsAsync(model.RoleName);
        //                if (!roleExists)
        //                {
        //                    var roleResult = await roleManager.CreateAsync(new IdentityRole<int>(model.RoleName));
        //                    if (!roleResult.Succeeded)
        //                    {
        //                        return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating role: {roleResult.Errors.FirstOrDefault()?.Description}");
        //                    }

        //                }
        //                var addToRoleResult = await userManager.AddToRoleAsync(user, model.RoleName);
        //                if (!addToRoleResult.Succeeded)
        //                {
        //                    return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding user to role: {addToRoleResult.Errors.FirstOrDefault()?.Description}");
        //                }
        //                return Ok("User registered successfully");
        //            }
        //            return BadRequest(result.Errors.FirstOrDefault()?.Description);

        //        }
        //        return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data : {ex}");

        //    }

        //}
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.FullName, Email = model.Email };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, model.Role.ToString());

                        return Ok("User registered successfully");
                    }

                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing request: {ex.Message}");
            }
        }




        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userlog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(userlog.Email);
                    if (user != null)
                    {
                        bool found = await userManager.CheckPasswordAsync(user, userlog.Password);
                        if (found)
                        {
                            List<Claim> userData =
                            [
                                new Claim(ClaimTypes.Name, user.UserName),
                                new Claim(ClaimTypes.Role,string.Join(',', (await userManager.GetRolesAsync(user)))),
                            ];

                            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                            SigningCredentials Signcer = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            //create Token
                            var token = new JwtSecurityToken(
                                issuer: configuration["JWT:ValidIssuer"],
                                audience: configuration["JWT:ValidAudiance"],
                                 claims: userData,
                                 expires: DateTime.Now.AddDays(1),
                                 signingCredentials: Signcer
                            );

                            return Ok(new
                            {
                                stringToken = new JwtSecurityTokenHandler().WriteToken(token),
                                expairation = token.ValidTo
                            });

                        }
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data : {ex}");

            }
        }
    }
}
