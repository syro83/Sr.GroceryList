using System;
using System.Collections.Generic;
using System.Text;
using Sr.GroceryList.Infra;

namespace Sr.GroceryList.Wcf.Infra
{
	public sealed class WcfUserContext : IUserContext
	{

		public WcfUserContext()
		{
		}

		public string Name
		{
			get
			{
				var wcfContext = System.ServiceModel.OperationContext.Current;
				return wcfContext.ClaimsPrincipal.Identity.Name;
			}
		}

		public string GetIp()
		{
			return GetClientIp();
		}

		// ToDo SR implement
		private string GetClientIp()
		{
			var wcfContext = System.ServiceModel.OperationContext.Current;
			throw new NotImplementedException();
		}
	}

}
