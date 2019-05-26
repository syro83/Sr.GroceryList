using System;
using System.Collections.Generic;
using System.Text;

namespace Sr.GroceryList.Infra
{
	public interface IConnectionSettings
	{
		string DatabaseConnectionString { get; }
	}
}
