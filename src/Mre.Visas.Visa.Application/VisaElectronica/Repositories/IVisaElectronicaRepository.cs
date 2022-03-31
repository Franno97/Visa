using Mre.Visas.Visa.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Application.VisaElectronica.Repositories
{
  public interface IVisaElectronicaRepository : IRepository<Domain.Entities.VisaElectronica>
  {
    Task<List<Domain.Entities.VisaElectronica>> GetById(Guid id);
    Task<Domain.Entities.VisaElectronica> GetByTramiteId(Guid tramiteId);

    Int64 ObtenerSecuenciaVisaElectronica();

  }
}