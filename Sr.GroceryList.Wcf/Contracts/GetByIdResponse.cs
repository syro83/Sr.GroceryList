using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using Sr.GroceryList.Wcf.Models;

namespace Sr.GroceryList.Wcf.Contracts
{
	[MessageContract]
	public class GetByIdResponse
	{
		[MessageBodyMember]
		public ListItem Result { get; set; }

	}
}