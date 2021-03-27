using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUEVO.EmlakOfisi.Case.Data.Abstract;
using NUEVO.EmlakOfisi.Case.Entity;

namespace NUEVO.EmlakOfisi.Case.Data.Concrete.EfCore
{
    public class EfCoreIlanRepository : EfCoreGenericRepository<Ilan, EmlakfOfisiContext>, IIlanRepository
    {
    }
}
