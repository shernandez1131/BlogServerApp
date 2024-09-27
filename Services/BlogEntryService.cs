using AutoMapper;
using BlogApp.Data;
using BlogApp.DTOs;
using BlogApp.Models;
using BlogApp.Repositories;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class BlogEntryService : IBlogEntryService
    {
        private readonly IRepository<BlogEntry> _blogEntryRepository;
        private readonly IBlogEntryCategoryRepository _blogEntryCategoryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly BlogDbContext _context;

        public BlogEntryService(BlogDbContext context, IRepository<BlogEntry> blogEntryRepository, IBlogEntryCategoryRepository blogEntryCategoryRepository, IRepository<Category> categoryRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _blogEntryRepository = blogEntryRepository;
            _blogEntryCategoryRepository = blogEntryCategoryRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<BlogEntryDTO>> GetAllBlogEntriesAsync()
        {
            var blogEntries = await _blogEntryRepository.GetAllAsync();
            var returnBlogEntries = new List<BlogEntryDTO>();
            foreach (var blogEntry in blogEntries)
            {
                var blogEntryDTO = _mapper.Map<BlogEntryDTO>(blogEntry);
                var authorBase = await _userRepository.GetByIdAsync(blogEntry.AuthorId);
                if (authorBase != null)
                {
                    var authorDTO = _mapper.Map<UserDTO>(authorBase);
                    blogEntryDTO.Author = authorDTO;
                }
                var categories = await GetCategoriesByBlogEntryIdAsync(blogEntry.Id);
                blogEntryDTO.Categories = categories;
                returnBlogEntries.Add(blogEntryDTO);
            }
            return returnBlogEntries;
        }
        public async Task<IEnumerable<BlogEntryDTO>> GetAllUserBlogEntriesAsync(int userId)
        {
            var blogEntries = await _context.BlogEntries.Where(x => x.AuthorId == userId).ToListAsync();
            var returnBlogEntries = new List<BlogEntryDTO>();
            foreach (var blogEntry in blogEntries)
            {
                var blogEntryDTO = _mapper.Map<BlogEntryDTO>(blogEntry);
                var authorBase = await _userRepository.GetByIdAsync(userId);
                if (authorBase != null)
                {
                    var authorDTO = _mapper.Map<UserDTO>(authorBase);
                    blogEntryDTO.Author = authorDTO;
                }
                var categories = await GetCategoriesByBlogEntryIdAsync(blogEntry.Id);
                blogEntryDTO.Categories = categories;
                returnBlogEntries.Add(blogEntryDTO);
            }
            return returnBlogEntries;
        }
        public async Task<BlogEntry> GetBlogEntryByIdAsync(int id) => await _blogEntryRepository.GetByIdAsync(id);

        public async Task<BlogEntryDTO> CreateBlogEntryAsync(BlogEntryDTO blogEntryDto)
        {
            var blogEntry = _mapper.Map<BlogEntry>(blogEntryDto);
            await _blogEntryRepository.AddAsync(blogEntry);
            var blogEntryDTO = _mapper.Map<BlogEntryDTO>(blogEntry);
            return blogEntryDTO;
        }

        public async Task UpdateBlogEntryAsync(BlogEntryDTO blogEntryDto)
        {
            var blogEntry = _mapper.Map<BlogEntry>(blogEntryDto);
            await _blogEntryRepository.UpdateAsync(blogEntry);
        }

        public async Task DeleteBlogEntryAsync(int id)
        {
            var blogEntry = await _blogEntryRepository.GetByIdAsync(id);
            if (blogEntry != null)
            {
                await _blogEntryRepository.DeleteAsync(blogEntry);
            }
        }

        public async Task AssociateCategoryAsync(int blogEntryId, int categoryId)
        {
            var blogEntryCategory = new BlogEntryCategory
            {
                BlogEntryId = blogEntryId,
                CategoryId = categoryId
            };
            await _blogEntryCategoryRepository.AddAsync(blogEntryCategory);
        }

        public async Task DisassociateCategoryAsync(int blogEntryId, int categoryId)
        {
            await _blogEntryCategoryRepository.RemoveAsync(blogEntryId, categoryId);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByBlogEntryIdAsync(int blogEntryId)
        {
            var blogEntryCategories = await _blogEntryCategoryRepository.GetByBlogEntryIdAsync(blogEntryId);
            var categories = new List<CategoryDTO>();

            foreach (var blogEntryCategory in blogEntryCategories)
            {
                var category = await _categoryRepository.GetByIdAsync(blogEntryCategory.CategoryId);
                if (category != null)
                {
                    var categoryDTO = _mapper.Map<CategoryDTO>(category);
                    categories.Add(categoryDTO);
                }
            }
            return categories;
        }
    }
}
