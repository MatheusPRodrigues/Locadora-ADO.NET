namespace Locadora_ADO.NET.ML;

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cpf { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }
    public bool Ativo { get; set; }

    public Cliente(int id, string? nome, string? cpf, string? telefone, string? endereco, bool ativo)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        Telefone = telefone;
        Endereco = endereco;
        Ativo = ativo;
    }

    public Cliente()
    {
        
    }
}