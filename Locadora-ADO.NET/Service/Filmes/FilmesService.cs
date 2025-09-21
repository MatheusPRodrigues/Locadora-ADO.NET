using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.ML;

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
}