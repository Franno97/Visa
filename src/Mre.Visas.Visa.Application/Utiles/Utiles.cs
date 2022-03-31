using System;
using System.IO;
using QRCoder;
namespace Mre.Visas.Visa.Application.Utiles
{
    /// <summary>
    /// Clase con métodos utiles
    /// </summary>
    public class Utiles
    {

        /// <summary>
        /// Método que genera ina imagen de un código de barras
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static System.Drawing.Image GenerarImagenCodigoBarras(string cadena)
        {
            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = true;
            var imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, cadena, 300, 150);
            return imagen;
        }

        /// <summary>
        /// Método que genera una imgan de un código QR
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap GenerarImagenCodigoQR(string cadena)
        {
            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(cadena, QRCodeGenerator.ECCLevel.Q, true);
            QRCode qrCode = new QRCode(_qrCodeData);

            return qrCode.GetGraphic(20);
        }

        /// <summary>
        /// Método que devuelve una cadena Base64String de un archivo de imagen
        /// </summary>
        /// <param name="imagen"></param>
        /// <returns></returns>
        public static string GenerarStringDesdeImagen(System.Drawing.Image imagen)
        {
            using (MemoryStream m = new MemoryStream())
            {
                imagen.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = m.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

    }
}
