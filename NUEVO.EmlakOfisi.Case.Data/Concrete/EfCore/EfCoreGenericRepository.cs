using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUEVO.EmlakOfisi.Case.Data.Abstract;

namespace NUEVO.EmlakOfisi.Case.Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class // TEntity bir class olmak zorunda
    where TContext : DbContext, new() //Tcontext bir Dbcontext olmak zorunda ve newlenebilir.
    {
        public void Create(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }
    }
}
