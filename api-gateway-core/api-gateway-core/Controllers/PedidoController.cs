using System.Collections.Generic;
using System.Linq;
using api_gateway_core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_gateway_core.Controllers
{
    public class PedidoController : Controller
    {
        private readonly Request.Request _request;

        public PedidoController()
        {
            _request = new Request.Request();
        }

        [Route("api/Pedido/Relatorio")]
        [HttpGet]
        public IActionResult Get()
        {
            var pedidos = BuscarPedidos();
            var clientes = BuscarClientes();
            var produtos = BuscarProdutos();

            foreach (var cliente in clientes)
            {
                cliente.Pedidos = pedidos.Where(x => x.IdCliente == cliente._id);
                foreach (var clientePedido in cliente.Pedidos)
                    clientePedido.Produtos = produtos.Where(x => x.Id == clientePedido.IdProduto);
            }

            var relatorioVendas = new RelatorioVendaDto
            {
                NomeClienteMaisComprou = clientes.OrderByDescending(x => x.Pedidos.Sum(y => y.Quantidade)).First().nome,
                NomeClienteMenosComprou = clientes.OrderBy(x => x.Pedidos.Sum(y => y.Quantidade)).First().nome,
                ValorTotalVendas = clientes.Sum(x => x.Pedidos.Sum(y => y.Produtos.Sum(z => z.Preco))),
                QuantidadeTotalProdutosVendidos = clientes.Sum(x => x.Pedidos.Sum(y => y.Quantidade))
            };

            return Ok(relatorioVendas);
        }

        [Route("api/Cliente/Pedido")]
        [HttpGet]
        public IActionResult GetClientes()
        {
            var pedidos = BuscarPedidos();
            var clientes = BuscarClientes();
            var produtos = BuscarProdutos();

            foreach (var cliente in clientes)
            {
                cliente.Pedidos = pedidos.Where(x => x.IdCliente == cliente._id);
                foreach (var clientePedido in cliente.Pedidos)
                    clientePedido.Produtos = produtos.Where(x => x.Id == clientePedido.IdProduto);
            }

            return Ok(clientes);
        }

        [Route("api/Pedido/Cliente")]
        [HttpGet]
        public IActionResult GetPedidos()
        {
            var clientes = BuscarClientes();
            var produtos = BuscarProdutos();

            foreach (var cliente in clientes)
            {
                cliente.Pedidos = BuscarPedidosPorCliente(cliente._id);
                foreach (var clientePedido in cliente.Pedidos)
                    clientePedido.Produtos = produtos.Where(x => x.Id == clientePedido.IdProduto);
            }

            return Ok(clientes);
        }

        private IEnumerable<Pedido> BuscarPedidos()
        {
            var pedidosResponse = _request.Get("http://localhost:2831/microservice/pedido");
            if (pedidosResponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Pedido>>(pedidosResponse.Content.ReadAsStringAsync().Result);
            return null;
        }

        private IEnumerable<Pedido> BuscarPedidosPorCliente(string idCliente)
        {
            var pedidosResponse = _request.Get("http://localhost:2831/microservice/pedido/cliente/" + idCliente);
            if (pedidosResponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Pedido>>(pedidosResponse.Content.ReadAsStringAsync().Result);
            return null;
        }

        private IEnumerable<Cliente> BuscarClientes()
        {
            var clientesResponse = _request.Get("http://localhost:5000/microservice/cliente/");
            if (clientesResponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cliente>>(clientesResponse.Content.ReadAsStringAsync().Result);
            return null;
        }

        private IEnumerable<Produto> BuscarProdutos()
        {
            var produtosResponse = _request.Get("http://localhost:5001/microservice/produto/");
            if (produtosResponse.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Produto>>(produtosResponse.Content.ReadAsStringAsync().Result);
            return null;
        }
    }
}
