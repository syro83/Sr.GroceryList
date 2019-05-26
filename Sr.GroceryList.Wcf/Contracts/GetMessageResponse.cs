using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Sr.GroceryList.Wcf.Contracts
{
	[MessageContract]
	public class GetMessageResponse
	{
		[MessageBodyMember]
		public Guid MessageId { get; set; }
		[MessageBodyMember]
		public string Message { get; set; }
	}
}