using System.Data.SQLite;
using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.Exceptions;
using Locadora_ADO.NET.ML;

namespace Locadora_ADO.NET.Service.Generos;

public class GenerosService
{
    private static int VerificaSeEhNumeroInteiro(string mensagemDeInsercao, string mensagemDeErro)
    {
        int numero;
        do
        {
            Console.Write($"{mensagemDeInsercao}");
            bool ehNumero = Int32.TryParse(Console.ReadLine(), out numero);
            if (ehNumero && numero > 0)
                break;
            Console.WriteLine($"\n{mensagemDeErro}");
        } while (true);
        return numero;
    }

    private static string VerificarStringValida(string mensagemDeInsercao, string mensagemDeErro)
    {
        string? nome;
        do
        {
            Console.Write($"{mensagemDeInsercao}");
            nome = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(nome))
                break;
            Console.WriteLine($"\n{mensagemDeErro}");
        } while (true);
        return nome;
    }
    
    private static void ExibirInformacoes(Genero genero)
    {
        Console.WriteLine($"\nId: {genero.Id}");
        Console.WriteLine($"Nome: {genero.Nome}");
        Console.WriteLine($"Descrição: {genero.Descricao}");
    }

    private static void PercorrerListaDeGeneros(List<Genero> generos)
    {
        Console.WriteLine("======== GÊNEROS ENCONTRADOS ========");
        foreach (Genero genero in generos)
        {
            ExibirInformacoes(genero);
        }
    }

    private static void VerificarSeNomeDeGeneroJaExiste(string nome)
    {
        List<Genero> generos = LocadoraDAL.ListarTodosOsGeneros();
        foreach (Genero genero in generos) 
        {
            if (genero.Nome.ToLower().Equals(nome.ToLower()))
            {
                throw new ArgumentException($"O gênero {genero.Nome} já foi cadastrado!");
            }
        }
    }

    private static void VerificarSeExisteGenerosCadastrados()
    {
        List<Genero> generos = LocadoraDAL.ListarTodosOsGeneros();
        if (generos.Count < 1)
            throw new RegistroNaoEcontradoException("Não há gêneros cadastrados na base de dados!");
    }
    
    
    public static void CadastrarGeneroNoSistema()
    {
        try
        {
            string nomeDoGenero = VerificarStringValida("Digite um nome para o novo gênero de filme: ",
                "Nome para o gênero inválido! Ex válido: Aventura");
            VerificarSeNomeDeGeneroJaExiste(nomeDoGenero);
            
            string descricaoDoGenero = VerificarStringValida(
                "Digite uma descrição breve sobre o novo gênero de filme: ",
                "Insira uma descrição para o gênero de filme!!"
                );
            
            Genero genero = new Genero();
            genero.Nome = nomeDoGenero;
            genero.Descricao = descricaoDoGenero;
            
            LocadoraDAL.CadastrarGeneroNoSistema(genero);
            ListarTodosOsGeneros();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
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
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public static void ExibirUmGeneroPorId()
    {
        try
        {
            int numero = VerificaSeEhNumeroInteiro(
                "Insira o id do gênero que deseja consultar: ",
                "Digite somente números inteiros positivos!"
                );
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorId(numero));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void ExibirUmGeneroPorNome()
    {
        try
        {
            string nome = VerificarStringValida("Insira o nome do gênero que deseja consultar: ", 
                "Digite um nome para gênero válido! Ex: Terror");
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorNome(nome));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public static void DeletarUmGeneroPeloId()
    {
        try
        {
            VerificarSeExisteGenerosCadastrados();
            ListarTodosOsGeneros();
            
            int id = VerificaSeEhNumeroInteiro(
                "Digite o id do gênero que deseja deletar: ",
                "Digite um número inteiro positivo para o id!");
            
            LocadoraDAL.DeletarUmGeneroPorId(id);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
            
            ListarTodosOsGeneros();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void DeletarUmGeneroPeloNome()
    {
        try
        {
            VerificarSeExisteGenerosCadastrados();
            ListarTodosOsGeneros();

            string nome = VerificarStringValida(
                "Digite o nome do gênero que deseja excluir:",
                "Digite um nome válido! Ex: Terror");
            
            LocadoraDAL.DeletarUmGeneroPeloNome(nome);
            
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
            
            ListarTodosOsGeneros();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
}