
using Microsoft.Extensions.Configuration;
using Sr.GroceryList.Infra;

namespace Sr.GroceryList.Infra.Core
{
	public class EnvironmentVariableConnectionSettings : IConnectionSettings
	{
		private IConfiguration _configuration;

		public EnvironmentVariableConnectionSettings(IConfiguration Configuration)
		{
			_configuration = Configuration;
		}

		public string DatabaseConnectionString
		{
			get
			{
				return _configuration.GetConnectionString("SrGroceryListConnectionString");				
			}
		}
	}
}
