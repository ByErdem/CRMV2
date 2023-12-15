using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Dtos
{
	public class UserDto
	{
        public int Id { get; set; }
        public string TCKN { get; set; }
        public string Email { get; set; }
        public int AccessFailedCount { get; set; }
        public string SaltPassword { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RoleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
