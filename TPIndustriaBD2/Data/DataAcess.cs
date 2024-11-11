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

        public List<Setor> ListarSetores()
        {
            List<Setor> setores = new List<Setor>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[ListarSetores ]";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    Setor setor = new Setor();
                    setor.ID_Setor = Convert.ToInt32(reader["ID_Setor"]);
                    setor.Nome_Setor = reader["Nome_Setor"].ToString();
                    setores.Add(setor);
                }
            }

            return setores;
        }

        public List<ListarProdutosConsumidosPorSetor> ListarProdutosConsumidosPorSetor()
        {
            List<ListarProdutosConsumidosPorSetor> resultado = new List<ListarProdutosConsumidosPorSetor>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.Text;
                _command.CommandText = "SELECT Nome_Setor, Nome_Produto FROM ProdutosConsumidosPorSetores";

                _connection.Open();
                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    var setorNome = reader["Nome_Setor"].ToString();
                    var produtoNome = reader["Nome_Produto"].ToString();

                    var setor = resultado.Find(s => s.Nome_Setor == setorNome);
                    if (setor == null)
                    {
                        setor = new ListarProdutosConsumidosPorSetor { Nome_Setor = setorNome };
                        resultado.Add(setor);
                    }
                    setor.Produtos.Add(produtoNome);
                }
            }

            return resultado;
        }


        public ProdutoDetailVM DetalharProduto(int idProduto)
        {
            ProdutoDetailVM produtoDetailVM = new ProdutoDetailVM();

            try
            {
                using (_connection = new SqlConnection(GetConnectionString()))
                {
                    _connection.Open();

                    using (_command = new SqlCommand("[dbo].[BuscarCompras]", _connection))
                    {
                        _command.CommandType = System.Data.CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@ID_Produto", idProduto);

                        SqlDataReader readerCompras = _command.ExecuteReader();
                        while (readerCompras.Read())
                        {
                            CompraViewModel compra = new CompraViewModel
                            {
                                NomeFornecedor = readerCompras["Nome_Fornecedor"].ToString(),
                                ValorCompra = readerCompras["Valor_Compra"] != DBNull.Value ? Convert.ToDecimal(readerCompras["Valor_Compra"]) : 0,
                                QuantidadeComprada = readerCompras["Quantidade"] != DBNull.Value ? Convert.ToInt32(readerCompras["Quantidade"]) : 0,
                                DiaCompra = readerCompras["Data_Compra"] != DBNull.Value ? readerCompras["Data_Compra"].ToString() : "Data não disponível"
                            };
                            produtoDetailVM.Compras.Add(compra);
                        }
                        readerCompras.Close();
                    }

                    using (_command = new SqlCommand("[dbo].[BuscarConsumos]", _connection))
                    {
                        _command.CommandType = System.Data.CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@ID_Produto", idProduto);

                        SqlDataReader readerConsumos = _command.ExecuteReader();
                        while (readerConsumos.Read())
                        {
                            ConsumoViewModel consumo = new ConsumoViewModel
                            {
                                ValorConsumido = readerConsumos["ValorUnitario"] != DBNull.Value ? Convert.ToDecimal(readerConsumos["ValorUnitario"]) : 0,
                                QuantidadeConsumida = readerConsumos["Quantidade"] != DBNull.Value ? Convert.ToInt32(readerConsumos["Quantidade"]) : 0,
                                DiaConsumido = readerConsumos["Data_Consumo"] != DBNull.Value ? Convert.ToDateTime(readerConsumos["Data_Consumo"]).ToString("dd/MM/yyyy") : "Data não disponível",
                                Nome_Setor = readerConsumos["Nome_Setor"] != DBNull.Value ? readerConsumos["Nome_Setor"].ToString() : "Setor não disponível"
                            };
                            produtoDetailVM.Consumos.Add(consumo);
                        }
                        readerConsumos.Close();
                    }
                    using (_command = new SqlCommand("SELECT Nome_Produto FROM Produto WHERE ID_Produto = @ID_Produto", _connection))
                    {
                        _command.Parameters.AddWithValue("@ID_Produto", idProduto);
                        produtoDetailVM.NomeProduto = _command.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Erro SQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
               
            }

            return produtoDetailVM;
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
                    listarProdutosGrupo.Nome_Produto = reader["Nome_Produto"].ToString();
                    listarProdutosGrupo.Nome_Grupo = reader["Nome_Grupo"].ToString();
                    listarProdutosGrupos.Add(listarProdutosGrupo);
                }
            }

            return listarProdutosGrupos;
        }

        public List<ListarFornecedoresProdutosVM> ListarFornecedoresProdutos()
        {
            List<ListarFornecedoresProdutosVM> listarFornecedoresProdutos = new List<ListarFornecedoresProdutosVM>();

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.Text;
                _command.CommandText = "SELECT * FROM FornecedoresDeCadaPorduto";

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    ListarFornecedoresProdutosVM fornecedorProduto = new ListarFornecedoresProdutosVM();
                    fornecedorProduto.Fornecedor = reader["Fornecedor"].ToString();
                    fornecedorProduto.Produto = reader["Produto"].ToString();
                    listarFornecedoresProdutos.Add(fornecedorProduto);
                }
            }

            return listarFornecedoresProdutos;
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

        public void RegistrarConsumo(int idProduto, int fK_Setor, int quantidade)
        {
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("RegistrarConsumo", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ID_Produto", idProduto);
                command.Parameters.AddWithValue("@FK_Setor", fK_Setor);
                command.Parameters.AddWithValue("@Quantidade", quantidade);
                _connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public CompraViewModel BuscarMenorPrecoCompra(int idProduto)
        {
            CompraViewModel compraMenorPreco = null;

            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[BuscarMenorPrecoCompra]";
                _command.Parameters.AddWithValue("@ID_Produto", idProduto);

                _connection.Open();

                SqlDataReader reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    compraMenorPreco = new CompraViewModel
                    {
                        NomeFornecedor = reader["Nome_Fornecedor"] !=  DBNull.Value ?  reader["Nome_Fornecedor"].ToString() : "Fornecedor não disponível",
                        ValorCompra = reader["Valor_Compra"] != DBNull.Value ? Convert.ToDecimal(reader["Valor_Compra"]) : 0,
                        QuantidadeComprada = reader["Quantidade"] != DBNull.Value ? Convert.ToInt32(reader["Quantidade"]) : 0,
                        DiaCompra = reader["Data_Compra"] != DBNull.Value ? Convert.ToDateTime(reader["Data_Compra"]).ToString("dd/MM/yyyy") : "Data não disponível"
                    };
                }
                reader.Close();
            }

            return compraMenorPreco;
        }


    }
}

