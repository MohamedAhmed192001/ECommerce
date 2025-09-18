using ECommerce.Core.DTOs.Auth;
using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IAuth
    {
        Task<string> RegisterAsync(RegisterDTO registerDTO);
        Task<string> LoginAsync(LoginDTO login);
        Task<bool> SendEmailForForgetPassword(string email);
        Task<string> ResetPassword(RestPasswordDTO restPassword);
        Task<bool> ActiveAccount(ActiveAccountDTO accountDTO);
        
    }
}
