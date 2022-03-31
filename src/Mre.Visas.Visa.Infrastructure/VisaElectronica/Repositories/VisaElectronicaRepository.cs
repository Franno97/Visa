using Microsoft.EntityFrameworkCore;
using Mre.Visas.Visa.Application.VisaElectronica.Repositories;
using Mre.Visas.Visa.Infrastructure.Persistence.Contexts;
using Mre.Visas.Visa.Infrastructure.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Infrastructure.VisaElectronica.Repositories
{
  public class VisaElectronicaRepository : Repository<Domain.Entities.VisaElectronica>, IVisaElectronicaRepository
  {
    #region Constructors

    public VisaElectronicaRepository(ApplicationDbContext context)
        : base(context)
    {
    }

    #endregion Constructors

    #region Metodos Others 
    public async Task<List<Domain.Entities.VisaElectronica>> GetById(Guid id)
    {
      //aqui falta agregar los estados de las definciones solo para el beneficiario
      return await _context.VisaElectronicas.Where(x => x.Id == id).ToListAsync();
    }

    public Int64 ObtenerSecuenciaVisaElectronica()
    {
      if (_context.VisaElectronicas.Count() > 0)
        return _context.VisaElectronicas.Max(c => c.SecuenciaVisa);
      else
        return 0;
    }

    public async Task<Domain.Entities.VisaElectronica> GetByTramiteId(Guid tramiteId)
    {
      return await _context.VisaElectronicas.Where(x => x.TramiteId == tramiteId).FirstOrDefaultAsync();
    }


    #endregion
  }
}