using Locadora_ADO.NET.Service.Locacoes;

namespace Locadora_ADO.NET.UI.LocacoesUI;

using static LocacoesService;

public class LocacoesMenuExibicao
{
    public static void MenuDeExibicaoDeLocacoes()
    {
        do
        {
            Console.WriteLine("========== MENU DE EXIBIÇÃO DE LOCAÇÕES DE FILMES ==========");
            Console.WriteLine("1 - Exibir todas locações");
            Console.WriteLine("2 - Exibir uma locação pelo id");
            Console.WriteLine("3 - Exibir apenas locações devolvidas");
            Console.WriteLine("4 - Exibir apenas locações não devolvidas");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ExibirTodasLocacoes();
                    break;
                case "2":
                    ExibirLocacaoPorId();
                    break;
                case "3":
                    ExibirTodasLocacoesDevolvidas();
                    break;
                case "4":
                    ExibirTodasLocacoesNaoDevolvidas();
                    break;
                case "0":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida! Insira uma das opções presentes na lista!");
                    break;
            }
        } while (true);
    }

}