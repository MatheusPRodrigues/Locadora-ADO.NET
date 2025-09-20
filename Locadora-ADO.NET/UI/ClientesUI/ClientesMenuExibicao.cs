using Locadora_ADO.NET.Service.Clientes;

namespace Locadora_ADO.NET.UI.ClientesUI;

public class ClientesMenuExibicao
{
    public static void MenuDeExibicaoDeClientes()
    {
        do
        {
            Console.WriteLine("========== MENU DE EXIBIÇÃO DE CLIENTES DA LOCADORA ==========");
            Console.WriteLine("1 - Exibir todos os clientes");
            Console.WriteLine("2 - Exibir um cliente pelo cpf");
            Console.WriteLine("3 - Exibir um cliente pelo nome");
            // TODO
            //Console.WriteLine("4 - Exibir somente clientes ativos");
            //Console.WriteLine("5 - Exibir somente clientes desativos");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ClientesService.ExibirTodosClientes();
                    Console.Clear();
                    break;
                case "2":
                    ClientesService.ExibirClientePeloCpf();
                    Console.Clear();
                    break;
                case "3":
                    ClientesService.ExibirClientePorNome();
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