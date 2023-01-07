using Application.DataTransferObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IRoleService
    {
        Task<bool> Save(RoleDto roleDto);
        Task<List<Role>> GetAllRoles();
        Task<RoleDto> GetRoleByCode(string code);
    }
}
