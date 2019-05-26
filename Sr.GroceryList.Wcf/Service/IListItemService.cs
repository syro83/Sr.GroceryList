using System.ServiceModel;
using System.ServiceModel.Web;
using Sr.GroceryList.Wcf.Contracts;

namespace Sr.GroceryList.Wcf.Service
{
	[ServiceContract]
	public interface IListItemService
	{

		[OperationContract]
		[WebGet(UriTemplate = "GET", ResponseFormat = WebMessageFormat.Json)]
		GetAllResponse GetAll();

		[OperationContract]
		[WebGet(UriTemplate = "GET/{idStr}", ResponseFormat = WebMessageFormat.Json)]
		GetByIdResponse GetById(string idStr);
	}
}