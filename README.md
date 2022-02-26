# NetPix
Biblioteca de geração de QR Codes e payloads para PIXs estáticos. Esta biblioteca é uma simplificação da biblioteca [emv-qrcps.net](https://github.com/FerreraGab95/emv-qrcps.net)
para este uso específico.

### Utilização
```csharp
using NetPix.EMVQR;
using NetPix.PaymentData;
using NetPix;

NetPixGenerator pixGenerator = new NetPixGenerator();

PixEstaticoPaymentData paymentData = new PixEstaticoPaymentData()
{
    NomeBeneficiario = "Fulano de Tal",
    TipoChave = PixTipoChave.CPF,
    Chave = "12345678901",
    CidadeBeneficiario = "Cidade Ficticia",
    CampoAdicional = "PAGAMENTOID001",
    Valor = 12.34m
};

PixEstaticoEMVQR emvqr = new PixEstaticoEMVQR(paymentData);

//Gera o payload de pagamento (Pix copia e cola).
string payload = pixGenerator.GerarPayload(emvqr);

//Retorna uma Bitmap(System.Drawing.Bitmap) da imagem do QR Code.
Bitmap imagem = pixGenerator.GerarQRCode(emvqr)

//Exporta a imagem do QR Code no formato BMP;.
pixGenerator.ExportarQRCode(emvqr, [DiretorioQRCode]);

```

