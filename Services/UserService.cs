using AutoMapper;
using BlogApp.Data;
using BlogApp.DTOs;
using BlogApp.Models;
using BlogApp.Repositories.Interfaces;
using BlogApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IPasswordHasher<User> passwordHasher, BlogDbContext context, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var usersBase = await _userRepository.GetAllAsync();
            var userList = new List<UserDTO>();
            foreach(var user in usersBase)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                userList.Add(userDto);
            }
            return userList;
        }

        public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task CreateUserAsync(User user) => await _userRepository.AddAsync(user);

        public async Task UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }
        public async Task<User> ValidateUserAsync(string email, string password)
        {
            //Se debería integrar esta llamada a la BD en un repositorio
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            return verificationResult == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User> RegisterUserAsync(string firstName, string lastName, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new Exception("Email already in use");
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RegistrationDate = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
