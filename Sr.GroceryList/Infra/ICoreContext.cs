using System;
using System.Collections.Generic;
using System.Text;
using Sr.GroceryList.Infra;
using Microsoft.AspNetCore.Http;

namespace Sr.GroceryList.Infra
{
	public interface ICoreContext
	{
		IConnectionSettings ConnectionSettings { get; }

		IUserContext User { get; }

	}
}
