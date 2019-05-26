using System;
using System.Collections.Generic;
using System.Text;

namespace Sr.GroceryList.Entities
{
    interface IListItemDto : IIdentityBaseDto
    {
        string Name { get; }
        string Description { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }        
        string Status { get; }
    }
}
