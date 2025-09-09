using Locadora_ADO.NET.Service.Generos;

namespace Locadora_ADO.NET.UI.GenerosUI;

public class GenerosMenuDeletar
{
    public static void MenuDeDeletarGeneros()
    {
        do
        {
            Console.WriteLine("========== MENU DE DELETAR GÊNEROS DE FILMES ==========");
            Console.WriteLine("1 - Deletar um gênero por id");
            Console.WriteLine("2 - Deletar um gênero pelo nome");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    GenerosService.DeletarUmGeneroPeloId();
                    Console.Clear();
                    break;
                case "2":
                    GenerosService.DeletarUmGeneroPeloNome();
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