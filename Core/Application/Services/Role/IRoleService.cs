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
        Task Save(RoleDto roleDto);
        Task<List<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleByCode(string code);
        Task Update(RoleDto roleDto);
        Task Delete(string code);
    }
}
