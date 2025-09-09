namespace Locadora_ADO.NET.UI.GenerosUI;

public class GenerosMenuGeral
{
    public static void MenuDeInteracaoDeGeneros()
    {
        do
        {
            Console.WriteLine("========== MENU GERAL DA ABA - GÊNEROS DE FILMES ==========");
            Console.WriteLine("1 - Cadastrar novo gênero");
            Console.WriteLine("2 - Exibir gêneros");
            Console.WriteLine("3 - Atualizar dados de gênero");
            Console.WriteLine("4 - Exibir gêneros");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "2":
                    Console.Clear();
                    GenerosMenuExibicao.MenuDeExibicaoDeGeneros();
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