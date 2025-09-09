using Locadora_ADO.NET.UI.GenerosUI;

namespace Locadora_ADO.NET;

class Program
{
    static void Main(string[] args)
    {
        do
        {
            Console.WriteLine("============ L O C A D O R A  D E  F I L M E S ============");
            Console.WriteLine("============ S E J A  B E M - V I N D O (A) ! ============");
            Console.WriteLine("1 - Menu de gêneros de filme");
            Console.WriteLine("0 - Encerrar sistema");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    Console.Clear();
                    GenerosMenuGeral.MenuDeInteracaoDeGeneros();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opção inválida! Insira uma das opções presentes na lista!");
                    break;
            }
        } while (true);
    }
}