using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using Sr.GroceryList.Entities;
using Sr.GroceryList.Infra;
using System.Threading.Tasks;
using Sr.GroceryList.Infra.Exceptions;

namespace Sr.GroceryList.Dal
{
	public abstract class IdentityEntityBaseRepository<T> : IEntityAsyncRepository<T>, IEntityRepository<T> where T : IdentityBaseDto
	{
		protected readonly IConnectionSettings _connectionSettings;
		protected readonly ICoreContext _coreContext;


		public IdentityEntityBaseRepository(ICoreContext coreContext)
		{
			_connectionSettings = coreContext.ConnectionSettings;
			_coreContext = coreContext;
		}

		public IDbConnection GetConnection()
		{
			return new SqlConnection(_connectionSettings.DatabaseConnectionString);
		}

		public virtual bool Delete(T entity)
		{
			using (var connection = GetConnection())
			{
				connection.Open();

				var result = connection.Delete<T>(entity);
				return result;
			}
		}

		public virtual T GetById(int id)
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				return connection.Get<T>(id);
			}
		}

		public virtual T Insert(T entity)
		{
			if (entity.Id > 0)
			{
				throw new SrArgumentException("Cannot insert entity", string.Format("Id can not be greater then 0 when inserting, entity: {0}", entity), "Id");
			}

			using (var connection = GetConnection())
			{
				connection.Open();
				var result = connection.Insert(entity);
				entity.Id = result as int? ?? -1;
				return entity;
			}
		}

		public virtual bool Update(T entity)
		{
			if (entity.Id <= 0)
			{
				throw new SrArgumentException("Cannot update entity", string.Format("Id has to be set when updating, entity: {0}", entity), "Id");
			}

			using (var connection = GetConnection())
			{
				connection.Open();
				return connection.Update(entity);
			}
		}

		public virtual IEnumerable<T> GetAll(object predicate, IList<ISort> sort = null)
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				return connection.GetList<T>(predicate, sort).ToList<T>();
			}
		}

		public virtual IEnumerable<T> GetAll()
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				return connection.GetList<T>().ToList() ?? new List<T>();
			}
		}

		public virtual async Task<bool> DeleteAsync(T entity)
		{
			using (var connection = GetConnection())
			{
				connection.Open();

				var result = await connection.DeleteAsync<T>(entity);
				return result;
			}
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				var result = await connection.GetAsync<T>(id);
				return result;
			}
		}

		public virtual async Task<T> InsertAsync(T entity)
		{
			if (entity.Id > 0)
			{
				throw new SrArgumentException("Cannot insert entity", string.Format("Id can not be greater then 0 when inserting, entity: {0}", entity), "Id");
			}

			using (var connection = GetConnection())
			{
				connection.Open();
				var result = await connection.InsertAsync(entity);
				entity.Id = result as int? ?? -1;
				return entity;
			}
		}

		public virtual async Task<bool> UpdateAsync(T entity)
		{
			if (entity.Id <= 0)
			{
				throw new SrArgumentException("Cannot update entity", string.Format("Id has to be set when updating, entity: {0}", entity), "Id");
			}

			using (var connection = GetConnection())
			{
				connection.Open();
				var result = await connection.UpdateAsync<T>(entity);
				return result;
			}
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync(object predicate, IList<ISort> sort = null)
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				var result = await connection.GetListAsync<T>(predicate, sort);
				return result;
			}
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			using (var connection = GetConnection())
			{
				connection.Open();
				var result = await connection.GetListAsync<T>();
				result = result ?? new List<T>();
				return result;
			}
		}

		protected Dapper.SqlMapper.ICustomQueryParameter ToTvp<Tt>(IEnumerable<Tt> enumerable, params Func<Tt, object>[] columnSelectors)
		{
			if (columnSelectors.Length == 0)
			{
				Func<Tt, object> getSelf = x => x;
				columnSelectors = new[] { getSelf };
			}
			var result = new DataTable();
			foreach (var selector in columnSelectors)
			{
				result.Columns.Add();
			}
			foreach (var item in enumerable)
			{
				var colValues = columnSelectors.Select(selector => selector(item)).ToArray();
				result.Rows.Add(colValues);
			}
			return result.AsTableValuedParameter();
		}
	}
}
