using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.ML;
using Locadora_ADO.NET.Service.Generos;

namespace Locadora_ADO.NET.Service.Filmes;

using static Util.Utils;

public class FilmesService
{
    private static void ExibirInfoFilmes(Filme filme)
    {
        Console.WriteLine($"Id: {filme.Id}");
        Console.WriteLine($"Título: {filme.Titulo}");
        Console.WriteLine($"Sinopse: {filme.Sinopse}");
        Console.WriteLine($"Ano: {filme.Ano}");
        Console.WriteLine($"Gênero: {filme.Genero.Nome}"); 
        Console.WriteLine();
    }
    
    private static void PercorrerListaDeFilmes(List<Filme> filmes)
    {
        Console.WriteLine("======== FILMES ENCONTRADOS ========");
        foreach (var f in filmes)
        {
            ExibirInfoFilmes(f);
        }
    }

    private static int SelecionarGenero()
    {
        int contador = 1;
        List<Genero> generos = LocadoraDAL.ListarTodosOsGeneros();
        foreach (var g in generos)
        {
            Console.WriteLine($"Id: {g.Id} - Gênero: {g.Nome}");
            contador++;
        }
        Console.WriteLine();
        int idGenero = VerificaSeEhNumeroInteiro("Selecione um dos gêneros da lista (pelo id): ");
        if (generos.Exists(g => g.Id == idGenero))
            return idGenero;

        throw new ArgumentException($"Não há registro de gênero com id: {idGenero}! Tente novamente!");
    }
    
    public static void CadastrarFilme()
    {
        try
        {
            Filme filme = new Filme();
            
            filme.Genero = LocadoraDAL.ExibirUmGeneroPorId(SelecionarGenero());
            string titulo = VerificarStringValida("Insira o título do filme: ");
            
            LocadoraDAL.ConsultarSeFilmeJáExiste(titulo);
            filme.Titulo = titulo;
            
            filme.Sinopse = VerificarStringValida("Insira a sinopse do filme: ");
            filme.Ano = VerificaSeEhNumeroInteiro("Digite o ano de publicação do filme: ");
            
            LocadoraDAL.CadastrarFilme(filme);
            Console.WriteLine("Filme cadastrado com sucesso no banco de dados!");
            ExibirTodosFilmes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    public static void ExibirTodosFilmes()
    {
        try
        {
            PercorrerListaDeFilmes(LocadoraDAL.ExibirTodosOsFilmes());
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            PressioneEnterParaContinuar();
        }
    }

    public static void ExibirFilmesPorTitulo()
    {
        try
        {
            string titulo = VerificarStringValida("Digite o título do filme que deseja consultar (Ex: Vingadores): ");
            PercorrerListaDeFilmes(LocadoraDAL.ExibirFilmesPorTitulo(titulo));
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }

    public static void ExibirFilmesPorGenero()
    {
        try
        {
            PercorrerListaDeFilmes(LocadoraDAL.ExibirFilmesPorGenero(SelecionarGenero()));
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }

    public static void AlterarDadosDoFilme()
    {
        try
        {
            ExibirTodosFilmes();
            int id = VerificaSeEhNumeroInteiro("Digite o id do filme que deseja modificar dados: ");
            Filme filme = LocadoraDAL.ExibirFilmePorId(id);
            
            bool continuar = true;
            do
            {
                Console.Clear();
                ExibirInfoFilmes(filme);
                Console.WriteLine("\nInsira a operação que deseja realizar: ");
                Console.WriteLine("1 - Alterar titulo");
                Console.WriteLine("2 - Alterar sinopse");
                Console.WriteLine("3 - Alterar ano de publicação");
                Console.WriteLine("4 - Alterar gênero");
                Console.WriteLine("5 - Salvar alterações e sair");
                Console.WriteLine("0 - Sair sem salvar");
                Console.Write("=> ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        filme.Titulo = VerificarStringValida("Insira um novo título para o filme: ");
                        break;
                    case "2":
                        filme.Sinopse = VerificarStringValida("Insira uma nova sinopse para o filme: ");
                        break;
                    case "3":
                        filme.Ano = VerificaSeEhNumeroInteiro("Insira um novo ano de publicação para o filme: ");
                        break;
                    case "4":
                        try
                        {
                            filme.Genero = LocadoraDAL.ExibirUmGeneroPorId(SelecionarGenero());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            PressioneEnterParaContinuar();
                        }
                        break;
                    case "5":
                        LocadoraDAL.AlterarDadosDoFilme(filme);
                        ExibirTodosFilmes();
                        continuar = false;
                        break;
                    case "0":
                        while (true)
                        {
                            Console.Write("Deseja realmente sair das alterações sem aplicá-la? (s - sim | n - não): ");
                            string encerrarPrograma = Console.ReadLine().ToLower();

                            if (encerrarPrograma == "s")
                            {
                                Console.WriteLine("Processo encerrado!");
                                PressioneEnterParaContinuar();
                                continuar = false;
                                break;
                            }
                            if (encerrarPrograma == "n") 
                                break;
                            
                            Console.WriteLine("Entrada inválida! Tente novamente!");                             
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente!");
                        break;
                }
            } while (continuar);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }

    public static void ExcluirFilmePorId()
    {
        try
        {
            ExibirTodosFilmes();
            int id = VerificaSeEhNumeroInteiro("Digite o id do filme que deseja excluir: ");
            LocadoraDAL.ExcluirFilmePorId(id);
            ExibirTodosFilmes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }

    public static void ExcluirFilmePeloTitulo()
    {
        try
        {
            ExibirTodosFilmes();
            string titulo = VerificarStringValida("Digite o nome do filme que deseja excluir: ");
            LocadoraDAL.ExcluirFilmePeloTitulo(titulo);
            ExibirTodosFilmes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
}