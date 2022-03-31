using FluentValidation;
using MediatR;
using Mre.Visas.Visa.Application.Shared.Handlers;
using Mre.Visas.Visa.Application.Shared.Interfaces;
using Mre.Visas.Visa.Application.VisaElectronica.Requests;
using Mre.Visas.Visa.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mre.Visas.Visa.Application.VisaElectronica.Queries
{
  public class ConsultarVisaElectronicaPorTramiteIdQuery : ConsultarVisaElectronicaPorTramiteIdRequest, IRequest<ApiResponseWrapper<Domain.Entities.VisaElectronica>>
  {
    public ConsultarVisaElectronicaPorTramiteIdQuery(ConsultarVisaElectronicaPorTramiteIdRequest request)
    {
      TramiteId = request.TramiteId;
    }

    public class ConsultarTramitePorIdQueryHandler : BaseHandler, IRequestHandler<ConsultarVisaElectronicaPorTramiteIdQuery, ApiResponseWrapper<Domain.Entities.VisaElectronica>>
    {
      public ConsultarTramitePorIdQueryHandler(IUnitOfWork unitOfWork)
          : base(unitOfWork)
      {
      }

      public async Task<ApiResponseWrapper<Domain.Entities.VisaElectronica>> Handle(ConsultarVisaElectronicaPorTramiteIdQuery query, CancellationToken cancellationToken)
      {
        var tramite = await UnitOfWork.VisaElectronicaRepository.GetByTramiteId(query.TramiteId);
        var response = new ApiResponseWrapper<Domain.Entities.VisaElectronica>(HttpStatusCode.OK, tramite);

        return response;
      }
    }
  }

  public class ConsultarTramitePorIdQueryValidator : AbstractValidator<ConsultarVisaElectronicaPorTramiteIdQuery>
  {
    public ConsultarTramitePorIdQueryValidator()
    {
      RuleFor(e => e.TramiteId)
          .NotEmpty().WithMessage("{PropertyName} es requerdio.")
          .NotNull().WithMessage("{PropertyName} no puede ser nulo.");
    }
  }
}
