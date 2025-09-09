using Locadora_ADO.NET.Service.Generos;

namespace Locadora_ADO.NET.UI.GenerosUI;

public class GenerosMenuExibicao
{
    public static void MenuDeExibicaoDeGeneros()
    {
        do
        {
            Console.WriteLine("========== MENU DE EXIBIÇÃO DE GÊNEROS DE FILMES ==========");
            Console.WriteLine("1 - Exibir todos os gêneros");
            Console.WriteLine("2 - Exibir um gênero pelo id");
            Console.WriteLine("3 - Exibir um gênero pelo nome");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    GenerosService.ListarTodosOsGeneros();
                    Console.Clear();
                    break;
                case "2":
                    GenerosService.ExibirUmGeneroPorId();
                    Console.Clear();
                    break;
                case "3":
                    GenerosService.ExibirUmGeneroPorNome();
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