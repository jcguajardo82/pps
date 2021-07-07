using System;
using System.Threading.Tasks;

using Soriana.PPS.Common.DTO.ClosureOrder;

namespace Soriana.PPS.PaymentOrderProcess.ClosureOrderGrocery.Services
{
    public class ResponseXMLService : IResponseXMLService
    {
        public async Task<string> GetResponseXML(string Code, string Descripcion)
        {
            if (Code == "Bad Request")
               return await Generar_XML_ErrorCierreVta("400", Descripcion, Descripcion);
            else
                return await Generar_XML_ExitosoCierreVta();
        }

        private async Task<string> Generar_XML_ExitosoCierreVta()
        {
            try
            {
                //string strxml = "<?xml version='1.0' encoding='UTF-8'?><root><NumError>" + respuesta.NumError + "</NumError>" +
                //    "<DescEror>" + respuesta.DescEror + "</DescEror>" +
                //    "<merchId>" + respuesta.merchId + "</merchId>" +
                //    "<tipotarjeta>" + respuesta.tipotarjeta + "</tipotarjeta>" +
                //    "<NumeroTarjetadeCredito>" + respuesta.NumeroTarjetadeCredito + "</NumeroTarjetadeCredito>" +
                //    "<Orden>" + respuesta.Orden + "</Orden>" +
                //    "<TransaccionSoriana>" + respuesta.TransaccionSoriana + "</TransaccionSoriana>" +
                //    "<NumTransaccion>" + respuesta.NumTransaccion + "</NumTransaccion>" +
                //    "<IdAutorizacion>" + respuesta.IdAutorizacion + "</IdAutorizacion>" +
                //    "<merchTxnRef>" + respuesta.merchTxnRef + "</merchTxnRef>" +
                //    "<qsiResponseCode>" + respuesta.qsiResponseCode + "</qsiResponseCode>" +
                //    "<ResultadoTransaccion>" + respuesta.ResultadoTransaccion + "</ResultadoTransaccion>" +
                //    "<CSC>" + respuesta.CSC + "</CSC>" +
                //    "<importe>" + respuesta.importe + "</importe>" +
                //    "<receiptNo>" + respuesta.receiptNo + "</receiptNo>" +
                //    "<acqResponseCode>" + respuesta.acqResponseCode + "</acqResponseCode>" +
                //    "<batchNo>" + respuesta.batchNo + "</batchNo></root>";

                string strxml = "<?xml version='1.0' encoding='UTF-8'?><root><NumError>" + "</NumError>" +
                    "<DescEror>" + "</DescEror>" +
                    "<merchId>" + "</merchId>" +
                    "<tipotarjeta>" + "</tipotarjeta>" +
                    "<NumeroTarjetadeCredito>" + "</NumeroTarjetadeCredito>" +
                    "<Orden>" + "</Orden>" +
                    "<TransaccionSoriana>" + "</TransaccionSoriana>" +
                    "<NumTransaccion>" +  "</NumTransaccion>" +
                    "<IdAutorizacion>" +  "</IdAutorizacion>" +
                    "<merchTxnRef>" +  "</merchTxnRef>" +
                    "<qsiResponseCode>" +  "</qsiResponseCode>" +
                    "<ResultadoTransaccion>" + "</ResultadoTransaccion>" +
                    "<CSC>" +  "</CSC>" +
                    "<importe>" +  "</importe>" +
                    "<receiptNo>" + "</receiptNo>" +
                    "<acqResponseCode>" + "</acqResponseCode>" +
                    "<batchNo>" + "</batchNo></root>";

                return strxml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> Generar_XML_ErrorCierreVta(string NumError, string DescError, string DescClienteError)
        {
            try
            {
                string strXML = "<root><NumError>" + NumError + "</NumError>" +
                                "<DescClienteError>" + DescClienteError + "</DescClienteError>" +
                                "<DescEror>" + DescError + "</DescEror></root>";

                return strXML;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
