using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sr.GroceryList.Entities;

namespace Sr.GroceryList.Dal
{
	public interface IListItemRepository: IEntityAsyncRepository<ListItemDto>, IEntityRepository<ListItemDto>
	{
		Task<ListItemDto> GetByNameAsync(string code);
	}
}
