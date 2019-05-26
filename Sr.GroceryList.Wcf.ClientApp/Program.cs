using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sr.GroceryList.Wcf.Contracts;
using Flurl;
using RestSharp;

namespace Sr.GroceryList.Wcf.ClientApp
{
	class Program
	{
		static void Main(string[] args)
		{

			var client = new RestClient("http://localhost:15581/ListItem");

			var getRequest = new RestRequest("GET");
			var getResponse = client.Execute<GetAllResponse>(getRequest);
			var ListItems = getResponse.Data?.Result;
			System.Console.WriteLine($"Result {getRequest} : {ListItems}");

			var getByIdRequest = new RestRequest("GET/{id}");
			getByIdRequest.AddUrlSegment("id", 1);
			var getByIdResponse = client.Execute<GetByIdResponse>(getByIdRequest);
			var ListItem = getByIdResponse.Data?.Result;
			System.Console.WriteLine($"Result {getByIdRequest} : {ListItem}");

			System.Console.ReadLine();
		}
	}
}
