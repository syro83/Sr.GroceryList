using System.ServiceModel;
using System.ServiceModel.Web;
using Sr.GroceryList.Wcf.Contracts;

namespace Sr.GroceryList.Wcf.Service
{
	[ServiceContract]
	public interface IService1
	{

		[OperationContract]
		[WebGet(UriTemplate = "Hello")]
		GetMessageResponse Hello();

		[OperationContract]
		[WebGet(UriTemplate = "Hello/{name}", ResponseFormat = WebMessageFormat.Json)]
		GetMessageResponse HelloName(string name);
	}
}