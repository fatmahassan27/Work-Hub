using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UnitWork unitOfWork;

        public AccountController(UnitWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("Register")]
        public IActionResult Registration(RegistrationDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = userDTO.Email,
                    Email = userDTO.Email,
                    PasswordHash = userDTO.Password,

                };
                //unitOfWork.AppUserRepo.Create(appUser);
                //unitOfWork.saveChanges();
                //return Created();
            }
            return BadRequest(ModelState);

        }
        [HttpPost("Login")]

        public IActionResult Login(LoginDTO userlogin)
        {
            var user = unitOfWork.ServiceRepository.FindEmail(userlogin.Email);
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
                return Ok(finalToken);

                #endregion
                return Ok();
            }
            else
                return Unauthorized();

        }

    }
}
