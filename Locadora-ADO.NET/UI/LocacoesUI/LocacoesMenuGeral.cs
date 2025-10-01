namespace Locadora_ADO.NET.UI.LocacoesUI;

using static LocacoesMenuExibicao;

public class LocacoesMenuGeral
{
    public static void MenuDeInteracaoDeLocacoes()
    {
        do
        {
            Console.WriteLine("========== MENU GERAL DA ABA - LOCAÇÕES DE FILMES ==========");
            Console.WriteLine("1 - Cadastrar nova locação");
            Console.WriteLine("2 - Exibir locações");
            Console.WriteLine("3 - Atualizar dados de uma locação");
            Console.WriteLine("4 - Deletar locações");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    break;
                case "2":
                    Console.Clear();
                    MenuDeExibicaoDeLocacoes();
                    break;
                case "3":
                    break;
                case "4":
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