using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Dtos
{
	public class UserRegisterDto
	{
        public string TCKN { get; set; }
        public string Name { get; set; }
		public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string SaltPassword { get; set; } = Guid.NewGuid().ToString();
	}
}
