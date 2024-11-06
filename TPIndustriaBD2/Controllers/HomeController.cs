using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TPIndustriaBD2.Data;
using TPIndustriaBD2.Models;

namespace TPIndustriaBD2.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAcess _dataAcess;

        public HomeController(DataAcess dataAcess)
        {
            _dataAcess = dataAcess;
        }

        public IActionResult Index()
        {
            var fornecedores = _dataAcess.ListarFornecedores();
            return View(fornecedores);
        }
    }
}
