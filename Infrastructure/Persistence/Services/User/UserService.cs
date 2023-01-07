using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IRoleReadRepository _roleReadRepository;
        private readonly IMapper _mapper;

        public UserService(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IRoleReadRepository roleReadRepository, IMapper mapper)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _roleReadRepository = roleReadRepository;
            _mapper = mapper;
        }

        public async Task<User> loadByUser(LoginDto loginDto)
        {
            var user =await  _userReadRepository.GetWhereWithInclude(x => x.Username == loginDto.Username && x.Password == loginDto.Password, true, x => x.Roles).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> Save(UserDto userDto)
        {
            User user = new();
            user.Firstname = userDto.Firstname;
            user.Lastname = userDto.Lastname;
            user.Username = userDto.Username;
            user.Password = userDto.Lastname;
            user.BirthDate = (DateTime)userDto.BirthDate;
            user.IsActive = true;
            user.IsDeleted = false;
            user.Email = userDto.Email;
            var roles = new HashSet<Role>();
            var role = _roleReadRepository.GetWhere(role => role.Code == "user").FirstOrDefault();
            roles.Add(role);
            user.Roles = roles;
            var result = await _userWriteRepository.AddAsync(user);
            return result;   
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await _userReadRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            var user = await _userReadRepository.GetWhere(x => x.Username == username).Include(x=>x.Roles).ThenInclude(x=> x.Permissions).FirstOrDefaultAsync();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

    }
}
