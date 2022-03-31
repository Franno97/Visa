using MediatR;
using FluentValidation;
using Mre.Visas.Visa.Application.VisaElectronica.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mre.Visas.Visa.Application.Wrappers;
using Mre.Visas.Visa.Application.Shared.Handlers;
using Mre.Visas.Visa.Application.Shared.Interfaces;
using System.Threading;
using System.Net;

namespace Mre.Visas.Visa.Application.VisaElectronica.Queries
{
    public class CrearVisaElectronicaQuery
    {
    }

    #region Configuracion
    public class ObtenerCodigoBarrasQuery : ObtenerCodigoBarrasRequest, IRequest<ApiResponseWrapper>
    {
        public ObtenerCodigoBarrasQuery(ObtenerCodigoBarrasRequest request)
        {
            Cadena = request.Cadena;
            TipoCodigo = request.TipoCodigo;
        }

        public class ObtenerCodigoBarrasHandler : BaseHandler, IRequestHandler<ObtenerCodigoBarrasQuery, ApiResponseWrapper>
        {
            public ObtenerCodigoBarrasHandler(IUnitOfWork unitOfWork)
                : base(unitOfWork)
            {
            }

            public async Task<ApiResponseWrapper> Handle(ObtenerCodigoBarrasQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    string stringCodigoBarras;
                    if (query.TipoCodigo == (int)Domain.Enums.TipoCodigo.Tipo.CodigoBarras)
                    {
                        var imagenCodigoBarras = Utiles.Utiles.GenerarImagenCodigoBarras(query.Cadena);
                        stringCodigoBarras = Utiles.Utiles.GenerarStringDesdeImagen(imagenCodigoBarras);
                    }
                    else
                    {
                        var imagenCodigoQr = Utiles.Utiles.GenerarImagenCodigoQR(query.Cadena);
                        stringCodigoBarras = Utiles.Utiles.GenerarStringDesdeImagen(imagenCodigoQr);
                    }

                    var response = new ApiResponseWrapper(HttpStatusCode.OK, stringCodigoBarras);

                    return response;

                }
                catch (System.Exception ex)
                {

                    return new ApiResponseWrapper(HttpStatusCode.BadRequest, ex.Message == null ? ex.InnerException : ex.Message);
                }

            }
        }
    }

    public class ObtenerCodigoBarrasValidator : AbstractValidator<ObtenerCodigoBarrasQuery>
    {
        public ObtenerCodigoBarrasValidator()
        {
            //no aplica validadores
            //RuleFor(e => e.Value)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull().WithMessage("{PropertyName} must not be null.");
        }
    }
    #endregion
}
