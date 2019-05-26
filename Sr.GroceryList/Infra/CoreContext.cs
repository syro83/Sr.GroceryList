using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Sr.GroceryList.Infra
{
	public class CoreContext : ICoreContext
	{
		private readonly IConnectionSettings _connectionSettings;
		private readonly IUserContext _userContext;

		public CoreContext(IConnectionSettings connectionSettings, IUserContext userContext)
		{
			_connectionSettings = connectionSettings;
			_userContext = userContext;
		}

		public IConnectionSettings ConnectionSettings => _connectionSettings;

		public IUserContext User => _userContext;

		public void GetCurrentUser()
		{
			var userName = _userContext.Name;
		}
	}
}
