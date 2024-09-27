using BlogApp.DTOs;
using BlogApp.Models;

namespace BlogApp.Services.Interfaces
{
    public interface IBlogEntryService
    {
        Task<IEnumerable<BlogEntryDTO>> GetAllBlogEntriesAsync();
        Task<IEnumerable<BlogEntryDTO>> GetAllUserBlogEntriesAsync(int userId);
        Task<BlogEntry> GetBlogEntryByIdAsync(int id);
        Task<BlogEntryDTO> CreateBlogEntryAsync(BlogEntryDTO blogEntry);
        Task UpdateBlogEntryAsync(BlogEntryDTO blogEntry);
        Task DeleteBlogEntryAsync(int id);
        Task AssociateCategoryAsync(int blogEntryId, int categoryId);
        Task DisassociateCategoryAsync(int blogEntryId, int categoryId);
        Task<IEnumerable<CategoryDTO>> GetCategoriesByBlogEntryIdAsync(int blogEntryId);
    }
}
