using EDA.Service.Identity.Entities;
using EDA.Service.Identity.Interfaces;
using EDA.Service.Identity.Models;
using EDA.Shared.Authorization;
using EDA.Shared.DTOs;
using EDA.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EDA.Service.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;
        private readonly IPasswordHasher<IUser> _passwordHasher;
        private readonly IIssueTokenService _issueTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        public AccountService(AppDbContext db, IIssueTokenService issueTokenService,
            IRefreshTokenService refreshTokenService, IPasswordHasher<IUser> passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _issueTokenService = issueTokenService;
            _refreshTokenService = refreshTokenService;
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

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Email = userDto.Email,
                Name = userDto.Name,
                PasswordHash = userDto.PasswordHash
            };

            return newUser;
        }

        public async Task<Guid> SignUpAsync(SignUpUserDto userDto)
        {
            var newUser = await GetNewUserAsync(userDto);

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<AuthenticationResult> SignInAsync(SignInUserDto userDto)
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

        public async Task SignOutAsync(Guid refreshToken)
        {
            await _refreshTokenService.SetAsInvalidatedAsync(refreshToken);
        }

        public async Task<AuthenticationResult> RefreshAsync(Guid refreshToken)
        {
            return await _issueTokenService.RefreshTokenAsync(refreshToken);
        }

        public async Task DeleteAccountAsync(Guid userId)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) return;

            user.IsDeleted = true;
            await _db.SaveChangesAsync();
        }

    }
}
