using Application.DataTransferObject.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObject
{
    public class UserDto: BaseDto
    {
        public string Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public ISet<Address>? Addresses { get; set; }
        public ISet<RoleDto>? Roles { get; set; }
        public ISet<PermissionDto>? Permissions { get; set; }
      
    }
}
