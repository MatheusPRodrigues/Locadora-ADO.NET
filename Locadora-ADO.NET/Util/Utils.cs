namespace Locadora_ADO.NET.Util;

public class Utils
{
    public static int VerificaSeEhNumeroInteiro(string mensagemDeInsercao, string mensagemDeErro)
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

    public static string VerificarStringValida(string mensagemDeInsercao, string mensagemDeErro)
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

    public static void PressioneEnterParaContinuar()
    {
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
    }

}