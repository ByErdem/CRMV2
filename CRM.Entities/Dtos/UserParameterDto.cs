using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Dtos
{
	public class UserParameterDto
	{
		public int UserId { get; set; }
		public string EMail { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
		public string GuidKey { get; set; }
        public string SecretKey { get; set; }
    }
}
