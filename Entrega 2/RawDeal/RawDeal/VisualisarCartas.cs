using RawDealView.Formatters;

namespace RawDeal;

public class VisualisarCartas
{   
    public CardInfoImplementation CrearIViewableCardInfo(Carta carta)
    {
        var cardInfo = new CardInfoImplementation(
            carta.Title,
            carta.Fortitude,
            carta.Damage,
            carta.StunValue,
            carta.Types,
            carta.Subtypes,
            carta.CardEffect);
        return cardInfo;
    }
    
    public PlayInfoImplementation CrearIViewablePlayedInfo(Carta carta)
    {   
        var cardInfo = new PlayInfoImplementation(
            carta.Title,
            carta.Fortitude,
            carta.Damage,
            carta.StunValue,
            carta.Types,
            carta.Subtypes,
            carta.CardEffect);
        
        return cardInfo;
    }
    
    
    public string ObtenerStringInfo<T>(Carta carta, Func<Carta, T> createInfoFunc, Func<T, string> toStringFunc)
    {
        T info = createInfoFunc(carta);
        string formattedInfo = toStringFunc(info);
        return formattedInfo;
    }

    public string ObtenerStringCartaInfo(Carta carta)
    {
        return ObtenerStringInfo(carta, CrearIViewableCardInfo, Formatter.CardToString);
    }

    public string ObtenerStringPlayedInfo(Carta carta)
    {
        return ObtenerStringInfo(carta, CrearIViewablePlayedInfo, Formatter.PlayToString);
    }
    
    
    public List<string> CrearListaStringInfo(List<Carta> cartasConjuntoSeleccionado, Func<Carta, string> obtenerInfoFunc)
    {
        List<string> stringCartas = new List<string>();

        foreach (var carta in cartasConjuntoSeleccionado)
        {
            stringCartas.Add(obtenerInfoFunc(carta));
        }

        return stringCartas;
    }

    public List<string> CrearListaStringCarta(List<Carta> cartasConjuntoSeleccionado)
    {
        return CrearListaStringInfo(cartasConjuntoSeleccionado, ObtenerStringCartaInfo);
    }

    public List<string> CrearListaStringCartaPlayed(List<Carta> cartasConjuntoSeleccionado)
    {
        return CrearListaStringInfo(cartasConjuntoSeleccionado, ObtenerStringPlayedInfo);
    }
    
}