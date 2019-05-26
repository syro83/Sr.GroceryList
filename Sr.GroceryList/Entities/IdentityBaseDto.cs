using System;
using System.Collections.Generic;
using System.Text;

namespace Sr.GroceryList.Entities
{
	public abstract class IdentityBaseDto : IIdentityBaseDto
	{
		protected IdentityBaseDto()
		{
		}

		protected IdentityBaseDto(int id)
		{
			Id = id;
		}

		/// <summary>
		/// The identifier of the entity
		/// </summary>
		public int Id
		{
			get;
			set;
		}


		public override string ToString()
		{
			return string.Format(
				$"({GetType()})[Id={Id}]", this);
		}

	}
}
