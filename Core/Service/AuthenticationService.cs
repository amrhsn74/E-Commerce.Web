using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DTOs.IdentityModuleDto;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            // Check if the user exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException("User not found");

            // Check if the password is correct
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPasswordValid)
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = CreateTkenAsync(user)
                };
            else
                throw new UnauthorizedException();

        }

        private static string CreateTkenAsync(ApplicationUser user)
        {
            return "Token - TODO";
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Map RegisterDto to ApplicationUser
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            // Create a new user
            var Result = await _userManager.CreateAsync(user, registerDto.Password);
            if (Result.Succeeded)
            {
                // Return UserDto
                return new UserDto
                {
                    Email = user.Email,
                    Token = CreateTkenAsync(user),
                    DisplayName = user.DisplayName
                };
            }
            else
            {
                // Handle errors
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }
    }
}
