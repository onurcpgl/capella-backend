using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleReadRepository _roleReadRepository;
        private readonly IRoleWriteRepository _roleWriteRepository;
        private readonly IPermissionReadRepository _permissionReadRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleReadRepository roleReadRepository, IRoleWriteRepository roleWriteRepository, IPermissionReadRepository permissionReadRepository, IMapper mapper)
        {
            _roleReadRepository = roleReadRepository;
            _roleWriteRepository = roleWriteRepository;
            _permissionReadRepository = permissionReadRepository;
            _mapper = mapper;
        }

        public async Task Delete(string code)
        {
            var role = _roleReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            await _roleWriteRepository.RemoveAsync(role);
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
            var roles = await _roleReadRepository.GetAll().Include(x => x.Permissions).ToListAsync();
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return rolesDto;
        }

        public async Task<RoleDto> GetRoleByCode(string code)
        {
            var role = _roleReadRepository.GetWhere(x => x.Code == code).FirstOrDefault();
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;
        }

        public async Task Save(RoleDto roleDto)
        {
            Role role = new();
            role.Name = roleDto.Name;
            role.IsActive = roleDto.IsActive;
            var permissions = new HashSet<Domain.Entities.Permission>();
            foreach (var item in roleDto.Permissions)
            {
                var permission = _permissionReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                permissions.Add(permission);
            }
            role.Permissions = permissions;
            await _roleWriteRepository.AddAsync(role);
        }

        public async Task Update(RoleDto roleDto)
        {
            var role = await _roleReadRepository.GetWhere(x => x.Code == roleDto.Code).Include(x => x.Permissions).FirstOrDefaultAsync();
            role.Name = roleDto.Name;
            role.IsActive = roleDto.IsActive;
            var permissions = new HashSet<Domain.Entities.Permission>();
            foreach (var item in roleDto.Permissions)
            {
                var permission = _permissionReadRepository.GetWhere(x => x.Code == item.Code).FirstOrDefault();
                permissions.Add(permission);
            }
            role.Permissions = permissions;
            await _roleWriteRepository.UpdateAsync(role,role.Id);
        }
    }
}
