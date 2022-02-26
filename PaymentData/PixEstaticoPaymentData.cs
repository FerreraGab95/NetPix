using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetPix.PaymentData
{
    public class PixEstaticoPaymentData : IPaymentData
    {
        public PixEstaticoPaymentData()
        {
            Chave = string.Empty;
            NomeBeneficiario = string.Empty;
            CidadeBeneficiario = string.Empty;
            Valor = 0;
            CampoAdicional = string.Empty;
        }

        public string Chave { get; set; }

        /// <summary>
        /// Nome do emissor do QRCode (Máx 25 caracteres)
        /// </summary>
        public string NomeBeneficiario { get; set; }


        /// <summary>
        /// Cidade do emissor do QRCode (Máx 15)
        /// </summary>
        public string CidadeBeneficiario { get; set; }


        public decimal Valor { get; set; }

        public string CampoAdicional { get; set; } 

        public PixTipoChave TipoChave { get; set; }


        public void Validate()
        {
            string errorMsg = string.Empty;

            errorMsg += !ValidarChave() ? $"*{TipoChave} inválido.\n" : string.Empty;
            errorMsg += !Regex.IsMatch(NomeBeneficiario, "^.{1,25}$") ? $"*Nome inválido.\n" : string.Empty;
            errorMsg += !Regex.IsMatch(CidadeBeneficiario, "^[A-Za-z\\s]{1,15}$") ? $"*Cidade inválida.\n" : string.Empty;
            errorMsg += Valor < 0 ? "*Valor inválido.\n" : string.Empty;
            errorMsg += !Regex.IsMatch(CampoAdicional.Trim(), "^.{0,20}$") ? $"*Campo adicional inválido.\n" : string.Empty;

            if (!string.IsNullOrEmpty(errorMsg))
                throw new FormatException($"Os campos a seguir possuem valores inválidos:\n{errorMsg}");
        }


        private bool ValidarChave()
        {
            switch (TipoChave)
            {
                case PixTipoChave.CPF:
                    if (!Regex.IsMatch(Chave, "^[0-9]{11}$"))
                    {
                        return false;
                    }
                    break;
                case PixTipoChave.Email:
                    if (!Regex.IsMatch(Chave, ".+\\@.+\\..+"))
                    {
                        return false;
                    }
                    break;
                case PixTipoChave.CNPJ:
                    if (!Regex.IsMatch(Chave, "^[0-9]{14}$"))
                    {
                        return false;
                    }
                    break;
                case PixTipoChave.Telefone:
                    Chave = Chave.Contains("+55") ? Chave : $"+55{Chave}"; //Adiciona o código internacional caso não possua;

                    if (!Regex.IsMatch(Chave,
                        "^(\\+55)?[\\s]?\\(?(\\d{2})?\\)?[\\s-]?(9?\\d{4}[\\s-]?\\d{4})$"))
                    {
                        return false;
                    }
                    break;
                case PixTipoChave.Aleatoria:
                    if (!Regex.IsMatch(Chave, "^.{32}$"))
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }
    }


    public enum PixTipoChave
    {
        CPF,
        Email,
        CNPJ,
        Telefone,
        Aleatoria //Até 60 caracteres;
    }
}
