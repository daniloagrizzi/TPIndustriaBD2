using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using TPIndustriaBD2.Models;
using TPIndustriaBD2.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace TPIndustriaBD2.Data
{
    public class DataAcess
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Fornecedor> ListarFornecedores()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[ListarFornecedores]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor.ID_Fornecedor = Convert.ToInt32(reader["ID_Fornecedor"]);
                    fornecedor.Nome_Fornecedor = reader["Nome_Fornecedor"].ToString();
                    fornecedor.CNPJ = reader["CNPJ"].ToString();
                    fornecedor.Contato = reader["Contato"].ToString();

                    fornecedores.Add(fornecedor);
                }
            }

            return fornecedores;
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> produtos = new List<Produto>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[ListarProdutos]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Produto produto = new Produto();
                    produto.ID_Produto = Convert.ToInt32(reader["ID_Produto"]);
                    produto.Nome_Produto = reader["Nome_Produto"].ToString();
                    produto.Preco_medio = Convert.ToDecimal(reader["Preco_Medio"]);
                    produto.Saldo = Convert.ToInt32(reader["Saldo"]);

                    produtos.Add(produto);
                }
            }

            return produtos;
        }


        public List<ProdutoDetailVM> DetalharProduto(int Id)
        {
            List<ProdutoDetailVM> produtoDetails = new List<ProdutoDetailVM>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[FichaProduto]";

                _command.Parameters.AddWithValue("@ID_Produto", Id);

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    ProdutoDetailVM produtoDetail = new ProdutoDetailVM();
                    produtoDetail.NomeProduto = reader["Produto"] != DBNull.Value ? reader["Produto"].ToString() : null;
                    produtoDetail.NomeFornecedor = reader["Fornecedor"] != DBNull.Value ? reader["Fornecedor"].ToString() : null;
                    produtoDetail.ValorConsumido = reader["ValorConsumido"] != DBNull.Value ? Convert.ToDecimal(reader["ValorConsumido"]) : 0;
                    produtoDetail.ValorCompra = reader["ValorCompra"] != DBNull.Value ? Convert.ToDecimal(reader["ValorCompra"]) : 0;

                    produtoDetail.DiaConsumido = reader["DiaConsumido"] != DBNull.Value ? reader["DiaConsumido"].ToString() : null;
                    produtoDetail.DiaCompra = reader["DiaCompra"] != DBNull.Value ? reader["DiaCompra"].ToString() : null;

                    produtoDetail.QuantidadeConsumida = reader["QuantidadeConsumida"] != DBNull.Value ? Convert.ToInt32(reader["QuantidadeConsumida"]) : 0;
                    produtoDetail.QuantidadeComprada = reader["QuantidadeComprada"] != DBNull.Value ? Convert.ToInt32(reader["QuantidadeComprada"]) : 0;

                    produtoDetails.Add(produtoDetail);
                }
            }

            if (produtoDetails.Count == 0)
            {
                Console.WriteLine("Nenhum detalhe de produto encontrado.");
            }

            return produtoDetails;
        }






        public List<FornecedoresEnderecosVM> ListarFornecedoresEnderecos()
        {
            List<FornecedoresEnderecosVM> fornecedoresEnderecos = new List<FornecedoresEnderecosVM>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.Text;
                _command.CommandText = "Select * From ListarFornecedoresComEndereco";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    FornecedoresEnderecosVM fornecedorEndereco = new FornecedoresEnderecosVM();
                    fornecedorEndereco.ID_Fornecedor = Convert.ToInt32(reader["ID_Fornecedor"]);
                    fornecedorEndereco.Nome_Fornecedor = reader["Nome_Fornecedor"].ToString();
                    fornecedorEndereco.CNPJ = reader["CNPJ"].ToString();
                    fornecedorEndereco.Contato = reader["Contato"].ToString();
                    fornecedorEndereco.CEP = reader["CEP"].ToString();
                    fornecedorEndereco.Rua = reader["Rua"].ToString();
                    fornecedorEndereco.Numero = Convert.ToInt32(reader["Numero"]);

                    fornecedoresEnderecos.Add(fornecedorEndereco);
                }
            }

            return fornecedoresEnderecos;
        }


        public List<ListarProdutosGruposVM> ListarProdutosGrupos()
        {
            List<ListarProdutosGruposVM> listarProdutosGrupos = new List<ListarProdutosGruposVM>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.Text;
                _command.CommandText = "Select * From ProdutosPorGrupo";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    ListarProdutosGruposVM listarProdutosGrupo = new ListarProdutosGruposVM();
                    listarProdutosGrupo.Nome_Produto = reader["Produto"].ToString();
                    listarProdutosGrupo.Nome_Grupo = reader["Grupo"].ToString();
                    listarProdutosGrupo.Preco_Medio = Convert.ToDecimal(reader["Preco_Medio"]); ;
                    listarProdutosGrupo.Saldo = Convert.ToInt32(reader["Saldo"]);

                    listarProdutosGrupos.Add(listarProdutosGrupo);
                }
            }

            return listarProdutosGrupos;
        }

        public void RegistrarCompra(int idProduto, int idFornecedor, int quantidade, decimal valorCompra)
        {
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("RegistrarCompra", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_Produto", idProduto);
                command.Parameters.AddWithValue("@ID_Fornecedor", idFornecedor);
                command.Parameters.AddWithValue("@Quantidade", quantidade);
                command.Parameters.AddWithValue("@Valor_Compra", valorCompra);
                _connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}

