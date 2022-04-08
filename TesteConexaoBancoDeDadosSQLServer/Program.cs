using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace TesteConexaoBancoDeDadosSQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Início do Teste de conexão com banco de dados");
            var tempo = new Stopwatch();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "endereco_servidor";
                builder.UserID = "seu_usuario";
                builder.Password = "sua_senha";
                builder.InitialCatalog = "sua_base_de_dados";

                tempo.Start();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    var sql = "SELECT TOP 1 1 FROM sua_tabela";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"A consulta foi executada com sucesso e retornou o valor de {reader.GetValue(0)}");
                                tempo.Stop();
                            }
                        }

                        Console.WriteLine($"O tempo de resposta da consulta no banco de dados foi de {tempo.Elapsed.TotalSeconds} segundos");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Ocorreu um erro ao tentar se conectar ao banco de dados {e.ToString()}");
            }
        }
    }
    }
}
