namespace Locadora_ADO.NET.Util;

public class Utils
{
    private static string MensagemDeErro = "Entrada inválida! Tente novamente!"; 
    
    public static int VerificaSeEhNumeroInteiro(string mensagemDeInsercao)
    {
        int numero;
        do
        {
            Console.Write($"{mensagemDeInsercao}");
            bool ehNumero = Int32.TryParse(Console.ReadLine(), out numero);
            if (ehNumero && numero > 0)
                break;
            Console.WriteLine($"\n{MensagemDeErro}");
        } while (true);
        return numero;
    }

    public static string VerificarStringValida(string mensagemDeInsercao)
    {
        string? nome;
        do
        {
            Console.Write($"{mensagemDeInsercao}");
            nome = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(nome))
                break;
            Console.WriteLine($"\n{MensagemDeErro}");
        } while (true);
        return nome;
    }

    public static void PressioneEnterParaContinuar()
    {
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
    }

}