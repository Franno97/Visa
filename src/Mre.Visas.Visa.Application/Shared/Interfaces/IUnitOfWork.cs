using Mre.Visas.Visa.Application.VisaElectronica.Repositories;

using System.Threading.Tasks;

namespace Mre.Visas.Visa.Application.Shared.Interfaces
{
  public interface IUnitOfWork
  {
    IVisaElectronicaRepository VisaElectronicaRepository { get; }


    Task<(bool, string)> SaveChangesAsync();
  }
}