using Locadora_ADO.NET.UI.ClientesUI;
using Locadora_ADO.NET.UI.FilmesUI;
using Locadora_ADO.NET.UI.GenerosUI;
using Locadora_ADO.NET.UI.LocacoesUI;

namespace Locadora_ADO.NET;

class Program
{
    static void Main(string[] args)
    {
        bool continuar = true;
        do
        {
            Console.WriteLine("============ L O C A D O R A  D E  F I L M E S ============");
            Console.WriteLine("============ S E J A  B E M - V I N D O (A) ! ============");
            Console.WriteLine("1 - Menu de gêneros de filme");
            Console.WriteLine("2 - Menu de administração dos clientes");
            Console.WriteLine("3 - Menu de filmes");
            Console.WriteLine("4 - Menu de locações de filmes");
            Console.WriteLine("0 - Encerrar sistema");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    Console.Clear();
                    GenerosMenuGeral.MenuDeInteracaoDeGeneros();
                    break;
                case "2":
                    Console.Clear();
                    ClientesMenuGeral.MenuDeInteracaoDeClientes();
                    break;
                case "3":
                    Console.Clear();
                    FilmesMenuGeral.MenuDeFilmes();
                    break;
                case "4":
                    Console.Clear();
                    LocacoesMenuGeral.MenuDeInteracaoDeLocacoes();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Sistema finalizado!");
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida! Insira uma das opções presentes na lista!");
                    break;
            }
        } while (continuar);
    }
}