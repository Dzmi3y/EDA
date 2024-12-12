using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Interfaces;
using EDA.Service.Identity.Models;
using EDA.Shared.DTOs;
using EDA.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDA.Service.Identity.Services
{
    public class UserService: IUserService
    {
        private readonly AppDbContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IIssueTokenService _issueTokenService;
        public UserService(AppDbContext db, IIssueTokenService issueTokenService,
            IPasswordHasher<User> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _issueTokenService = issueTokenService;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _db.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        private async Task<User> GetNewUserAsync(SignUpUserDto userDto)
        {
            var existing = await GetUserByEmailAsync(userDto.Email);
            if (existing != null)
                throw new UserException("User already exists");

            var passwordHash = _passwordHasher.HashPassword(null, userDto.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Name = userDto.Name,
                PasswordHash = passwordHash
            };

            return newUser;
        }

        public async Task<Guid> SignUp(SignUpUserDto userDto)
        {
            var newUser = await GetNewUserAsync(userDto);

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<AuthenticationResult> SignIn(SignInUserDto userDto)
        {
            var user = await GetUserByEmailAsync(userDto.Email);

            if (user == null)
                throw new UserException("User does not exist");


            var passwordVerifyResult = _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, userDto.Password);
            if (passwordVerifyResult == PasswordVerificationResult.Failed)
                throw new UserException("Password is wrong");

            var tokenIssueServiceResponse = await _issueTokenService.GenerateAuthenticationResult(user);
            return tokenIssueServiceResponse;
        }

    }
}
