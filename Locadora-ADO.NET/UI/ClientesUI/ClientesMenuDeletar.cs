using Locadora_ADO.NET.Service.Clientes;

namespace Locadora_ADO.NET.UI.ClientesUI;

public class ClientesMenuDeletar
{
    public static void MenuDeExclusãoDeClientes()
    {
        do
        {
            Console.WriteLine("========== MENU DE EXCLUSÃO DE CLIENTES DA LOCADORA ==========");
            Console.WriteLine("1 - Deletar um cliente por id");
            Console.WriteLine("2 - Deletar um cliente pelo cpf");
            Console.WriteLine("0 - Retornar ao menu anterior");
            Console.Write(": ");
            string? opcaoDoUsuario = Console.ReadLine();

            switch (opcaoDoUsuario)
            {
                case "1":
                    ClientesService.ExcluirClientePeloId();
                    break;
                case "2":
                    ClientesService.ExcluirClientePeloCpf();
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