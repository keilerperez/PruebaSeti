using System.Xml.Serialization;

namespace PruebaTecnicaSeti
{
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvioPedidoEnvelope
    {
        [XmlElement("Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public string Header { get; set; }

        [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public EnvioPedidoBody Body { get; set; }
    }

    public class EnvioPedidoBody
    {
        [XmlElement("EnvioPedidoAcmeResponse", Namespace = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme")]
        public EnvioPedidoAcmeResponse EnvioPedidoAcmeResponse { get; set; }
    }

    public class EnvioPedidoAcmeResponse
    {
        [XmlElement("EnvioPedidoResponse")]
        public EnvioPedidoResponse EnvioPedidoResponse { get; set; }
    }

    public class EnvioPedidoResponse
    {
        [XmlElement("Codigo")]
        public string Codigo { get; set; }

        [XmlElement("Mensaje")]
        public string Mensaje { get; set; }
    }
}
