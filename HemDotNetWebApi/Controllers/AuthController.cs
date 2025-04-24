using HemDotNetWebApi.Constants;
using HemDotNetWebApi.Data;
using HemDotNetWebApi.DTO;
using HemDotNetWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HemDotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<RealEstateAgent> _userManager;
        private readonly ApplicationDbContext _context;
        public AuthController(UserManager<RealEstateAgent> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;   
            this._context = context;
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
                var agency = agencies.First(a => a.RealEstateAgencyId == registerDto.RealEstateAgencyId);

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
                    RealEstateAgentImageUrl = "/images/RealEstateAgentWoman.jpg",
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
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
            
        }
    }
}
