using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Abstract
{
	public interface ISessionService
	{
		int SetSessionValue(string key, string value);
		string GetSessionValue(string key);
	}
}
