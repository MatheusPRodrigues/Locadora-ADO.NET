using Locadora_ADO.NET.Service.Clientes;

namespace Locadora_ADO.NET.UI.ClientesUI;

using static ClientesMenuExibicao;
using static ClientesMenuDeletar;

public class ClientesMenuGeral
{
    public static void MenuDeInteracaoDeClientes()
    {
        do
        {
            Console.WriteLine("========== MENU GERAL DA ABA - CLIENTES ==========");
            Console.WriteLine("1 - Cadastrar novo cliente");
            Console.WriteLine("2 - Exibir clientes");
            Console.WriteLine("3 - Alterar dados de cliente");
            Console.WriteLine("4 - Deletar cliente do sistema");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ClientesService.CadastrarCliente();
                    break;
                case "2":
                    MenuDeExibicaoDeClientes();
                    Console.Clear();
                    break;
                case "3":
                    ClientesService.AlterarDadosDoCliente();
                    break;
                case "4":
                    MenuDeExclusãoDeClientes();
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