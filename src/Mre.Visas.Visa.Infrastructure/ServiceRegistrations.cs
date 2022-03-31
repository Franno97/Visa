using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mre.Visas.Visa.Application.Repositories;
using Mre.Visas.Visa.Application.Shared.Interfaces;
using Mre.Visas.Visa.Application.VisaElectronica.Repositories;
using Mre.Visas.Visa.Application.Shared.Interfaces;
//using Mre.Visas.Visa.Application.VisaElectronica.Repositories;
//using Mre.Visas.Visa.Application.RolEstado.Repositories;
using Mre.Visas.Visa.Infrastructure.Persistence.Contexts;
using Mre.Visas.Visa.Infrastructure.Shared.Interfaces;
using Mre.Visas.Visa.Infrastructure.Shared.Repositories;
using Mre.Visas.Visa.Infrastructure.VisaElectronica.Repositories;

namespace Mre.Visas.Visa.Infrastructure
{
  public static class ServiceRegistrations
  {
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(
          options => options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"),
          options => options.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
      

      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

      services.AddTransient<IUnitOfWork, UnitOfWork>();

      services.AddTransient<IVisaElectronicaRepository, VisaElectronicaRepository>();

      //services.AddTransient<IRolEstadoRepository, RolEstadoRepository>();

      //services.AddTransient<IMovimientoRepository, MovimientoRepository>();

    }
  }
}