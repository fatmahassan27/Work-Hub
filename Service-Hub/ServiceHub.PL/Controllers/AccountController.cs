using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
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
        private readonly UnitWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        // user manager CRUD login register
        // , store manager , role manager , store manager
        
        public AccountController(UnitWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        //logging serilog try catch

        [HttpPost("Register")]
        public async Task<IActionResult> Registration(RegistrationDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = userDTO.FullName,
                    Email = userDTO.Email,
                    PasswordHash = userDTO.Password,

                };
                IdentityResult result = await userManager.CreateAsync(appUser,appUser.PasswordHash);
                if (result.Succeeded)
                {
                    return Created(); //
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("error", error.Description);
                    }
                    return BadRequest(ModelState);
                }
                
                //unitOfWork.AppUserRepo.Create(appUser);
                //unitOfWork.saveChanges();
                //return Created();

            }
            return BadRequest(ModelState);

        }
        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDTO userlogin)
        {
            var user = await userManager.FindByEmailAsync(userlogin.Email);
            if (user != null && user.PasswordHash == userlogin.Password)
            {
                #region Claims

                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim("Role","user")) ;
                userdata.Add(new Claim(ClaimTypes.Email,userlogin.Email));
                //userdata.Add(new Claim(ClaimTypes.Name, user.UserName));
                //userdata.Add(new Claim(ClaimTypes.UserData, user.FullName));

                #endregion

                #region singing
                string key = " Welcome To My SecretKey Fatma Hassan";
                var SecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var signingcer = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                #endregion

                #region generate token
              //  hashing algorithm
              //  playRole => claims,expiredate
                //signture =>secertkey
                var token = new JwtSecurityToken(
                            claims: userdata,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: signingcer
                );
                //token object =>encoding string
                var tokenObjHand = new JwtSecurityTokenHandler();
                var finalToken = tokenObjHand.WriteToken(token);
                #endregion

                return Ok(finalToken);
            }
            else
                return Unauthorized();
        }
    }
}
