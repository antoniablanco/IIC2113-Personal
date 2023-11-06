namespace RawDeal.Exceptions;

public class CardNotFoundException: Exception
{
    public CardNotFoundException(string mensaje) : base(mensaje)
    {
    }
}