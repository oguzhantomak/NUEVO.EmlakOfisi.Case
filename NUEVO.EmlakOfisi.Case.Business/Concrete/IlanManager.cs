using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUEVO.EmlakOfisi.Case.Business.Abstract;
using NUEVO.EmlakOfisi.Case.Data.Abstract;
using NUEVO.EmlakOfisi.Case.Entity;

namespace NUEVO.EmlakOfisi.Case.Business.Concrete
{
    public class IlanManager : IIlanService
    {
        #region [ Inj ]

        private readonly IIlanRepository _ilanRepository;

        public IlanManager(IIlanRepository ilanRepository)
        {
            _ilanRepository = ilanRepository;
        }

        #endregion

        public void Create(Ilan entity)
        {
            _ilanRepository.Create(entity);
        }
    }
}
