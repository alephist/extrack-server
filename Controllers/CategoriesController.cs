using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IWrapperRepository _repo;
        private readonly IMapper _mapper;

        public CategoriesController(IWrapperRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var categories = await _repo.Category.GetAllCategoriesByUser(userId);

            var categoriesResult = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesResult);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public async Task<IActionResult> GetCategory(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var categoryFromRepo = await _repo.Category.GetCategory(id);

            if (categoryFromRepo == null)
            {
                return NotFound($"Category with id {id} does not exist");
            }

            var categoryResult = _mapper.Map<CategoryDto>(categoryFromRepo);

            return Ok(categoryResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(int userId, CategoryForCreationDto category)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            if (await _repo.Category.CategoryExist(category))
            {
                return BadRequest("Category already exists");
            }

            var categoryEntity = _mapper.Map<Category>(category);
            categoryEntity.UserId = userId;

            _repo.Category.CreateCategory(categoryEntity);
            await _repo.Save();

            var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute(nameof(GetCategory), new { userId = userId, id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int userId, int id, CategoryForUpdateDto category)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var categoryFromRepo = await _repo.Category.GetCategory(id);

            if (categoryFromRepo == null)
            {
                return NotFound($"Category with id {id} does not exist");
            }

            if (await _repo.Category.CategoryExist(category))
            {
                return BadRequest("Category already exists");
            }

            _mapper.Map(category, categoryFromRepo);

            _repo.Category.UpdateCategory(categoryFromRepo);
            await _repo.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var categoryFromRepo = await _repo.Category.GetCategory(id);

            if (categoryFromRepo == null)
            {
                return NotFound("Category does not exist");
            }

            _repo.Category.DeleteCategory(categoryFromRepo);
            await _repo.Save();

            return NoContent();
        }
    }
}