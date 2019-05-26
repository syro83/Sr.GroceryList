using System;
using System.Collections.Generic;
using System.Text;
using DapperExtensions.Mapper;

namespace Sr.GroceryList.Entities
{
	class ListItemDtoMapper : ClassMapper<ListItemDto>
	{
		public ListItemDtoMapper()
		{
			SchemaName = "dbo";
			TableName = "ListItem";

			//Map(x => x.rv).ReadOnly();  .Ignore();
			Map(x => x.Id).Key(KeyType.Identity);

			// ToDo SR CreatedAt should be InsertOnly(), so add this to DapperExtensions
			//Map(x => x.CreatedAt).ReadOnly();

			//optional, map all other columns
			AutoMap();
		}
	}
}
