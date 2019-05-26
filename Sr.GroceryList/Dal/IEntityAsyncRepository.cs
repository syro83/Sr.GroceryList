using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sr.GroceryList.Entities;

namespace Sr.GroceryList.Dal
{
	public interface IEntityAsyncRepository<T> where T : IdentityBaseDto
	{
		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param Description="id">Identifier</param>
		/// <returns>Entity</returns>
		Task<T> GetByIdAsync(int id);

		/// <summary>
		/// Get all entities
		/// </summary>
		/// <returns>Collection with all entities</returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		Task<T> InsertAsync(T entity);

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		Task<bool> UpdateAsync(T entity);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		Task<bool> DeleteAsync(T entity);
	}
}
