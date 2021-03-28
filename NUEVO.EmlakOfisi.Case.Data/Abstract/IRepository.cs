using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUEVO.EmlakOfisi.Case.Data.Abstract
{
    public interface IRepository<T>
    { 
        T Create(T entity);
    }
}
