using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TPIndustriaBD2.Data;
using TPIndustriaBD2.Models.ViewModels;

namespace TPIndustriaBD2.Controllers
{

    public class FornecedorController : Controller
    {
        private readonly DataAcess _dataAcess;

        public FornecedorController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }
        public IActionResult Index()
        {
            var fornecedoresEnderecos = _dataAcess.ListarFornecedoresEnderecos();
            return View(fornecedoresEnderecos);
        }

        public IActionResult ListarFornecedoresProdutos()
        {
            var fornecedoresProdutos = _dataAcess.ListarFornecedoresProdutos();
            return View(fornecedoresProdutos);
        }

    }
}
