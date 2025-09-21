namespace Locadora_ADO.NET.ML;

public class Filme
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Sinopse { get; set; }
    public int Ano { get; set; }
    public Genero Genero { get; set; }

    public Filme(int id, string titulo, string sinopse, int ano, Genero genero)
    {
        Id = id;
        Titulo = titulo;
        Sinopse = sinopse;
        Ano = ano;
        Genero = genero;
    }
    
    public Filme()
    {
        
    }
}