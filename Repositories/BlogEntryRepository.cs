using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Repositories
{
    public class BlogEntryRepository : IRepository<BlogEntry>
    {
        private readonly BlogDbContext _context;

        public BlogEntryRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogEntry>> GetAllAsync()
        {
            return await _context.BlogEntries.ToListAsync();
        }

        public async Task<BlogEntry> GetByIdAsync(int id)
        {
            return await _context.BlogEntries.FindAsync(id);
        }

        public async Task AddAsync(BlogEntry blogEntry)
        {
            await _context.BlogEntries.AddAsync(blogEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogEntry blogEntry)
        {
            _context.BlogEntries.Update(blogEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogEntry blogEntry)
        {
            _context.BlogEntries.Remove(blogEntry);
            await _context.SaveChangesAsync();
        }
    }
}
