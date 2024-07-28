using IceCoffee.SimpleCRUD.Dtos;
using IceCoffee.SimpleCRUD.SqlGenerators;
using System;
using System.Security.Cryptography;

namespace IceCoffee.SimpleCRUD
{
    public abstract class RepositoryBase<TEntity> : ProtectedRepositoryBase<TEntity>, IRepository<TEntity>
    {
        public RepositoryBase(IDbConnectionFactory dbConnectionFactory, string dbAliase) : base(dbConnectionFactory, dbAliase)
        {
        }

        #region Sync

        #region Get
        public virtual TEntity? GetById<TKey>(TKey id)
        {
            return base.GetById(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return base.GetList();
        }

        public virtual PagedDto<TEntity> GetPagedList(int pageNumber, int pageSize)
        {
            return base.GetPagedList(pageNumber, pageSize);
        }

        public virtual int GetRecordCount()
        {
            return base.GetRecordCount();
        }
        #endregion

        #region Insert
        public virtual int Insert(TEntity entity)
        {
            return base.Insert(entity);
        }

        public virtual int Insert<TId>(TEntity entity, out TId id)
        {
            return base.Insert(entity, out id);
        }

        public virtual int Insert(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.Insert(entities, useTransaction);
        }
        #endregion

        #region InsertOrIgnore
        public virtual int InsertOrIgnore(TEntity entity)
        {
            return base.InsertOrIgnore(entity);
        }

        public virtual int InsertOrIgnore(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.InsertOrIgnore(entities, useTransaction);
        }
        #endregion

        #region InsertOrReplace
        public virtual int InsertOrReplace(TEntity entity)
        {
            return base.InsertOrReplace(entity);
        }

        public virtual int InsertOrReplace(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.InsertOrReplace(entities, useTransaction);
        }
        #endregion

        #region Update
        public virtual int Update(TEntity entity)
        {
            return base.Update(entity);
        }

        public virtual int Update(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.Update(entities, useTransaction);
        }
        #endregion

        #region Delete
        public virtual int Delete(TEntity entity)
        {
            return base.Delete(entity);
        }

        public virtual int Delete(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.Delete(entities, useTransaction);
        }

        public virtual int DeleteById<TId>(TId id)
        {
            return base.DeleteById(id);
        }

        public virtual int DeleteByIds<TId>(IEnumerable<TId> ids, bool useTransaction = false)
        {
            return base.DeleteByIds(ids, useTransaction);
        }

        public override int DeleteAll(bool useTransaction = false)
        {
            return base.Execute("DELETE FROM " + SqlGenerator.TableName, useTransaction: useTransaction);
        }
        #endregion

        #endregion

        #region Async


        public virtual Task<TEntity?> GetByIdAsync<TKey>(TKey id)
        {
            return base.GetByIdAsync(id);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return base.GetListAsync();
        }

        public virtual Task<PagedDto<TEntity>> GetPagedListAsync(int pageNumber, int pageSize)
        {
            return base.GetPagedListAsync(pageNumber, pageSize);
        }

        public virtual Task<int> GetRecordCountAsync()
        {
            return base.GetRecordCountAsync();
        }

        public virtual Task<int> InsertAsync(TEntity entity)
        {
            return base.InsertAsync(entity);
        }

        public virtual Task<int> InsertAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.InsertAsync(entities, useTransaction);
        }

        public virtual Task<int> InsertOrIgnoreAsync(TEntity entity)
        {
            return base.InsertOrIgnoreAsync(entity);
        }

        public virtual Task<int> InsertOrIgnoreAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.InsertOrIgnoreAsync(entities, useTransaction);
        }

        public virtual Task<int> InsertOrReplaceAsync(TEntity entity)
        {
            return base.InsertOrReplaceAsync(entity);
        }

        public virtual Task<int> InsertOrReplaceAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.InsertOrReplaceAsync(entities, useTransaction);
        }

        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            return base.UpdateAsync(entity);
        }

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.UpdateAsync(entities, useTransaction);
        }


        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            return base.DeleteAsync(entity);
        }

        public virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool useTransaction = false)
        {
            return base.DeleteAsync(entities, useTransaction);
        }

        public virtual Task<int> DeleteByIdAsync<TId>(TId id)
        {
            return base.DeleteByIdAsync(id);
        }

        public virtual Task<int> DeleteByIdsAsync<TId>(IEnumerable<TId> ids, bool useTransaction = false)
        {
            return base.DeleteByIdsAsync(ids, useTransaction);
        }

        public override Task<int> DeleteAllAsync(bool useTransaction = false)
        {
            return base.ExecuteAsync("DELETE FROM " + SqlGenerator.TableName, useTransaction: useTransaction);
        }
        #endregion
    }
}