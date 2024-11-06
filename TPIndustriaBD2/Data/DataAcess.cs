using System;
using System.Collections.Generic; 
using System.Data.SqlClient;
using TPIndustriaBD2.Models;
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
    }
}
