using System.Xml.Serialization;

namespace PruebaTecnicaSeti
{
    public class PedidoDto
    {
        public int numPedido { get; set; }
        public int cantidadPedido { get; set; }
        public string codigoEAN { get; set; }
        public string nombreProducto { get; set; }
        public string numDocumento { get; set; }
        public string direccion { get; set; }
    }
}
