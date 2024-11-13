using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection;
using TPIndustriaBD2.Data;
using TPIndustriaBD2.Models;
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

        public IActionResult ProdutoDetails(int Id)
        {

            var produtosDetails = _dataAcess.DetalharProduto(Id);

            return View(produtosDetails);
        }

        public IActionResult ListarProdutosPorGrupo()
        {
            var produtosGrupo = _dataAcess.ListarProdutosGrupos();
            return View(produtosGrupo);
        }

        public IActionResult ExibirMenorPrecoCompra(int id)
        {
            var dataAcess = new DataAcess();
            var compraMenorPreco = dataAcess.BuscarMenorPrecoCompra(id);

            if (compraMenorPreco == null)
            {
                ViewBag.Message = "Nenhuma compra encontrada para o produto.";
                return View("Error");
            }

            return View("ExibirMenorPrecoCompra", compraMenorPreco);
        }



        [HttpGet]
        public IActionResult RegistrarCompra()
        {

            var model = new RegistrarCompraVM
            {
                Fornecedor = _dataAcess.ListarFornecedores(),
                Produto = _dataAcess.ListarProdutos()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult InserirCompra(RegistrarCompraVM model)
        {
            try
            {
                _dataAcess.RegistrarCompra(
                    model.ID_Produto,
                    model.ID_Fornecedor,
                    model.Quantidade,
                    model.Valor_Compra
                );
                return RedirectToAction("Index", "Produto");

            }
            catch (SqlException ex)
            {
                model.mensagemErro =  ex.Message;
                model.Produto = _dataAcess.ListarProdutos();
                model.Fornecedor = _dataAcess.ListarFornecedores();
                return View("RegistrarCompra", model);
            }
            catch (Exception ex)
            {

                model.mensagemErro =  ex.Message;
                model.Produto = _dataAcess.ListarProdutos();
                model.Fornecedor = _dataAcess.ListarFornecedores();
                return View("RegistrarCompra", model);
            }
        }


        [HttpGet]
        public IActionResult RegistrarConsumo()
        {
            var model = new RegistrarConsumo
            {
                Setor = _dataAcess.ListarSetores(),
                Produto = _dataAcess.ListarProdutos()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult InserirConsumo(RegistrarConsumo model)
        {
            try
            {
                _dataAcess.RegistrarConsumo(
                model.ID_Produto,
                model.FK_Setor,
                model.Quantidade
            );
                return RedirectToAction("Index", "Produto");
            }
            catch (SqlException ex)
            {

                model.mensagemErro = ex.Message;
                model.Setor = _dataAcess.ListarSetores();
                model.Produto = _dataAcess.ListarProdutos();
                return View("RegistrarConsumo", model);
            }

            catch (Exception ex)
            {

                model.mensagemErro = ex.Message;
                model.Setor = _dataAcess.ListarSetores();
                model.Produto = _dataAcess.ListarProdutos();
                return View("RegistrarConsumo", model);
            }

        }
    }
}

