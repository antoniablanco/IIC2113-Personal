namespace RawDeal;

public class Player
{
    private SuperStar _superestar;
    private List<Carta> _cartasArsenal = new List<Carta>();
    private List<Carta> _cartasHand = new List<Carta>();
    private List<Carta> _cartasRingSide = new List<Carta>();
    private List<Carta> _cartasRingArea = new List<Carta>();
    
    public Player(List<Carta> cartasPlayer, SuperStar superstar)
    {
        superestar = superstar;
        cartasArsenal.AddRange(cartasPlayer);
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
            _cartasHand.Add(_cartasArsenal[lastIndex]);
            _cartasArsenal.RemoveAt(lastIndex);
        }
    }

    public void RobarCartasHandInicial()
    {
        for (int i = 0; i < superestar.HandSize; i++)
        {
            RobarCarta();
        }
    }
    
    public int FortitudRating()
    {   
        int fortitudRating = 0;
        foreach (Carta carta in cartasRingArea)
        {
            fortitudRating += int.Parse(carta.Damage);
        }
        return fortitudRating;
    }
    
    public List<Carta> CartasPosiblesDeJugar()
    {
        return cartasHand
            .Where(carta => int.Parse(carta.Fortitude) <= FortitudRating())
            .ToList();
    }

    public Carta CartaPasaDelArsenalAlRingSide()
    {
        int lastIndex = _cartasArsenal.Count - 1;
        Carta cartaMovida = _cartasArsenal[lastIndex];
        _cartasRingSide.Add(cartaMovida);
        _cartasArsenal.RemoveAt(lastIndex);

        return cartaMovida;
    }

    public void CartaPasaDeHandAlRingArea(Carta carta)
    {   
        _cartasRingArea.Add(carta);
        _cartasHand.Remove(carta);
    }

    public bool TieneCartasEnArsenal()
    {
        return (cartasArsenal.Count > 0);
    }
}