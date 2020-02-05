using System.Collections.Generic;
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
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repo.Category.GetAllCategories();

            var categoriesResult = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesResult);
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryFromRepo = await _repo.Category.GetCategory(id);

            if (categoryFromRepo == null)
            {
                return NotFound($"Category with id {id} does not exist");
            }

            var categoryResult = _mapper.Map<CategoryDto>(categoryFromRepo);

            return Ok(categoryResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryForCreationDto category)
        {
            if (await _repo.Category.CategoryExist(category))
            {
                return BadRequest("Category already exists");
            }

            var categoryEntity = _mapper.Map<Category>(category);

            _repo.Category.CreateCategory(categoryEntity);
            await _repo.Save();

            var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryForUpdateDto category)
        {
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
        public async Task<IActionResult> DeleteCategory(int id)
        {
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