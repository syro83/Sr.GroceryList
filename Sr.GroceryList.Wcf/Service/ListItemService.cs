using AutoMapper;
using Sr.GroceryList.Dal;
using Sr.GroceryList.Entities;
using Sr.GroceryList.Wcf.Models;
using Sr.GroceryList.Wcf.Contracts;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;

namespace Sr.GroceryList.Wcf.Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class ListItemService : IListItemService
	{
		private readonly IListItemRepository _ListItemRepository;
		protected IMapper mapper = null;

		public ListItemService(IListItemRepository ListItemRepository)
		{
			_ListItemRepository = ListItemRepository;

			// ToDo SR : Maybe use DI to inject the mapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ListItemDto, ListItem>();
				cfg.CreateMap<ListItem, ListItemDto>();
			});
			mapper = config.CreateMapper();
		}

		public GetAllResponse GetAll()
		{
			var ListItems = _ListItemRepository.GetAll();

			if (ListItems == null)
			{
				// ToDo SR fault exception
				throw new FaultException("Not found");
			}

			var result = mapper.Map<IEnumerable<ListItemDto>, IEnumerable<ListItem>>(ListItems);

			return new GetAllResponse()
			{
				Result = result.ToList()
			};
		}


		public GetByIdResponse GetById(string idStr)
		{
			int id;
			if(!int.TryParse(idStr, out id))
			{
				throw new FaultException("Id should be an int");
			}

			var ListItem = _ListItemRepository.GetById(id);

			if (ListItem == null)
			{
				// ToDo SR fault exception
				throw new FaultException("Not found");
			}

			var result = mapper.Map<ListItemDto, ListItem>(ListItem);

			return new GetByIdResponse()
			{
				Result = result
			};
		}
	}
}