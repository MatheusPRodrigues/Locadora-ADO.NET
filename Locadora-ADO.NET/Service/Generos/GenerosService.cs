using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.ML;

namespace Locadora_ADO.NET.Service.Generos;

public class GenerosService
{
    private static int VerificaSeEhNumero(string mensagem)
    {
        bool ehNumero;
        int numero;
        do
        {
            Console.Write($"{mensagem}");
            ehNumero = Int32.TryParse(Console.ReadLine(), out numero);
            if (!ehNumero)
                Console.WriteLine("\nInsira um número inteiro! Tente novamente!");
        } while (!ehNumero);
        return numero;
    }

    private static string VerificarStringValida(string mensagem)
    {
        string? nome;
        bool stringInvalida;
        do
        {
            Console.Write($"{mensagem}");
            nome = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("\nInsira um nome de gênero válido! Ex: Terror\nTente novamente!");
                stringInvalida = true;
            }
            else
                stringInvalida = false;
        } while (stringInvalida);
        return nome;
    }
    
    private static void ExibirInformacoes(Genero genero)
    {
        Console.WriteLine($"Id: {genero.Id}");
        Console.WriteLine($"Nome: {genero.Nome}");
        Console.WriteLine($"Descrição: {genero.Descricao}");
    }

    private static void PercorrerListaDeGeneros(List<Genero> generos)
    {
        Console.WriteLine("======== GÊNEROS ENCONTRADOS ========");
        foreach (Genero genero in generos)
        {
            ExibirInformacoes(genero);
            Console.WriteLine();
        }
    }
    
    public static void ListarTodosOsGeneros()
    {
        try
        {
            PercorrerListaDeGeneros(LocadoraDAL.ListarTodosOsGeneros());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            Console.Write("Pressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public static void ExibirUmGeneroPorId()
    {
        try
        {
            int numero = VerificaSeEhNumero("Insira o id do gênero que deseja consultar: ");
            Console.WriteLine();
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorId(numero));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("Pressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void ExibirUmGeneroPorNome()
    {
        try
        {
            string nome = VerificarStringValida("Insira o nome do gênero que deseja consultar: ");
            Console.WriteLine();
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorNome(nome));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("Pressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
}