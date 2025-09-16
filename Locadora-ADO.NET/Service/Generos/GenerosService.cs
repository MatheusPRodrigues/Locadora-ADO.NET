using Locadora_ADO.NET.DAL;
using Locadora_ADO.NET.Exceptions;
using Locadora_ADO.NET.ML;

// importação estática
using static Locadora_ADO.NET.Util.Utils;

namespace Locadora_ADO.NET.Service.Generos;

public class GenerosService
{
    private static void ExibirInformacoes(Genero genero)
    {
        Console.WriteLine($"\nId: {genero.Id}");
        Console.WriteLine($"Nome: {genero.Nome}");
        Console.WriteLine($"Descrição: {genero.Descricao}");
    }

    private static void PercorrerListaDeGeneros(List<Genero> generos)
    {
        Console.WriteLine("======== GÊNEROS ENCONTRADOS ========");
        foreach (Genero genero in generos)
        {
            ExibirInformacoes(genero);
        }
    }

    private static void VerificarSeNomeDeGeneroJaExiste(string nome)
    {
        List<Genero> generos = LocadoraDAL.ListarTodosOsGeneros();
        foreach (Genero genero in generos) 
        {
            if (genero.Nome.ToLower().Equals(nome.ToLower()))
            {
                throw new ArgumentException($"O gênero {genero.Nome} já foi cadastrado!");
            }
        }
    }

    private static void VerificarSeExisteGenerosCadastrados()
    {
        List<Genero> generos = LocadoraDAL.ListarTodosOsGeneros();
        if (generos.Count < 1)
            throw new RegistroNaoEcontradoException("Não há gêneros cadastrados na base de dados!");
    }

    private static void AlterandoDadosDeGenero(Genero genero)
    {
        do
        {
            try
            {
                Console.Clear();
                Console.WriteLine("======== ALTERE OS DADOS DO GÊNERO ========");
                ExibirInformacoes(genero);
                Console.WriteLine("\nSelecione uma das seguintes opções: ");
                Console.WriteLine("1 - Alterar nome");
                Console.WriteLine("2 - Alterar descricao");
                Console.WriteLine("3 - Salvar alterações");
                Console.WriteLine("4 - Cancelar alterações e retornar");
                Console.Write(": ");
                string? opcaoDoUsuario = Console.ReadLine();

                switch (opcaoDoUsuario)
                {
                    case "1":
                        string novoNome = VerificarStringValida(
                            "Insira um novo nome para o gênero: ",
                            "Nome inválido! Ex válido: Terror");
                        VerificarSeNomeDeGeneroJaExiste(novoNome);
                        genero.Nome = novoNome;
                        break;
                    case "2":
                        string novaDescricao = VerificarStringValida(
                            "Insira uma nova descrição para o gênero: ",
                            "Digite uma descrição!!");
                        genero.Descricao = novaDescricao;
                        break;
                    case "3":
                        LocadoraDAL.AtualizarUmGenero(genero);
                        Console.Write("\nPressioner ENTER para continuar...");
                        Console.ReadLine();
                        ListarTodosOsGeneros();
                        return;
                    case "4":
                        Console.WriteLine("Operação de atualização encerrada com sucesso!");
                        Console.Write("\nPressioner ENTER para continuar...");
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente com uma das opções do menu!");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Write("\nPressioner ENTER para continuar...");
                Console.ReadLine();
            }
        } while (true);
    }
    
    
    public static void CadastrarGeneroNoSistema()
    {
        try
        {
            string nomeDoGenero = VerificarStringValida("Digite um nome para o novo gênero de filme: ",
                "Nome para o gênero inválido! Ex válido: Aventura");
            VerificarSeNomeDeGeneroJaExiste(nomeDoGenero);
            
            string descricaoDoGenero = VerificarStringValida(
                "Digite uma descrição breve sobre o novo gênero de filme: ",
                "Insira uma descrição para o gênero de filme!!"
                );
            
            Genero genero = new Genero();
            genero.Nome = nomeDoGenero;
            genero.Descricao = descricaoDoGenero;
            
            LocadoraDAL.CadastrarGeneroNoSistema(genero);
            ListarTodosOsGeneros();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void ListarTodosOsGeneros()
    {
        try
        {
            PercorrerListaDeGeneros(LocadoraDAL.ListarTodosOsGeneros());
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

    public static void ExibirUmGeneroPorId()
    {
        try
        {
            int numero = VerificaSeEhNumeroInteiro(
                "Insira o id do gênero que deseja consultar: ",
                "Digite somente números inteiros positivos!"
                );
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorId(numero));
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
    
    public static void ExibirUmGeneroPorNome()
    {
        try
        {
            string nome = VerificarStringValida("Insira o nome do gênero que deseja consultar: ", 
                "Digite um nome para gênero válido! Ex: Terror");
            ExibirInformacoes(LocadoraDAL.ExibirUmGeneroPorNome(nome));
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

    public static void AtualizarGenero()
    {
        try
        {
            VerificarSeExisteGenerosCadastrados();
            ListarTodosOsGeneros();
            
            int id = VerificaSeEhNumeroInteiro(
                "Insira o id do gênero que deseja consultar: ",
                "Digite somente números inteiros positivos!"
            );

            AlterandoDadosDeGenero(LocadoraDAL.ExibirUmGeneroPorId(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void DeletarUmGeneroPeloId()
    {
        try
        {
            VerificarSeExisteGenerosCadastrados();
            ListarTodosOsGeneros();
            
            int id = VerificaSeEhNumeroInteiro(
                "Digite o id do gênero que deseja deletar: ",
                "Digite um número inteiro positivo para o id!");

            string opcao = "";
            while (opcao != "1" && opcao != "0")
            {
                Console.WriteLine($"Deseja excluir gênero de id: {id}\n1-Confirmar/0-Retornar:");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    LocadoraDAL.DeletarUmGeneroPorId(id);
                    Console.Write("\nPressioner ENTER para continuar...");
                    Console.ReadLine();
                    
                    ListarTodosOsGeneros();
                }
                else if(opcao == "0")
                {
                    Console.WriteLine("Operação cancelada com sucesso!");
                    Console.Write("\nPressioner ENTER para continuar...");
                    Console.ReadLine();
                    return;
                }
                else
                    Console.WriteLine("Opção inválida! Tente novamente!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
    
    public static void DeletarUmGeneroPeloNome()
    {
        try
        {
            VerificarSeExisteGenerosCadastrados();
            ListarTodosOsGeneros();

            string nome = VerificarStringValida(
                "Digite o nome do gênero que deseja excluir:",
                "Digite um nome válido! Ex: Terror");

            string opcao = "";
            while (opcao != "1" && opcao != "0")
            {
                Console.WriteLine($"Deseja excluir gênero : {nome}\n1-Confirmar/0-Retornar:");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    LocadoraDAL.DeletarUmGeneroPeloNome(nome);
                    Console.Write("\nPressioner ENTER para continuar...");
                    Console.ReadLine();

                    ListarTodosOsGeneros();
                }
                else if (opcao == "0")
                {
                    Console.WriteLine("Operação cancelada com sucesso!");
                    Console.Write("\nPressioner ENTER para continuar...");
                    Console.ReadLine();
                    return;
                }
                else
                    Console.WriteLine("Opção inválida! Tente novamente!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.Write("\nPressioner ENTER para continuar...");
            Console.ReadLine();
        }
    }
}