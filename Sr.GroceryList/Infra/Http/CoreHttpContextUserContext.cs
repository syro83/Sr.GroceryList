using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Sr.GroceryList.Infra.http
{
	public sealed class CoreHttpContextUserContext : IUserContext
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CoreHttpContextUserContext(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string Name
		{
			get
			{
				return _httpContextAccessor.HttpContext.User.Identity.Name;
			}
		}

		public string GetIp()
		{
			return GetClientIp();
		}

		// ToDo SR implement
		private string GetClientIp()
		{
			return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
		}
	}

}
