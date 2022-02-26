using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NetPix.EMVQR;
using QRCoder;
using System.IO;

namespace NetPix
{
    public class NetPixGenerator
    {
        private QRCodeGenerator qRCodeGenerator;


        public NetPixGenerator()
        {
            qRCodeGenerator = new QRCodeGenerator();
        }


        /// <summary>
        /// Retorna um Bitmap do QRCode de pagamento.
        /// </summary>
        /// <returns></returns>
        public Bitmap GerarQRCode(IEMVQR eMVQR)
        {
            QRCodeData qRData = qRCodeGenerator.CreateQrCode(eMVQR.GeneratePayload(), QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRData);
            return qRCode.GetGraphic(20);
        }


        /// <summary>
        /// Exporta o QRCode de pagamento para o diretório informado.
        /// </summary>
        /// <param name="diretorio"></param>
        /// <returns></returns>
        public void ExportarQRCode(IEMVQR eMVQR, string nomeArquivo)
        {
            Bitmap qrBitmap = GerarQRCode(eMVQR);
            qrBitmap.Save(nomeArquivo);
        }


        /// <summary>
        /// Gera os dados de pagamento como uma string (Pix copia e cola).
        /// </summary>
        /// <returns></returns>
        public string GerarPayload(IEMVQR eMVQR)
        {
            return eMVQR.GeneratePayload();
        }
    }
}
