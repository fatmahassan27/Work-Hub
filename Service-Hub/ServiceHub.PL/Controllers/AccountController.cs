using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Enums;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the email is already taken
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return BadRequest(ModelState);
                }

                var user = new ApplicationUser
                {
                    UserName = model.FullName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role.ToString());

                    if (model.Role == Role.Worker)
                    {
                        user.JobId = model.JobId;
                        user.DistrictId = model.DistrictId;
                        await userManager.UpdateAsync(user);
                    }

                    logger.LogInformation($"User {user.UserName} registered successfully as {model.Role}");

                    return Ok(new { message = $"{model.Role} registered successfully" });
                }
                else
                {
                    logger.LogWarning($"User registration failed for {user.UserName}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while registering a user");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing request: {ex.Message}");
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(loginDTO.Email);
                    if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
                    {
                        logger.LogWarning($"Login failed for email: {loginDTO.Email}");
                        return Unauthorized();
                    }
                    var token = await GenerateJwtToken(user);
                    logger.LogInformation($"User {user.UserName} logged in successfully");
                    return Ok( token );
                }
                logger.LogWarning("Invalid model state during login");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while logging in a user");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.UserName),
                new Claim("role", roles.FirstOrDefault() ?? string.Empty),
                new Claim("email", user.Email)
            };

            // Adding other claims if necessary
            if (user.DistrictId.HasValue)
            {
                claims.Add(new Claim("districtId", user.DistrictId.ToString()));
            }

            if (user.JobId.HasValue)
            {
                claims.Add(new Claim("jobId", user.JobId.ToString()));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));
            claims.Add(new Claim(ClaimTypes.Email, user.Email!));
            claims.Add(new Claim(ClaimTypes.Role, string.Join(',', roles)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:ValidIssuer"],
                audience: configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
