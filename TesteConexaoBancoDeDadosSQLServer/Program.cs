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
            var tempoConexao = new Stopwatch();
            var tempoDeExecucaoQuery = new Stopwatch();
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "endereco_servidor";
                builder.UserID = "seu_usuario";
                builder.Password = "sua_senha";
                builder.InitialCatalog = "sua_base_de_dados";
                var tabela = "sua_tabela";

                
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    tempoConexao.Start();
                    connection.Open();
                    tempoConexao.Stop();

                    var sql = $"SELECT TOP 1 1 FROM {tabela}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        tempoDeExecucaoQuery.Start();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"A consulta foi executada com sucesso e retornou o valor de {reader.GetValue(0)}");
                                tempoDeExecucaoQuery.Stop();
                            }
                        }

                        Console.WriteLine($"O tempo para conectar na base foi de {tempoConexao.Elapsed.TotalSeconds} segundos e o tempo de resposta da consulta no banco de dados foi de {tempoDeExecucaoQuery.Elapsed.TotalSeconds} segundos");
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

