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

    public static void CadastrarGeneroNoSistema(Genero generoParaCadastrar)
    {
        int linhasAfetadas;
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "INSERT INTO Generos (nome, descricao) VALUES (@nome, @descricao)";
                comando.Parameters.AddWithValue("@nome", generoParaCadastrar.Nome);
                comando.Parameters.AddWithValue("@descricao", generoParaCadastrar.Descricao);
                linhasAfetadas = comando.ExecuteNonQuery();
            }

            if (linhasAfetadas > 0)
                Console.WriteLine($"Gênero {generoParaCadastrar.Nome} cadastrado com sucesso!");
            else
                throw new ErroNovoRegistroExcpetion($"Erro ao cadastrar gênero {generoParaCadastrar.Nome}");
        }
        catch (SQLiteException e)
        { 
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
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
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
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
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
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
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }

    public static void AtualizarUmGenero(Genero generoParaAtualizar)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      UPDATE Generos
                                      SET nome=@nome, descricao=@descricao
                                      WHERE id=@id
                                      """;
                comando.Parameters.AddWithValue("@nome", generoParaAtualizar.Nome);
                comando.Parameters.AddWithValue("@descricao", generoParaAtualizar.Descricao);
                comando.Parameters.AddWithValue("@id", generoParaAtualizar.Id);
                comando.ExecuteNonQuery();
                Console.WriteLine("Gênero foi atualizado com sucesso!");
            }
        }
        catch (SQLiteException e)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
    public static void DeletarUmGeneroPorId(int id)
    {
        try
        {
            int linhasAfetadas;
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Generos WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                linhasAfetadas = comando.ExecuteNonQuery();
            }

            if (linhasAfetadas > 0)
                Console.WriteLine($"Gênero de id: {id} deletado com sucesso da base de dados!");
            else
                throw new RegistroNaoEcontradoException(
                    $"Gênero de id: {id} não foi encontrado na base de dados para ser excluído!");
        }
        catch (SQLiteException e)
        {
            //Console.WriteLine(e.Message);
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }

    public static void DeletarUmGeneroPeloNome(string nome)
    {
        try
        {
            int linhasAfetadas;
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Generos WHERE nome = @nome";
                comando.Parameters.AddWithValue("@nome", nome);
                linhasAfetadas = comando.ExecuteNonQuery();
            }
            if (linhasAfetadas > 0)
                Console.WriteLine($"Gênero de nome: {nome} deletado com sucesso da base de dados!");
            else
                throw new RegistroNaoEcontradoException(
                    $"Gênero de nome: {nome} não foi encontrado na base de dados para ser excluído!");
        }
        catch (SQLiteException e)
        {
            //Console.WriteLine(e.Message);
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
    #endregion

    // ---------------------------------------------------------------------------------------------------------------\\
    
    #region Operações relacionadas a tabela de clientes

    public static void CadastrarCliente(Cliente clienteParaCadastar)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "INSERT INTO Clientes(nome, cpf, telefone, endereco) " +
                                      "VALUES(@nome, @cpf, @telefone, @endereco)";
                comando.Parameters.AddWithValue("@nome", clienteParaCadastar.Nome);
                comando.Parameters.AddWithValue("@cpf", clienteParaCadastar.Cpf);
                comando.Parameters.AddWithValue("@telefone", clienteParaCadastar.Telefone);
                comando.Parameters.AddWithValue("@endereco", clienteParaCadastar.Endereco);
                if (comando.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Novo cliente inserido com sucesso!");
                }
                else
                {
                    throw new ErroNovoRegistroExcpetion(
                        "Falha ao cadastrar cliente! Verifique todos os campos e tente novamente!");
                }
            }
        }
        catch (Exception e)
        {
                Console.WriteLine(e);
                throw;
        }
    }
    
    public static List<Cliente> ExibirTodosClientes()
    {
        List<Cliente> clientes = new List<Cliente>();
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Clientes";
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        clientes.Add(
                            new Cliente(
                                Convert.ToInt32(leitor["id"]),
                                leitor["nome"].ToString(),
                                leitor["cpf"].ToString(),
                                leitor["telefone"].ToString(),
                                leitor["endereco"].ToString(),
                                Convert.ToBoolean(leitor["ativo"])
                            )
                        );
                    }
                    return clientes;
                }
                throw new RegistroNaoEcontradoException("Não foi encontrado nenhum cliente na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }

    public static List<Cliente> ExibirClientePorNome(string nome)
    {
        List<Cliente> clientes = new List<Cliente>();
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Clientes WHERE nome LIKE @nome";
                comando.Parameters.AddWithValue("@nome", $"{nome}%");
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        clientes.Add(new Cliente(
                            Convert.ToInt32(leitor["id"]),
                            leitor["nome"].ToString(),
                            leitor["cpf"].ToString(),
                            leitor["telefone"].ToString(),
                            leitor["endereco"].ToString(),
                            Convert.ToBoolean(leitor["ativo"])
                        ));
                    }
                    return clientes;
                }
                throw new RegistroNaoEcontradoException($"Não foi encontrado nenhum cliente com o nome '{nome}' na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
    public static Cliente ExibirClientePeloId(int id)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Clientes WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                using var leitor = comando.ExecuteReader();
                if (leitor.Read())
                {
                    return new Cliente(
                        Convert.ToInt32(leitor["id"]),
                        leitor["nome"].ToString(),
                        leitor["cpf"].ToString(),
                        leitor["telefone"].ToString(),
                        leitor["endereco"].ToString(),
                        Convert.ToBoolean(leitor["ativo"])
                    );
                }
                throw new RegistroNaoEcontradoException($"Não foi encontrado nenhum cliente com o id: {id} na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
    public static Cliente ExibirClientePeloCpf(string cpf)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Clientes WHERE cpf = @cpf";
                comando.Parameters.AddWithValue("@cpf", cpf);
                using var leitor = comando.ExecuteReader();
                if (leitor.Read())
                {
                    return new Cliente(
                        Convert.ToInt32(leitor["id"]),
                        leitor["nome"].ToString(),
                        leitor["cpf"].ToString(),
                        leitor["telefone"].ToString(),
                        leitor["endereco"].ToString(),
                        Convert.ToBoolean(leitor["ativo"])
                    );
                }
                throw new RegistroNaoEcontradoException($"Não foi encontrado nenhum cliente com o cpf '{cpf}' na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }

    public static void DesativarClientePeloId(int id)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "UPDATE Clientes SET ativo = false WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                Console.WriteLine($"Cliente de id: {id} desativado com sucesso!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException("Erro manipular banco de dados! Entre em contato com o suporte!");
        }
    }
    
    #endregion
    
    
}