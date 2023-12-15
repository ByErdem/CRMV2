using CRM.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Abstract
{
	public interface ITokenService
	{
        Tuple<string, string> GenerateToken(string username);

        bool ValidateToken(string token, string secretKey);

    }
}
