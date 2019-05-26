using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sr.GroceryList.Dal;
using FluentValidation;

namespace Sr.GroceryList.WebApi.V1.Models
{
	public class ListItemValidator : AbstractValidator<ListItem>
	{
		private readonly IListItemRepository _ListItemRepository;

		public ListItemValidator(IListItemRepository ListItemRepository)
		{
			_ListItemRepository = ListItemRepository;
			RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Id is mandatory");
			RuleFor(x => x.Name).MaximumLength(100).WithMessage("Name can not be 'henk'")
				.Must((o, list, context) =>
				{
					if (null != o.Name)
					{
						context.MessageFormatter.AppendArgument("Name", o.Name);
						return IsValidType(o.Name);
					}
					return true;
				});
			RuleFor(x => x.CreatedAt).NotEmpty();
			RuleFor(x => x.UpdatedAt).NotEmpty();
			RuleFor(x => x.Status).Matches(@"^[a-zA-Z0-9]*$");
			
			// ToDo SR very stupid test, but it does something
			RuleFor(x => x.Name).MustAsync(IsNameAvailable).When(x => x.Id == 0 && x.Name != null);
		}

		private async Task<bool> IsNameAvailable(string name, CancellationToken token)
		{
			var result = await _ListItemRepository.GetByNameAsync(name);
			return result == null;
		}

		private bool IsValidType(string type)
		{
			return type.Trim().ToLower() != "henk";

		}
	}
}
