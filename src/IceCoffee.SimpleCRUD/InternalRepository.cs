using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceCoffee.SimpleCRUD
{
    internal class InternalRepository<TEntity> : RepositoryBase<TEntity>
    {
        public InternalRepository(IDbConnectionFactory dbConnectionFactory, ISqlGeneratorFactory sqlGeneratorFactory) 
            : base(dbConnectionFactory, sqlGeneratorFactory)
        {
        }
    }
}
