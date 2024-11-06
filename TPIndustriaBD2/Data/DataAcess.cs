using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using TPIndustriaBD2.Models;
using TPIndustriaBD2.Models.ViewModels;
using Microsoft.Extensions.Configuration; 

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
    }
}
