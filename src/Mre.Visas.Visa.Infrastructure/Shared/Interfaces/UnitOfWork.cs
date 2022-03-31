//using Mre.Visas.Visa.Application.RolEstado.Repositories;
using Mre.Visas.Visa.Application.Shared.Interfaces;
using Mre.Visas.Visa.Application.VisaElectronica.Repositories;
using Mre.Visas.Visa.Infrastructure.Persistence.Contexts;
using System;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Infrastructure.Shared.Interfaces
{
  public class UnitOfWork : IUnitOfWork
  {
    #region Constructors

    public UnitOfWork(
        ApplicationDbContext context,
        IVisaElectronicaRepository visaElectronicaRepository)

    {
      _context = context;
      VisaElectronicaRepository = visaElectronicaRepository;
    }

    #endregion Constructors

    #region Attributes

    protected readonly ApplicationDbContext _context;

    #endregion Attributes

    #region Properties

    public IVisaElectronicaRepository VisaElectronicaRepository { get; }

    
    #endregion Properties

    #region Methods

    public async Task<(bool, string)> SaveChangesAsync()
    {
      try
      {
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return (true, null);
      }
      catch (Exception ex)
      {
        return (false, ex.InnerException is null ? ex.Message : ex.InnerException.Message);
      }
    }

    #endregion Methods
  }
}