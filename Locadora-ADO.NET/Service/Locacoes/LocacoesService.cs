using Locadora_ADO.NET.DAL;

namespace Locadora_ADO.NET.Service.Locacoes;

using ML;

using static Util.Utils;

public class LocacoesService
{
    private static void ExibirInfoDeLocacao(Locacao locacao)
    {
        Console.WriteLine($"Id: {locacao.Id}");
        Console.WriteLine($"Data de locação: {locacao.DataLocacao}");
        Console.WriteLine($"Data de devolução prevista: {locacao.DataDevolucaoPrevista}");
        string devolucaoReal = locacao.DataDevolucaoReal != null ?
            locacao.DataDevolucaoReal.ToString() :
            "Devolução ainda não realizada pelo cliente!";
        
        Console.WriteLine($"Data de devolução do cliente: {devolucaoReal}");
        Console.WriteLine($"Nome do cliente: {locacao.Cliente.Nome}");
        Console.WriteLine($"CPF do cliente: {locacao.Cliente.Cpf}");
        Console.WriteLine();
    }
    
    private static void PercorrerListaDeLocacoes(List<Locacao> locacoes)
    {
        Console.WriteLine("======== LOCAÇÕES ENCONTRADAS ========");
        foreach (var l in locacoes)
        {
            ExibirInfoDeLocacao(l);
        }
    }
    
    public static void ExibirTodasLocacoes()
    {
        try
        {
            PercorrerListaDeLocacoes(LocadoraDAL.ExibirTodasLocacoes());
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    public static void ExibirTodasLocacoesDevolvidas()
    {
        try
        {
            PercorrerListaDeLocacoes(LocadoraDAL.ExibirTodasLocacoesDevolvidas());
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    public static void ExibirTodasLocacoesNaoDevolvidas()
    {
        try
        {
            PercorrerListaDeLocacoes(LocadoraDAL.ExibirTodasLocacoesNaoDevolvidas());
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    public static void ExibirLocacaoPorId()
    {
        try
        {
            int id = VerificaSeEhNumeroInteiro("Digite o id da locação de filme que deseja consultar: ");
            Console.Clear();
            Console.WriteLine("==== LOCAÇÃO ENCONTRADA ====");
            ExibirInfoDeLocacao(LocadoraDAL.ExibirLocacaoPorId(id));
            PressioneEnterParaContinuar();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
}