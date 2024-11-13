using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TPIndustriaBD2.Data;

namespace TPIndustriaBD2.Controllers
{
    public class SetorController : Controller
    {
        private readonly DataAcess _dataAcess;
        private SqlConnection _connection;
        public SetorController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }
        public IActionResult Index()
        {
            var setores = _dataAcess.ListarSetores();
            return View(setores);
        }

        public IActionResult ProdutosConsumidosPorSetor()
        {
            var setoresComProdutos = _dataAcess.ListarProdutosConsumidosPorSetor();
            return View(setoresComProdutos);
        }

        [HttpGet]
        public IActionResult ExibirConsumosPorSetorEspecifico(int id)
        { 
            var consumosPorSetor = _dataAcess.BuscarConsumosPorSetorEspecifico(id);

            return View("ExibirConsumosPorSetorEspecifico", consumosPorSetor);
        }

    }
}
