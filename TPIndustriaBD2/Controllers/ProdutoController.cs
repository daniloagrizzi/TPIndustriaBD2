using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection;
using TPIndustriaBD2.Data;
using TPIndustriaBD2.Models.ViewModels;

namespace TPIndustriaBD2.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly DataAcess _dataAcess;
        private SqlConnection _connection;
        public ProdutoController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }
        public IActionResult Index()
        {
            var produtos = _dataAcess.ListarProdutos();
            return View(produtos);
        }

        [HttpGet]
        public IActionResult RegistrarCompra()
        {

            var model = new RegistrarCompraVM
            {
                Fornecedor = _dataAcess.ListarFornecedores()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult InserirCompra(RegistrarCompraVM model)
        {

            _dataAcess.RegistrarCompra(
                model.ID_Produto,
                model.ID_Fornecedor,
                model.Quantidade,
                model.Valor_Compra
            );

            return RedirectToAction("RegistrarCompra");

        }
    }
}
