using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Mre.Visas.Visa.Application.VisaElectronica.Commands;
using Mre.Visas.Visa.Application.VisaElectronica.Queries;
using Mre.Visas.Visa.Application.VisaElectronica.Requests;

using System.Threading.Tasks;
using System;
using System.Drawing.Imaging;
using ZXing;
using System.Drawing;
using Mre.Visas.Visa.Application.Wrappers;

namespace Mre.Visas.Visa.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VisaElectronicaController : BaseController
  {
    [HttpPost("CrearVisaElectronica")]
    [ActionName(nameof(CrearVisaElectronicaAsync))]
    public async Task<IActionResult> CrearVisaElectronicaAsync(CrearVisaElectronicaRequest request)
    {
      var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      var configuration = builder.Build();
      request.NumeroInicial = Convert.ToInt64(configuration["ConfiguracionVisas:NumeroInicial"].ToString());

      return Ok(await Mediator.Send(new CrearVisaElectronicaCommand(request)).ConfigureAwait(false));
    }

    [HttpPost("ActualizarVisaElectronica")]
    [ActionName(nameof(ActualizarVisaElectronicaAsync))]
    public async Task<IActionResult> ActualizarVisaElectronicaAsync(ActualizarVisaElectronicaRequest request)
    {
      return Ok(await Mediator.Send(new ActualizarVisaElectronicaCommand(request)).ConfigureAwait(false));
    }



    [HttpGet("ObtenerCodigoBarras")]
    [ActionName(nameof(ObtenerCodigoBarras))]
    public async Task<IActionResult> ObtenerCodigoBarras(string cadena, int tipoCodigo)
    {
      ObtenerCodigoBarrasRequest request = new()
      {
        Cadena = cadena,
        TipoCodigo = tipoCodigo
      };
      return Ok(await Mediator.Send(new ObtenerCodigoBarrasQuery(request)).ConfigureAwait(false));
    }

    // POST: api/Tramite/ConsultarTramitePorId
    [HttpPost("ConsultarVisaElectronicaPorTramiteId")]
    [ActionName(nameof(ConsultarVisaElectronicaPorTramiteIdAsync))]
    public async Task<ApiResponseWrapper<Domain.Entities.VisaElectronica>> ConsultarVisaElectronicaPorTramiteIdAsync(ConsultarVisaElectronicaPorTramiteIdRequest request)
    {
      return await Mediator.Send(new ConsultarVisaElectronicaPorTramiteIdQuery(request)).ConfigureAwait(false);
    }
    [HttpGet("GenerarCodigoQr")]
    [ActionName(nameof(GenerarCodigoQrAsync))]
    public string GenerarCodigoQrAsync(string numero)
    {
      Bitmap aztecBitmap;
      var barcodeWriter = new BarcodeWriter();
      barcodeWriter.Format = BarcodeFormat.QR_CODE;
      barcodeWriter.Options.PureBarcode = true;
      aztecBitmap = barcodeWriter.Write(numero);

      System.IO.MemoryStream ms = new MemoryStream();
      aztecBitmap.Save(ms, ImageFormat.Jpeg);
      byte[] byteImage = ms.ToArray();
      var SigBase64 = Convert.ToBase64String(byteImage);
      return SigBase64;
    }
    [HttpGet("GenerarBase64CodigoBarras")]
    [ActionName(nameof(GenerarBase64CodigoBarrasAsyn))]
    public string GenerarBase64CodigoBarrasAsyn(string cadena)
    {
      string base64 = string.Empty;
      BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
      codigo.IncludeLabel = true;
      var imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, cadena, 300, 150);

      System.IO.MemoryStream ms = new MemoryStream();
      imagen.Save(ms, ImageFormat.Jpeg);
      byte[] byteImage = ms.ToArray();
      var SigBase64 = Convert.ToBase64String(byteImage);
      return SigBase64;
    }


  }
}
