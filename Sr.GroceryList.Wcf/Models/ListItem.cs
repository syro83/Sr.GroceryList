using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Sr.GroceryList.Wcf.Models
{
	[DataContract]
	public class ListItem
	{
		[DataMember]
		public int Id{ get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public DateTime CreatedAt { get; set; }

		[DataMember]
		public DateTime UpdatedAt { get; set; }


		[DataMember]
		public string Status { get; set; }

		public ListItem()
		{
		}

		public override string ToString()
		{
			return string.Format(
				$"({GetType()})[Id={Id},Name={Name},Description={Description},CreatedAt={CreatedAt},UpdatedAt={UpdatedAt},Status={Status}]", this);
		}
	}
}
