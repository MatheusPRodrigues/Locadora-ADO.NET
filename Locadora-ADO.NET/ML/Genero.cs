namespace Locadora_ADO.NET.ML;

public class Genero
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public Genero(int id, string nome, string descricao)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
    }

    public Genero()
    {
        
    }
}