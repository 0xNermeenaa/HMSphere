using HMSphere.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSphere.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthDTO> RegisterAsync(RegisterDto model);
        Task<AuthDTO> LoginAsync(LoginDto model);

    }
}
