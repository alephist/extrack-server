using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWrapperRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IWrapperRepository repo, IMapper mapper, IConfiguration config)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto user)
        {
            if (await _repo.Auth.UsernameExists(user.Username))
            {
                return BadRequest("Username already exists");
            }

            if (await _repo.Auth.EmailExists(user.Email))
            {
                return BadRequest("Email already exists");
            }

            var userEntity = _mapper.Map<User>(user);
            _repo.Auth.RegisterUser(userEntity, user.Password);
            await _repo.Save();

            var createdUser = _mapper.Map<UserDto>(userEntity);

            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var userFromRepo = await _repo.Auth.LoginUser(user.Email, user.Password);

            if (userFromRepo == null)
            {
                return BadRequest("Invalid email or password");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}