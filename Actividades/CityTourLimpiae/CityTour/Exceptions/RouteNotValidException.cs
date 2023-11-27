namespace CityTour.Exceptions;

public class RouteNotValidException : Exception
{
    public RouteNotValidException() : base("La ruta no es válida.")
    {
        // Puedes personalizar el mensaje de la excepción en el constructor.
    }

    public RouteNotValidException(string message) : base(message)
    {
    }

}