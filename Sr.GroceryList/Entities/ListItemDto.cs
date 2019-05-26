using System;
using System.Collections.Generic;
using System.Text;

namespace Sr.GroceryList.Entities
{
	/// <summary>
	/// </summary>
	/// <remarks>
	/// ToDo SR: Use composition over inheritance
	/// </remarks>
	public class ListItemDto : IdentityBaseDto, IListItemDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string Status { get; set; }

	}
}
