using System.Configuration;
using Sr.GroceryList.Infra;

namespace Sr.GroceryList.Wcf.Infra
{
	public class ConfigurationManagerConnectionSettings : IConnectionSettings
	{
		public string DatabaseConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["SrGroceryListConnectionString"].ConnectionString;
			}
		}
	}
}
