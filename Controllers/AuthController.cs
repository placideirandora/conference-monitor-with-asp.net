using System;
using System.Text;
using CryptoHelper;
using Microsoft.AspNetCore.Mvc;
using ConferenceMonitorApi.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConferenceMonitorApi.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ConferenceMonitorApi.Controllers
{
    // Base route
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        // Construct fields for accessing the repositories and credentials
        public AuthController(IUserRepository userRepository, IAuthRepository authRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// User Registration (public)
        /// </summary>

        // Handle POST request of user registration
        [HttpPost, Route("SignUp")]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                var foundUser = await _authRepository.FindByEmailAsync(user.Email);

                if (foundUser != null) return Conflict(new { message = $"User with Email ({user.Email}) is already registered" });

                user.Password = Crypto.HashPassword(user.Password);
                user.ConfirmPassword = Crypto.HashPassword(user.ConfirmPassword);

                try
                {
                    await _authRepository.RegisterAsync(user);

                    object registeredUser = new { user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.Registered };

                    return StatusCode(201, registeredUser);
                }
                catch (DbUpdateException)
                {
                    return Conflict(new { message = $"User with Id {user.Id} already exists" });
                }
            }
            else
            {
                return BadRequest(new { message = "Validation Errors", ModelState });
            }
        }

        /// <summary>
        /// User Login (public)
        /// </summary>

        // Handle POST request of user login
        [HttpPost, Route("SignIn")]
        public async Task<ActionResult<User>> LogUserIn([FromBody] SignIn user)
        {
            var foundUser = await _authRepository.FindByEmailAsync(user.Email);

            if (foundUser == null) return Unauthorized(new { message = $"Incorrect Email: {user.Email}" });

            var verifyPassword = Crypto.VerifyHashedPassword(foundUser.Password, user.Password);

            if (!verifyPassword) return Unauthorized(new { message = "Incorrect Password" });

            string secretKey = _configuration.GetSection("Credentials").GetSection("SecretKey").Value;
            string issuer = _configuration.GetSection("Credentials").GetSection("Issuer").Value;
            string audience = _configuration.GetSection("Credentials").GetSection("Audience").Value;
    
            SymmetricSecurityKey sSK = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials sC = new SigningCredentials(sSK, SecurityAlgorithms.HmacSha256);

            // Define custom claims
            var claims = new List<Claim>();

            claims.Add(new Claim("UserID", $"{foundUser.Id}"));
            claims.Add(new Claim("UserEmail", $"{foundUser.Email}"));

            // Define JWT required options
            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: sC
            );

            // Generate JWT token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { message = "Signed In", Token = tokenString });
        }
    }
}