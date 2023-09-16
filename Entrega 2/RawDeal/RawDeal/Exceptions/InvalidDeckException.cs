namespace RawDeal;

public class InvalidDeckException : Exception
{
    public InvalidDeckException(string mensaje) : base(mensaje)
    {
    }
}