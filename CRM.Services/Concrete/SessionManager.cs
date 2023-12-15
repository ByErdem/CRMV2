using CRM.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Concrete
{
	public class SessionManager : ISessionService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionManager(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public int SetSessionValue(string key, string value)
		{
			if (_httpContextAccessor.HttpContext?.Session != null)
			{
				_httpContextAccessor.HttpContext.Session.Set(key, Encoding.UTF8.GetBytes(value));
				return 1;
			}

			return 0;
		}

		public string? GetSessionValue(string key)
		{
			if (_httpContextAccessor.HttpContext?.Session != null)
			{
				byte[] sessionValue;
				if (_httpContextAccessor.HttpContext.Session.TryGetValue(key, out sessionValue))
				{
					return Encoding.UTF8.GetString(sessionValue);
				}
			}

			return null;
		}
	}
}
