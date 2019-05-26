using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sr.GroceryList.WebApi.V1.Models
{
	public class ListItem
	{
		public int Id{ get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
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
