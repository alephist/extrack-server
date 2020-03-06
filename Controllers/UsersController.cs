using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;

namespace ExTrackAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWrapperRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IWrapperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userFromRepo = await _repo.User.GetUser(id);

            if (userFromRepo == null)
            {
                return NotFound("User not found");
            }

            var userToReturn = _mapper.Map<UserForDetailDto>(userFromRepo);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto user)
        {
            var userFromRepo = await _repo.User.GetUser(id);

            if (userFromRepo == null)
            {
                return NotFound("User does not exist");
            }

            if (await _repo.User.UsernameExist(user))
            {
                return BadRequest("Username already exists");
            }

            if (await _repo.User.EmailExist(user))
            {
                return BadRequest("Email already exists");
            }

            _mapper.Map(user, userFromRepo);
            _repo.User.UpdateUser(userFromRepo);
            await _repo.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userFromRepo = await _repo.User.GetUser(id);

            if (userFromRepo == null)
            {
                return NotFound("User does not exist");
            }

            _repo.User.DeleteUser(userFromRepo);
            await _repo.Save();

            return NoContent();
        }
    }
}