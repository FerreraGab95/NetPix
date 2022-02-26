using System;
using System.Collections.Generic;
using System.Text;

namespace NetPix.EMVQR
{
    public interface IEMVQR
    {
        /// <summary>
        /// Formata os dados de pagamento e gera o payload. Caso os dados não sejam válidos, uma 
        /// exceão do tipo FormatException será lançada;
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public string GeneratePayload();
    }
}
