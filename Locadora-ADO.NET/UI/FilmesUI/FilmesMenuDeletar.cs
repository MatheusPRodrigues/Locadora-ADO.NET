using Locadora_ADO.NET.Service.Filmes;

namespace Locadora_ADO.NET.UI.FilmesUI;

public class FilmesMenuDeletar
{
    public static void MenuDeDeletarFilmes()
    {
        do
        {
            Console.WriteLine("========== MENU DE DELETAR FILMES DA LOCADORA ==========");
            Console.WriteLine("1 - Deletar filme por id");
            Console.WriteLine("2 - Deletar filme por título");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    FilmesService.ExcluirFilmePorId();
                    break;
                case "2":
                    FilmesService.ExcluirFilmePeloTitulo();
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