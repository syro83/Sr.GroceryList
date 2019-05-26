using AutoMapper;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Entities;
using Sr.GroceryList.Wcf.Models;
using Sr.GroceryList.Wcf.Contracts;
using System.ServiceModel.Web;
using System.ServiceModel;

namespace Sr.GroceryList.Wcf.Service
{
	public class Service1 : IService1
	{
		public Service1()
		{
		}

		public GetMessageResponse Hello()
		{
			return HelloName(null);
		}

		public GetMessageResponse HelloName(string name)
		{
			name = string.IsNullOrEmpty(name) ? "world" : name;

			return new GetMessageResponse
			{
				MessageId = new System.Guid(),
				Message = $"Hello {name}!"
			};
		}
	}
}