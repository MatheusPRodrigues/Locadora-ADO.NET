using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.ML;

using static Locadora_ADO.NET.Util.Utils;

namespace Locadora_ADO.NET.Service.Clientes;

public class ClientesService
{
    private static string InserirUmTelefoneValido(string mensagemDeInteracao, string mensagemDeErro)
    {
        string telefone = "";
        do
        {
            Console.Write(mensagemDeInteracao);
            telefone = Console.ReadLine();
            bool ehValida = long.TryParse(telefone, out long number);
            
            if (String.IsNullOrWhiteSpace(telefone) ||
                !ehValida ||
                telefone.Length < 10 ||
                telefone.Length > 11)
                Console.WriteLine(mensagemDeErro);
            else
                return telefone;
            
        } while (true);
    }
    
    private static string InserirUmCpfValido(string mensagemDeInteracao, string mensagemDeErro)
    {
        string cpf = "";
        do
        {
            Console.Write(mensagemDeInteracao);
            cpf = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(cpf) || !(long.TryParse(cpf, out long number)) || cpf.Length != 11)
                Console.WriteLine(mensagemDeErro);
            else
                return cpf;
        } while (true);
    }
    
    private static void VerificarSeExisteClientesCadastrados()
    {
        try 
        {
            LocadoraDAL.ExibirTodosClientes();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    private static void ValidarCPFJaExistente(string cpf)
    {
        List<Cliente> clientes = LocadoraDAL.ExibirTodosClientes();
        foreach (var c in clientes)
        {
            if (c.Cpf == cpf)
            {
                throw new ArgumentException("Esse CPF já está cadastrado!");
            }
        }
    }
    
    private static void ExibirInformacoes(Cliente cliente)
    {
        if (cliente.Ativo)
        {
            Console.WriteLine($"\nId: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"CPF: {cliente.Cpf}");
            Console.WriteLine($"Telefone: {cliente.Telefone}");
            Console.WriteLine($"Endereço {cliente.Endereco}");
            Console.WriteLine($"Ativo: {cliente.Ativo}");
        }
        else
        {
            Console.WriteLine("Este cliente está inativo!");
        }
    }

    private static void PercorrerListaDeClientes(List<Cliente> clientes)
    {
        Console.WriteLine("======== CLIENTES ENCONTRADOS ========");
        foreach (Cliente cliente in clientes)
        {
            if (cliente.Ativo)
                ExibirInformacoes(cliente);
        }
    }

    public static void CadastrarCliente()
    {
        try
        {
            string nome = VerificarStringValida("Insira um nome para o cliente: ",
                "Entrada inválida! Tente novamente!");
            
            string cpf = InserirUmCpfValido("Insira o CPF do cliente. Ex (99999999999): ",
                "CPF inválido! Tente novamente!");
            ValidarCPFJaExistente(cpf);
            
            string telefone = InserirUmTelefoneValido("Insira um número de telefone, ex (1199224455 ou 11999224455): ",
                "Entrada inválida! Tente novamente!");
            
            string endereco = VerificarStringValida(
                "Insira o endereço do cliente, ex (Rua Jacaré, 324 - Belo Horizonte/MG): ",
            "Entrada inválida! Tente novamente!");

            Cliente clienteParaCadastro = new Cliente();
            clienteParaCadastro.Nome = nome;
            clienteParaCadastro.Cpf = cpf;
            clienteParaCadastro.Telefone = telefone;
            clienteParaCadastro.Endereco = endereco;

            LocadoraDAL.CadastrarCliente(clienteParaCadastro);
            ExibirTodosClientes();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    public static void ExibirTodosClientes()
    {
        try
        {
            PercorrerListaDeClientes(LocadoraDAL.ExibirTodosClientes());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public static void ExibirClientePorNome()
    {
        try
        {
            VerificarSeExisteClientesCadastrados();
            string nome = VerificarStringValida("Digite o nome do cliente. Ex(Marcos): ",
                "Entrada inválida! Tente novamente!");
            PercorrerListaDeClientes(LocadoraDAL.ExibirClientePorNome(nome));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void ExibirClientePeloCpf()
    {
        try
        {
            VerificarSeExisteClientesCadastrados();
            string cpf = InserirUmCpfValido("Digite o CPF do cliente para consultá-lo no banco. Ex (99999999999): ",
                "CPF inválido! Tente novamente!");
            ExibirInformacoes(LocadoraDAL.ExibirClientePeloCpf(cpf));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
}