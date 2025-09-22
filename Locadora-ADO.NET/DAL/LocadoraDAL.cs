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

    private static string mensagemDeErroSQLite = "Erro manipular banco de dados! Entre em contato com o suporte!";
    
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

    public static List<Cliente> ExibirTodosClientes(bool ativo)
    {
        List<Cliente> clientes = new List<Cliente>();
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Clientes WHERE ativo = @ativo";
                comando.Parameters.AddWithValue("@ativo", ativo);
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

                string mensagem = ativo ? "ativo" : "desativo";
                throw new RegistroNaoEcontradoException($"Não foi encontrado nenhum cliente {mensagem} na base de dados!");
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
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static void AlterarDadosDoCliente(Cliente cliente)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      UPDATE Clientes SET
                                            nome = @nome,
                                            telefone = @telefone,
                                            endereco = @endereco,
                                            ativo = @ativo
                                      WHERE id = @id
                                      """;
                comando.Parameters.AddWithValue("@nome", cliente.Nome);
                comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
                comando.Parameters.AddWithValue("@endereco", cliente.Endereco);
                comando.Parameters.AddWithValue("@ativo", cliente.Ativo);
                comando.Parameters.AddWithValue("@id", cliente.Id);
                if (comando.ExecuteNonQuery() > 0)
                    Console.WriteLine("Cliente atualizado com sucesso!");
                else
                    throw new ArgumentException(
                        "Falha ao alterar dados, tente novamente!\n Caso não consiga, entre em contato com o suporte");
            }
        }
        catch (SQLiteException e)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static void ExcluirClientePeloId(int id)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Clientes WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
        }
        catch (SQLiteException e)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }
    
    public static void ExcluirClientePeloCpf(string cpf)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Clientes WHERE cpf = @cpf";
                comando.Parameters.AddWithValue("@cpf", cpf);
                comando.ExecuteNonQuery();
            }
        }
        catch (SQLiteException e)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }
    
    #endregion
    
    // ---------------------------------------------------------------------------------------------------------------\\

    #region "Operações relacionadas a tabela de filmes"

    public static void ConsultarSeFilmeJáExiste(string titulo)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Filmes WHERE titulo = @titulo";
                comando.Parameters.AddWithValue("@titulo", titulo);
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                    throw new ArgumentException("Já existe esse filme na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }
    
    public static void CadastrarFilme(Filme filme)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText =
                    "INSERT INTO Filmes (titulo, sinopse, ano, idGenero) VALUES (@titulo, @sinopse, @ano, @idGenero)";
                comando.Parameters.AddWithValue("@titulo", filme.Titulo);
                comando.Parameters.AddWithValue("@sinopse", filme.Sinopse);
                comando.Parameters.AddWithValue("@ano", filme.Ano);
                comando.Parameters.AddWithValue("@idGenero", filme.Genero.Id);
                if (comando.ExecuteNonQuery() < 1) 
                    throw new ErroNovoRegistroExcpetion(
                        "Erro ao cadastrar novo filme na base de dados! Tente novamente!");
            }
        }
        catch (SQLiteException e)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    } 
    
    public static Filme ExibirFilmePorId(int id)
    {
        try
        {
            List<Filme> filmes = new List<Filme>();
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      SELECT
                                          f.id "filmesId",
                                          titulo,
                                          sinopse,
                                          ano,
                                          g.id "generosId",
                                          nome,
                                          descricao
                                      FROM Filmes f INNER JOIN Generos g ON f.idGenero = g.id 
                                      WHERE f.id = @id
                                      """;
                comando.Parameters.AddWithValue("@id", id);
                using var leitor = comando.ExecuteReader();
                if (leitor.Read())
                {
                    return new Filme(
                        Convert.ToInt32(leitor["filmesId"]),
                        leitor["titulo"].ToString(),
                        leitor["sinopse"].ToString(),
                        Convert.ToInt32(leitor["ano"]),
                        new Genero(
                            Convert.ToInt32(leitor["generosId"]),
                            leitor["nome"].ToString(),
                            leitor["descricao"].ToString()
                        ));
                }
                throw new RegistroNaoEcontradoException($"Não há nenhum registro de filme com id: {id} na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    
    public static List<Filme> ExibirTodosOsFilmes()
    {
        try
        {
            List<Filme> filmes = new List<Filme>();
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      SELECT
                                          f.id "filmesId",
                                          titulo,
                                          sinopse,
                                          ano,
                                          g.id "generosId",
                                          nome,
                                          descricao
                                      FROM Filmes f INNER JOIN Generos g ON f.idGenero = g.id 
                                      """;
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        // Adicionando objetos de filme na lista
                        filmes.Add(new Filme(
                            Convert.ToInt32(leitor["filmesId"]),
                            leitor["titulo"].ToString(),
                            leitor["sinopse"].ToString(),
                            Convert.ToInt32(leitor["ano"]),
                            new Genero(
                                Convert.ToInt32(leitor["generosId"]),
                                leitor["nome"].ToString(),
                                leitor["descricao"].ToString()
                                ))
                        );
                    }
                    return filmes;
                }
                throw new RegistroNaoEcontradoException("Não há filmes cadastrados na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static List<Filme> ExibirFilmesPorTitulo(string titulo)
    {
        try
        {
            List<Filme> filmes = new List<Filme>();
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      SELECT
                                          f.id "filmesId",
                                          titulo,
                                          sinopse,
                                          ano,
                                          g.id "generosId",
                                          nome,
                                          descricao
                                      FROM Filmes f INNER JOIN Generos g ON f.idGenero = g.id 
                                      WHERE titulo LIKE @titulo
                                      """;
                comando.Parameters.AddWithValue("@titulo", $"{titulo}%");
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        // Adicionando objetos de filme na lista
                        filmes.Add(new Filme(
                                Convert.ToInt32(leitor["filmesId"]),
                                leitor["titulo"].ToString(),
                                leitor["sinopse"].ToString(),
                                Convert.ToInt32(leitor["ano"]),
                                new Genero(
                                    Convert.ToInt32(leitor["generosId"]),
                                    leitor["nome"].ToString(),
                                    leitor["descricao"].ToString()
                                ))
                        );
                    }
                    return filmes;
                }
                throw new RegistroNaoEcontradoException($"Não há filmes com este título: '{titulo}' na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static List<Filme> ExibirFilmesPorGenero(int idGenero)
    {
        try
        {
            List<Filme> filmes = new List<Filme>();
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      SELECT
                                          f.id "filmesId",
                                          titulo,
                                          sinopse,
                                          ano,
                                          g.id "generosId",
                                          nome,
                                          descricao
                                      FROM Filmes f INNER JOIN Generos g ON f.idGenero = g.id 
                                      WHERE g.id = @idGenero
                                      """;
                comando.Parameters.AddWithValue("@idGenero", idGenero);
                using var leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        // Adicionando objetos de filme na lista
                        filmes.Add(new Filme(
                            Convert.ToInt32(leitor["filmesId"]),
                            leitor["titulo"].ToString(),
                            leitor["sinopse"].ToString(),
                            Convert.ToInt32(leitor["ano"]),
                            new Genero(
                                Convert.ToInt32(leitor["generosId"]),
                                leitor["nome"].ToString(),
                                leitor["descricao"].ToString()
                            ))
                        );
                    }

                    return filmes;
                }
                throw new RegistroNaoEcontradoException($"Não há filmes desse gênero na base de dados!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static void AlterarDadosDoFilme(Filme filme)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = """
                                      UPDATE Filmes SET
                                            titulo = @titulo,
                                            sinopse = @sinopse,
                                            ano = @ano,
                                            idGenero = @idGenero
                                      WHERE id = @id
                                      """;
                comando.Parameters.AddWithValue("@titulo", filme.Titulo);
                comando.Parameters.AddWithValue("@sinopse", filme.Sinopse);
                comando.Parameters.AddWithValue("@ano", filme.Ano);
                comando.Parameters.AddWithValue("@idGenero", filme.Genero.Id);
                comando.Parameters.AddWithValue("@id", filme.Id);
                if (comando.ExecuteNonQuery() > 0)
                    Console.WriteLine("Filme atualizado com sucesso!");
                else
                    throw new ArgumentException(
                        "Falha ao alterar dados, tente novamente!\n Caso não consiga, entre em contato com o suporte");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }

    public static void ExcluirFilmePorId(int id)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Filmes WHERE id = @id";
                comando.Parameters.AddWithValue("@id", id);
                if (comando.ExecuteNonQuery() > 0)
                    Console.WriteLine("Filme deletado com sucesso!");
                else 
                    throw new RegistroNaoEcontradoException($"Não foi encontrado um filme com id {id}!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }
    
    public static void ExcluirFilmePeloTitulo(string titulo)
    {
        try
        {
            using (var comando = DbConnection().CreateCommand())
            {
                comando.CommandText = "DELETE FROM Filmes WHERE titulo = @titulo";
                comando.Parameters.AddWithValue("@titulo", titulo);
                if (comando.ExecuteNonQuery() > 0)
                    Console.WriteLine("Filme deletado com sucesso!");
                else
                    throw new RegistroNaoEcontradoException($"Não foi encontrado um filme com título {titulo}!");
            }
        }
        catch (SQLiteException)
        {
            throw new SQLiteException(mensagemDeErroSQLite);
        }
    }
    
    #endregion
    
}