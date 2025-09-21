namespace Locadora_ADO.NET.UI.FilmesUI;

using static Service.Filmes.FilmesService;

public class FilmesMenuExibicao
{
    public static void MenuDeExibicaoDeFilmes()
    {
        do
        {
            Console.WriteLine("========== MENU DE EXIBIÇÃO DE FILMES DA LOCADORA ==========");
            Console.WriteLine("1 - Exibir todos os filmes");
            Console.WriteLine("2 - Exibir filmes por título");
            Console.WriteLine("3 - Exibir filmes por gênero");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ExibirTodosFilmes();
                    break;
                case "2":
                    ExibirFilmesPorTitulo();
                    break;
                case "3":
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