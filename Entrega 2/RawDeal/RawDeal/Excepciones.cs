namespace RawDeal;

public class ExcepcionMazoNoValido : Exception
{
    public ExcepcionMazoNoValido(string mensaje) : base(mensaje)
    {
    }
}