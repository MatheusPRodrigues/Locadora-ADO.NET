using Locadora_ADO.NET.Service.Clientes;

namespace Locadora_ADO.NET.UI.ClientesUI;

public class ClientesMenuGerencStatus
{
    public static void MenuDeGerencDeStatusDoCliente()
    {
        do
        {
            Console.WriteLine("========== MENU GERAL DE GERENCIAMENTO DE STATUS DOS CLIENTES DA LOCADORA ==========");
            Console.WriteLine("1 - Desativar cliente pelo id");
            Console.WriteLine("2 - Listar todos clientes desativos");
            Console.WriteLine("3 - Ativar cliente pelo id");
            Console.WriteLine("4 - Deletar dados do cliente da base de dados");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ClientesService.DesativarClientePeloId();
                    break;
                case "2":
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
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