using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;
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
                User appUser = new User()
                {
                    FullName = userDTO.Email,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                };
                unitOfWork.UserRepo.Create(appUser);
                unitOfWork.saveChanges();
                return Created();
            }
            return BadRequest(ModelState);

        }
        [HttpPost("Login")]

        public IActionResult Login(LoginDTO userlogin)
        {
            var user = unitOfWork.ServiceRepository.FindEmail(userlogin.Email);
            if (user != null && user.Password == userlogin.Password)
            {
                #region Claims
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Role,""));
               // userdata.Add(new Claim(User.Id, "user.Id"));
                userdata.Add(new Claim(ClaimTypes.Name,user.FullName));
                //userdata.Add(new Claim(ClaimTypes.Role,"worker"));
                #endregion


                #region singing
                string key = " Welcome To My SecretKey Fatma Hassan";
                var SecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var signingcer = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                #endregion
                #region generate token
                //hashing algorithm
                //playRole=> claims,expiredate
                ////signture =>secertkey
                var token = new JwtSecurityToken(
                            claims : userdata,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: signingcer
                );
                //token object =>encoding string
                var tokenObjHand = new JwtSecurityTokenHandler();
                var finalToken  =tokenObjHand.WriteToken(token);
                return Ok(finalToken);

                #endregion
                return Ok();
            }
            else
                return Unauthorized();
            
        }
      
    }
}
