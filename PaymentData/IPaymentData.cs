using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPix.PaymentData
{
    public interface IPaymentData
    {
        /// <summary>
        /// Valida os dados de pagamento, caso sejam inválidos, lança uma exceção do tipo FormatException;
        /// </summary>
        /// <exception cref="FormatException"></exception>
        void Validate();
    }
}
