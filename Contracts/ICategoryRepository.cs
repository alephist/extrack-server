using System.Collections.Generic;
using System.Threading.Tasks;
using ExTrackAPI.Dto;
using ExTrackAPI.Models;

namespace ExTrackAPI.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategory(int categoryId);
        Task<bool> CategoryExist(CategoryForCreationDto category);
        Task<bool> CategoryExist(CategoryForUpdateDto category);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}