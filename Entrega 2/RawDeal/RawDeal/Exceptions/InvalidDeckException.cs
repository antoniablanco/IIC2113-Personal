namespace RawDeal.Exceptions;

public class InvalidDeckException : Exception
{
    public InvalidDeckException(string mensaje) : base(mensaje)
    {
    }
}