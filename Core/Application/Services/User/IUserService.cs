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
        Task Save(UserDto userDto);
        Task<List<UserDto>> GetAllUsers();
        Task<User> loadByUser(LoginDto loginDto);
        Task<UserDto> GetUserByUsername(string username);
        Task Update(UserDto userDto);
        Task Delete(string username);
    }
}
