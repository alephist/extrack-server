using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExTrackAPI.Contracts;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public async Task<bool> CategoryExist(CategoryForCreationDto category)
        {
            return await GetByCondition(c => c.Name == category.Name).AnyAsync();
        }

        public async Task<bool> CategoryExist(CategoryForUpdateDto category)
        {
            return await GetByCondition(c => c.Name == category.Name).AnyAsync();
        }

        public void CreateCategory(Category category)
        {
            Create(category);
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesByUser(int userId)
        {
            return await GetByCondition(c => c.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await GetByCondition(c => c.Id == categoryId).FirstOrDefaultAsync();
        }

        public void UpdateCategory(Category category)
        {
            Update(category);
        }
    }
}