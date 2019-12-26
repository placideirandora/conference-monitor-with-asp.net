using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConferenceMonitorApi.Data;
using CryptoHelper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using System;

namespace ConferenceMonitorApi.Controllers
{
    // Base route
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;

        // Construct fields for accessing the repositories
        public AuthController(IUserRepository userRepository, IAuthRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        // Handle POST request of user registration
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                user.ConfirmPassword = Crypto.HashPassword(user.ConfirmPassword);

                try
                {
                    await _authRepository.RegisterAsync(user);

                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                }
                catch (DbUpdateException)
                {
                    return Conflict(new { message = $"User with Id {user.Id} already exists" });
                }
            }
            else
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
        }

        // Handle POST request of user login
        [HttpPost, Route("SignIn")]
        public async Task<ActionResult<User>> LogUserIn([FromBody] SignIn user)
        {
            var result = await _authRepository.AuthenticateAsync(user.Email, user.Password);

            if (result == null) return Unauthorized(new { message = $"Incorrect Email: {user.Email}" });

            var verifyPassword = Crypto.VerifyHashedPassword(result.Password, user.Password);

            if (!verifyPassword) return Unauthorized(new { message = "Incorrect Password" });

            // Define and encode the security key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s3cR3t!123K1y!&s3cR3t!123K1y!"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Define roles
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

            // Define JWT required options
            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:4000",
                audience: "http://localhost:4000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            
            // Generate JWT token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { message = "Signed In", Token = tokenString });
        }

        // Return a registered user
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.FindByIdAsync<User>(id);

            return Ok(new { message = "User Registered", user });
        }
    }
}