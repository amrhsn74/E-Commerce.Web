using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs.IdentityModuleDto;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        // Takes Email and Password then return Token, Email and DisplayName
        Task<UserDto> LoginAsync(LoginDto loginDto);

        // Register
        // Takes Email, Password, USer Name, Display Name and Phone Number
        // Then return Token, Email and DisplayName
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

    }
}
