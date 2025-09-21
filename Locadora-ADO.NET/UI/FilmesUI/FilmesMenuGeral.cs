namespace Locadora_ADO.NET.UI.FilmesUI;

using static FilmesMenuExibicao;

public class FilmesMenuGeral
{
    public static void MenuDeFilmes()
    {
        do
        {
            Console.WriteLine("========== MENU GERAL DA ABA - FILMES ==========");
            Console.WriteLine("1 - Cadastrar novo filme");
            Console.WriteLine("2 - Exibir filmes");
            Console.WriteLine("3 - Alterar dados de um filme");
            Console.WriteLine("4 - Deletar filme do sistema");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    Console.Clear();
                    break;
                case "2":
                    MenuDeExibicaoDeFilmes();
                    Console.Clear();
                    break;
                case "3":
                    break;
                case "4":
                    Console.Clear();
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