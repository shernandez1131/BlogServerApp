using BlogApp.Repositories.Interfaces;
using BlogApp.Services;
using Moq;
using BlogApp.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BlogApp.Data;
using BlogApp.DTOs;
using AutoMapper;
using BlogApp.Mappings;

namespace BlogApp.Tests
{
    public class BlogEntryServiceTests
    {
        private readonly Mock<IBlogEntryCategoryRepository> _blogEntryCategoryRepositoryMock;
        private readonly Mock<IRepository<Category>> _categoryRepositoryMock;
        private readonly Mock<IRepository<BlogEntry>> _blogEntryRepositoryMock;
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly BlogEntryService _blogEntryService;
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public BlogEntryServiceTests()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new BlogDbContext(options);
            _blogEntryCategoryRepositoryMock = new Mock<IBlogEntryCategoryRepository>();
            _categoryRepositoryMock = new Mock<IRepository<Category>>();
            _blogEntryRepositoryMock = new Mock<IRepository<BlogEntry>>();
            _userRepositoryMock = new Mock<IRepository<User>>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingsProfile());
            });
            _mapper = config.CreateMapper();
            _blogEntryService = new BlogEntryService(_context, _blogEntryRepositoryMock.Object, _blogEntryCategoryRepositoryMock.Object, _categoryRepositoryMock.Object, _userRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task AssociateCategoryAsync_ShouldCallRepository()
        {
            var blogEntryId = 1;
            var categoryId = 2;

            await _blogEntryService.AssociateCategoryAsync(blogEntryId, categoryId);

            _blogEntryCategoryRepositoryMock.Verify(r => r.AddAsync(It.IsAny<BlogEntryCategory>()), Times.Once);
        }

        [Fact]
        public async Task CreateBlogEntry_ShouldAddNewEntry()
        {
            var user = new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@example.com",
                PasswordHash = "password_test",
                RegistrationDate = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var blogEntryDto = new BlogEntryDTO
            {
                Title = "Test Blog Entry",
                Content = "This is a test blog entry.",
                PublicationDate = DateTime.UtcNow,
                AuthorId = user.Id
            };

            await _blogEntryService.CreateBlogEntryAsync(blogEntryDto);

            var entries = await _context.BlogEntries.ToListAsync();
            Assert.Single(entries);
            Assert.Equal("Test Blog Entry", entries[0].Title);
            Assert.Equal("This is a test blog entry.", entries[0].Content);
            Assert.Equal(user.Id, entries[0].AuthorId);
        }

    }

}
