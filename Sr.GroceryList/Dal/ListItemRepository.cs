using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using Sr.GroceryList.Entities;
using Sr.GroceryList.Infra;
using System.Threading.Tasks;

namespace Sr.GroceryList.Dal
{
	public class ListItemRepository : IdentityEntityBaseRepository<ListItemDto>, IListItemRepository
	{
		public ListItemRepository(ICoreContext coreContext)
			: base(coreContext)
		{
		}
		public async Task<ListItemDto> GetByNameAsync(string code)
		{
			var items = await GetAllAsync(Predicates.Field<ListItemDto>(f => f.Description, Operator.Eq, code));
			return items.FirstOrDefault();
		}


		private List<ListItemDto> GetMultiple(IEnumerable<int> ids)
		{
			using (var connection = GetConnection())
			{
				//var idsTvp = ToTvp(ids.Select(e => new { Id = e }));
				var idsTvp = ToTvp(ids, e => e);

				connection.Open();
				return connection.Query<ListItemDto>("[dbo].[ListItem_GetMultiple]",
					new
					{
						ids = idsTvp,
					}, commandType: CommandType.StoredProcedure).ToList();
			}
		}
	}
}
