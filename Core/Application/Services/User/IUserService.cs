using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<bool> Save(UserDto userDto);
        Task<List<User>> GetAllUsers();
        Task<User> loadByUser(LoginDto loginDto);
        Task<UserDto> GetUserByUsername(string username);
    }
}
