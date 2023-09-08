using RawDealView.Formatters;

namespace RawDeal;

public class Mazo
{
    private SuperStar _superestar;
    private List<Carta> _cartasArsenal = new List<Carta>();
    private List<Carta> _cartasHand = new List<Carta>();
    private List<Carta> _cartasRingSide = new List<Carta>();
    private List<Carta> _cartasRingArea = new List<Carta>();
    
    public Mazo(List<Carta> cartasMazo, SuperStar superstar)
    {
        superestar = superstar;
        cartasArsenal.AddRange(cartasMazo);
    }
    
    public SuperStar superestar
    {
        get => _superestar;
        set => _superestar = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Carta> cartasArsenal
    {
        get => _cartasArsenal;
        set => _cartasArsenal = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Carta> cartasHand
    {
        get => _cartasHand;
        set => _cartasHand = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Carta> cartasRingSide
    {
        get => _cartasRingSide;
        set => _cartasRingSide = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Carta> cartasRingArea
    {
        get => _cartasRingArea;
        set => _cartasRingArea = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void RobarCarta()
    {
        if (cartasArsenal.Count > 0)
        {
            int lastIndex = _cartasArsenal.Count - 1;
            _cartasHand.Add(_cartasArsenal[_cartasArsenal.Count - 1]);
            _cartasArsenal.RemoveAt(lastIndex);
        }
    }
    
    public int FortitudRating()
    {
        int fortitudRating = 0;
        foreach (Carta carta in _cartasRingArea)
        {
            fortitudRating += int.Parse(carta.Fortitude);
        }
        return fortitudRating;
    }
    
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
    
    private string ObtenerStringCarta(Carta carta)
    {
        CardInfoImplementation cardInfo = CrearIViewableCardInfo(carta);
        string formattedCard = Formatter.CardToString(cardInfo);
        return formattedCard;
    }
    
    public List<string> CrearListaStringCarta(List<Carta> cartasConjuntoSeleccionado)
    {
        List<string> stringCartas = new List<string>();

        foreach (var carta in cartasConjuntoSeleccionado)
        {
            stringCartas.Add(ObtenerStringCarta(carta));
        }

        return stringCartas;
    }
    
}