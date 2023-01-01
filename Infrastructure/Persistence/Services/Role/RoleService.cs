﻿using Application.DataTransferObject;
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
        public RoleService(IRoleReadRepository roleReadRepository, IRoleWriteRepository roleWriteRepository, IPermissionReadRepository permissionReadRepository)
        {
            _roleReadRepository = roleReadRepository;
            _roleWriteRepository = roleWriteRepository;
            _permissionReadRepository = permissionReadRepository;
        }

        public async Task<List<Role>> getAll()
        {
            List<Role> roles = await _roleReadRepository.GetAll().Include(x => x.Permissions).ToListAsync();
            return roles;
        }

        public async Task<Role> getRoleById(int roleId)
        {
            var role = await _roleReadRepository.GetWhereWithInclude(x=> x.Id == roleId,true, x => x.Permissions).FirstOrDefaultAsync();
            return role;
        }

        public async Task<bool> save(RoleDto roleDto)
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
            var result = await _roleWriteRepository.AddAsync(role);
            return result;
        }
    }
}
