using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories
{
    public class BlogEntryCategoryRepository : IBlogEntryCategoryRepository
    {
        private readonly BlogDbContext _context;

        public BlogEntryCategoryRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlogEntryCategory blogEntryCategory)
        {
            _context.BlogEntryCategories.Add(blogEntryCategory);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int blogEntryId, int categoryId)
        {
            var entity = await _context.BlogEntryCategories
                .FirstOrDefaultAsync(bc => bc.BlogEntryId == blogEntryId && bc.CategoryId == categoryId);

            if (entity != null)
            {
                _context.BlogEntryCategories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogEntryCategory>> GetByBlogEntryIdAsync(int blogEntryId)
        {
            return await _context.BlogEntryCategories
                .Where(bc => bc.BlogEntryId == blogEntryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogEntryCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.BlogEntryCategories
                .Where(bc => bc.CategoryId == categoryId)
                .ToListAsync();
        }
    }

}
