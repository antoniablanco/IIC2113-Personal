namespace RawDeal.Exceptions;

public class VariableIsNullException: Exception
{
    public VariableIsNullException(string mensaje) : base(mensaje)
    {
    }
}