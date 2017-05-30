using System.Collections.Generic;
using System.Linq;

namespace api_gateway_core.Models
{
    public class Cliente
    {
        public string _id { get; set; }
        public string  nome { get; set; }

        public IEnumerable<Pedido> Pedidos { get; set; }
    }
}
