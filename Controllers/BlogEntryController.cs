using BlogApp.DTOs;
using BlogApp.Models;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogEntryController : ControllerBase
    {
        private readonly IBlogEntryService _blogEntryService;

        public BlogEntryController(IBlogEntryService blogEntryService)
        {
            _blogEntryService = blogEntryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogEntries()
        {
            var blogEntries = await _blogEntryService.GetAllBlogEntriesAsync();
            return Ok(blogEntries);
        }

        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetAllUserBlogEntries(int userId)
        {
            var blogEntries = await _blogEntryService.GetAllUserBlogEntriesAsync(userId);
            return Ok(blogEntries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogEntryById(int id)
        {
            var blogEntry = await _blogEntryService.GetBlogEntryByIdAsync(id);
            if (blogEntry == null)
                return NotFound();

            return Ok(blogEntry);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogEntry([FromBody] BlogEntryDTO blogEntry)
        {
            var createdBlogEntry = await _blogEntryService.CreateBlogEntryAsync(blogEntry);
            return Ok(new { BlogEntry = createdBlogEntry });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogEntry(int id, [FromBody] BlogEntryDTO blogEntry)
        {
            if (id != blogEntry.Id)
                return BadRequest();

            await _blogEntryService.UpdateBlogEntryAsync(blogEntry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogEntry(int id)
        {
            await _blogEntryService.DeleteBlogEntryAsync(id);
            return NoContent();
        }

        [HttpPost("{blogEntryId}/categories/{categoryId}")]
        public async Task<IActionResult> AssociateCategory(int blogEntryId, int categoryId)
        {
            await _blogEntryService.AssociateCategoryAsync(blogEntryId, categoryId);
            return Ok();
        }

        [HttpDelete("{blogEntryId}/categories/{categoryId}")]
        public async Task<IActionResult> DisassociateCategory(int blogEntryId, int categoryId)
        {
            await _blogEntryService.DisassociateCategoryAsync(blogEntryId, categoryId);
            return Ok();
        }

        [HttpGet("{blogEntryId}/categories")]
        public async Task<IActionResult> GetCategoriesByBlogEntryId(int blogEntryId)
        {
            var categories = await _blogEntryService.GetCategoriesByBlogEntryIdAsync(blogEntryId);
            return Ok(categories);
        }
    }

}
