using E_CommerceApplication.Application.Interfaces;
using E_CommerceApplication.Core.DTOs.Auth;
using E_CommerceApplication.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace E_CommerceApplication.Application.Services {
    public class AuthService : IAuthService {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration) {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto) {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null) {
                throw new Exception("User with this email already exists.");
            }

            var newUser = new User {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded) {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var token = GenerateJwtToken(newUser);

            return new AuthResponseDto {
                Token = token,
                Email = newUser.Email,
                FullName = newUser.FullName,
                UserId = newUser.Id
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto) {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) {
                throw new Exception("Invalid email or password");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid) {
                throw new Exception("Invalid email or password");
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseDto {
                Token = token,
                Email = user.Email!,
                FullName = user.FullName ?? string.Empty,
                UserId = user.Id
            };
        }

        private string GenerateJwtToken(User user) {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
