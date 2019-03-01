using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using QTF.Data.Abstraction;

namespace QTF.Data.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        protected DbTransaction Transaction;
        protected Dictionary<string, dynamic> Repositories;

        public int? CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DbContext context, IServiceProvider serviceProvider )
        {
            _context = context;
            Repositories = new Dictionary<string, dynamic>();
            _serviceProvider = serviceProvider;
        }


        public virtual int SaveChanges() => _context.SaveChanges();

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public virtual IGenericRepository<TEntity,TKEY> GetRepository<TEntity, TKEY>(   ) where TEntity : class
        {

            if (Repositories == null)
            {
                Repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (Repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity, TKEY>)Repositories[type];
            }
           var newRepository = (IGenericRepository<TEntity, TKEY>)_serviceProvider.GetService(typeof(IGenericRepository<TEntity, TKEY>));

            Repositories.Add(type, newRepository);

            return newRepository;
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql, cancellationToken, parameters);
        }

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            //currently a solution to this is unavailable
            //var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            //if (objectContext.Connection.State != ConnectionState.Open)
            //{
            //    objectContext.Connection.Open();
            //}
            //Transaction = objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public virtual bool Commit()
        {
            Transaction.Commit();
            return true;
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
        }


    }
}
