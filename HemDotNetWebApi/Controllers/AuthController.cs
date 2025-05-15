using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HemDotNetWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<RealEstateAgent> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<RealEstateAgent> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            this._userManager = userManager;   
            this._context = context;
            this.configuration = configuration;
        }

        /* Coder: Allan, Participants: All */
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RealEstateAgentRegisterDto registerDto)
        {
            try
            {
                var hasher = new PasswordHasher<RealEstateAgent>();

                var agencies = await _context.RealEstateAgencies.ToListAsync();
                //var agency = agencies.First(a => a.RealEstateAgencyId == registerDto.RealEstateAgencyId);
                
                // When users register, they are instead put on a wait list. It is admin
                // who later can add them to the correct real estate agency.
                var agency = agencies.First(a => a.RealEstateAgencyName == "Wait list");

                RealEstateAgent user = new RealEstateAgent()
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    RealEstateAgentFirstName = registerDto.FirstName,
                    RealEstateAgentLastName = registerDto.LastName,
                    RealEstateAgentPhoneNumber = registerDto.PhoneNumber,
                    NormalizedEmail = registerDto.Email.ToUpperInvariant(),
                    NormalizedUserName = registerDto.Email.ToUpperInvariant(),
                    EmailConfirmed = true,
                    RealEstateAgentEmail = registerDto.Email,
                    RealEstateAgentImageUrl = "Images/DefaultProfilePicture.png",
                    RealEstateAgentAgency = agency
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, ApiRoles.User);
                return Accepted();
            }
            catch(Exception ex)
            {
                return Problem($"Something went wrong in the {nameof(Register)} {ex.InnerException.Message}", statusCode: 500);
            }
            
        }

        /* Coder: Christian, Participants: All */
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto userDto)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);

                var passwordValid = await _userManager.CheckPasswordAsync(user, userDto.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                if(passwordValid == false)
                {
                    return Unauthorized();
                }

                var waitList = await _context.RealEstateAgencies
                    .FirstOrDefaultAsync(a => a.RealEstateAgencyName == "Wait list");

                if (user.RealEstateAgentAgencyId == waitList.RealEstateAgencyId)
                    return Problem($"Administratören måste först godkänna det här kontot.", statusCode: 403);


                string tokenString = await GenerateToken(user);

                var response = new AuthResponse
                {
                    Email = userDto.Email,
                    Token = tokenString,
                    UserId = user.Id
                };

                //return Accepted(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong with {nameof(Login)}", statusCode: 500);
            }
        }

        // Coder: Allan, Participants: All
        private async Task<string> GenerateToken(RealEstateAgent? user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
