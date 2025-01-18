using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Serialization;

namespace PruebaTecnicaSeti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        [HttpPost("GenerarPedidoXml")]
        public IActionResult GenerarPedidoXml(List<PedidoDto> peticion)
        {
            return Content(ConvertirJsonXmlSoap(peticion), "application/soap+xml");
        }

        [HttpPost("GenerarRespuestaJson")]
        [Consumes("application/xml")]
        public IActionResult GenerarPedidoXml(string xml)
        {
            //StringReader textReader = new StringReader(xml);
            //XmlSerializer xmlSerializador = new XmlSerializer(typeof(EnvioPedidoEnvelope));
            //EnvioPedidoEnvelope des = (EnvioPedidoEnvelope)xmlSerializador.Deserialize(textReader);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nodoCodigo = doc.DocumentElement.GetElementsByTagName("Codigo");
            XmlNodeList nodoMensaje = doc.DocumentElement.GetElementsByTagName("Mensaje");

            EnvioPedidoAcmeResponse resultado = new()
            {
                EnvioPedidoResponse = new()
                {
                    Codigo = nodoCodigo.Count > 0 ? nodoCodigo[0].InnerText : "",
                    Mensaje = nodoMensaje.Count > 0 ? nodoMensaje[0].InnerText : ""
                }
            };

            return Ok(resultado);
        }

        private string ConvertirJsonXmlSoap(List<PedidoDto> pedidos)
        {
            var xmlDocument = new XmlDocument();

            XmlElement envelope = xmlDocument.CreateElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlElement header = xmlDocument.CreateElement("soapenv", "Header", "http://schemas.xmlsoap.org/soap/envelope/");
            envelope.AppendChild(header);

            XmlElement body = xmlDocument.CreateElement("soapenv", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            envelope.AppendChild(body);
            xmlDocument.AppendChild(envelope);

            XmlElement productosElement = xmlDocument.CreateElement("env", "EnvioPedidoAcme", "http://WSDLs/EnvioPedidos/EnvioPedidoAcme");
            body.AppendChild(productosElement);

            foreach (PedidoDto producto in pedidos)
            {
                XmlElement pedidoElement = xmlDocument.CreateElement("EnvioPedidoRequest");
                XmlElement idElement = xmlDocument.CreateElement("pedido");
                idElement.InnerText = producto.numPedido.ToString();
                pedidoElement.AppendChild(idElement);

                XmlElement cantidadElement = xmlDocument.CreateElement("Cantidad");
                cantidadElement.InnerText = producto.cantidadPedido.ToString();
                pedidoElement.AppendChild(cantidadElement);

                XmlElement eanElement = xmlDocument.CreateElement("EAN");
                eanElement.InnerText = producto.codigoEAN;
                pedidoElement.AppendChild(eanElement);

                XmlElement productoElement = xmlDocument.CreateElement("Producto");
                productoElement.InnerText = producto.cantidadPedido.ToString();
                pedidoElement.AppendChild(productoElement);

                XmlElement cedulaElement = xmlDocument.CreateElement("Cedula");
                cedulaElement.InnerText = producto.numDocumento;
                pedidoElement.AppendChild(cedulaElement);

                XmlElement direccionElement = xmlDocument.CreateElement("Cedula");
                direccionElement.InnerText = producto.direccion;
                pedidoElement.AppendChild(direccionElement);

                productosElement.AppendChild(pedidoElement);
            }

            return xmlDocument.OuterXml;
        }
    }
}
