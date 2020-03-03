using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IWrapperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
    }
}