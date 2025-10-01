namespace Locadora_ADO.NET.ML;

public class Locacao
{
    public int Id { get; set; }
    public DateTime? DataLocacao { get; set; }
    public DateTime? DataDevolucaoPrevista { get; set; }
    public DateTime? DataDevolucaoReal { get; set; }
    public bool Devolvido { get; set; }
    public Cliente Cliente { get; set; }

    public Locacao()
    {
        
    }

    public Locacao(int id,
        DateTime? dataLocacao,
        DateTime? dataDevolucaoPrevista,
        DateTime? dataDevolucaoReal,
        bool devolvido,
        Cliente cliente)
    {
        Id = id;
        DataLocacao = dataLocacao;
        DataDevolucaoPrevista = dataDevolucaoPrevista;
        DataDevolucaoReal = dataDevolucaoReal;
        Devolvido = devolvido;
        Cliente = cliente;
    }
}