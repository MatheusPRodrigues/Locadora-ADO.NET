namespace Locadora_ADO.NET.Exceptions;

public class RegistroNaoEcontradoException : Exception
{
    public RegistroNaoEcontradoException(string? message) : base(message)
    {
    }
}