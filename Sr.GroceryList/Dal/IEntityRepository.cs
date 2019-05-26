using System;
using System.Collections.Generic;
using System.Text;
using Sr.GroceryList.Entities;

namespace Sr.GroceryList.Dal
{
	public interface IEntityRepository<T> where T : IdentityBaseDto
	{
		/// <summary>
		/// Get entity by identifier
		/// </summary>
		/// <param Description="id">Identifier</param>
		/// <returns>Entity</returns>
		T GetById(int id);

		/// <summary>
		/// Get all entities
		/// </summary>
		/// <returns>Collection with all entities</returns>
		IEnumerable<T> GetAll();

		/// <summary>
		/// Insert entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		T Insert(T entity);

		/// <summary>
		/// Update entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		bool Update(T entity);

		/// <summary>
		/// Delete entity
		/// </summary>
		/// <param Description="entity">Entity</param>
		bool Delete(T entity);
	}
}
