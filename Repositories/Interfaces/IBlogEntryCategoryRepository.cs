using BlogApp.Models;

namespace BlogApp.Repositories.Interfaces
{
    public interface IBlogEntryCategoryRepository
    {
        Task AddAsync(BlogEntryCategory blogEntryCategory);
        Task RemoveAsync(int blogEntryId, int categoryId);
        Task<IEnumerable<BlogEntryCategory>> GetByBlogEntryIdAsync(int blogEntryId);
        Task<IEnumerable<BlogEntryCategory>> GetByCategoryIdAsync(int categoryId);
    }

}
