using System.Collections.Generic;
using System.Linq;

namespace api_gateway_core.Models
{
    public class Pedido
    {
        public string IdCliente { get; set; }
        public int IdProduto { get; set; }
        public short Quantidade { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
