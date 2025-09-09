using System.Data.SQLite;
using Locadora_ADO.NET.Exceptions;
using Locadora_ADO.NET.ML;

namespace Locadora_ADO.NET.DAL;

public class LocadoraDAL
{
    private static string path = Directory.GetCurrentDirectory() + "/Locadora.db";
    private static SQLiteConnection _sqLiteConnection;

    private static SQLiteConnection DbConnection()
    {
        _sqLiteConnection = new SQLiteConnection($"Data Source={path}");
        //Console.WriteLine(path);
        _sqLiteConnection.Open();
        return _sqLiteConnection;
    }

    #region Operações relacionadas a tabela gêneros

    public static List<Genero> ListarTodosOsGeneros()
    {
        try
        {
            List<Genero> generosEncontrados = new List<Genero>();
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Generos";
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        Genero generoParaInsercao = new Genero(
                            Convert.ToInt32(leitor["id"]),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString()
                        );
                        generosEncontrados.Add(generoParaInsercao);
                    }
                    return generosEncontrados;
                }
                throw new RegistroNaoEcontradoException("Não há registro de Gêneros no banco");
            }
        }
        catch (RegistroNaoEcontradoException e)
        {
            throw e;
        } 
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro ao manipular banco de dados!");
        }
    }

    public static Genero ExibirUmGeneroPorId(int id)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Generos WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                using (var leitor = comando.ExecuteReader())
                {
                    if (leitor.Read())
                        return new Genero(
                            Convert.ToInt32(leitor["id"]),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString()
                            );
                    
                    throw new RegistroNaoEcontradoException($"Não há nenhum registro de gênero com esse id: {id}");
                }
            }
        }
        catch (RegistroNaoEcontradoException e)
        {
            throw e;
        } 
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro ao manipular banco de dados!");
        }
    }
    
    public static Genero ExibirUmGeneroPorNome(string nome)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Generos WHERE nome = @nome";
                comando.Parameters.AddWithValue("@nome", nome);
                using (var leitor = comando.ExecuteReader())
                {
                    if (leitor.Read())
                        return new Genero(
                            Convert.ToInt32(leitor["id"]),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString()
                        );
                    throw new RegistroNaoEcontradoException($"Não há nenhum registro de gênero com esse nome: {nome}");
                }
            }
        }
        catch (RegistroNaoEcontradoException e)
        {
            throw e;
        } 
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro ao manipular banco de dados!");
        }
    }
    
    #endregion
}