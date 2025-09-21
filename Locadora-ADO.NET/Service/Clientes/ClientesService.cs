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
        Console.WriteLine($"\nId: {cliente.Id}");
        Console.WriteLine($"Nome: {cliente.Nome}");
        Console.WriteLine($"CPF: {cliente.Cpf}");
        Console.WriteLine($"Telefone: {cliente.Telefone}");
        Console.WriteLine($"Endereço {cliente.Endereco}");
        Console.WriteLine($"Ativo: {cliente.Ativo}");
    }

    private static void PercorrerListaDeClientes(List<Cliente> clientes)
    {
        Console.WriteLine("======== CLIENTES ENCONTRADOS ========");
        foreach (Cliente cliente in clientes) 
        {
            ExibirInformacoes(cliente);
        }
    }

    private static void ValidarSeClientePodeSerDesativado(int id)
    {
        try
        {
            Cliente cliente = LocadoraDAL.ExibirClientePeloId(id);
            if (!cliente.Ativo)
            {
                throw new ArgumentException("Este cliente já está desativo!");
            }
        }
        catch (Exception e)
        {
            throw e;
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

    public static void ExibirTodosClientesFiltrandoStatus()
    {
        try
        {
            string escolha;
            bool ativo = true, repeticao;
            do
            {
                Console.Write("Exibindo clientes de forma filtrada => (a - ativos | i - inativos): ");
                escolha = Console.ReadLine().ToLower();

                if (escolha == "a")
                    repeticao = false;
                else if (escolha == "i")
                {
                    ativo = false;
                    repeticao = false;
                }
                else
                {
                    repeticao = true;
                }
            } while (repeticao);
            PercorrerListaDeClientes(LocadoraDAL.ExibirTodosClientes(ativo));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            PressioneEnterParaContinuar();
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

    public static void AlterarDadosDoCliente()
    {
        try
        {
            ExibirTodosClientes();
            int id = VerificaSeEhNumeroInteiro("Digite o id do cliente que deseja alterar dados: ",
                "Entrada inválida! Tente novamente");
            Cliente cliente = LocadoraDAL.ExibirClientePeloId(id);

            bool continuar = true;
            do
            {
                string entradaInvalida = "Entrada inválida! Tente novamente!";
                
                Console.Clear();
                ExibirInformacoes(cliente);
                Console.WriteLine("\nInsira a operação que deseja realizar: ");
                Console.WriteLine("1 - Alterar nome");
                Console.WriteLine("2 - Alterar telefone");
                Console.WriteLine("3 - Alterar endereco");
                Console.WriteLine("4 - Alterar status no sistema (ativo/inativo)");
                Console.WriteLine("5 - Salvar alterações e sair");
                Console.WriteLine("0 - Sair sem salvar");
                Console.Write("=> ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        cliente.Nome = VerificarStringValida($"Insira um novo nome para o cliente {cliente.Nome}: ",
                            entradaInvalida);
                        break;
                    case "2":
                        cliente.Telefone = InserirUmTelefoneValido(
                            $"Insira um novo telefone para o cliente {cliente.Telefone}: ",
                            entradaInvalida);
                        break;
                    case "3":
                        cliente.Endereco = VerificarStringValida(
                            $"Insira um novo endereço para o cliente {cliente.Nome}: ",
                            entradaInvalida);
                        break;
                    case "4":
                        cliente.Ativo = !cliente.Ativo;
                        break;
                    case "5":
                        LocadoraDAL.AlterarDadosDoCliente(cliente);
                        ExibirTodosClientes();
                        continuar = false;
                        break;
                    case "0":
                        while (true)
                        {
                            Console.Write("Deseja realmente sair das alterações sem aplicá-la? (s - sim | n - não): ");
                            string encerrarPrograma = Console.ReadLine().ToLower();

                            if (encerrarPrograma == "s")
                            {
                                Console.WriteLine("Processo encerrado!");
                                PressioneEnterParaContinuar();
                                continuar = false;
                                break;
                            }
                            if (encerrarPrograma == "n") 
                                break;
                            
                            Console.WriteLine(entradaInvalida);                             
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente!");
                        break;
                }
            } while (continuar);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            PressioneEnterParaContinuar();
        }
    }
    
    
    
}