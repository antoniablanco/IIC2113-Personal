using RawDealView.Formatters;

namespace RawDeal;

public class Mazo
{
    private SuperStar _superestar;
    private List<Cartas> _cartasArsenal = new List<Cartas>();
    private List<Cartas> _cartasHand = new List<Cartas>();
    private List<Cartas> _cartasRingSide = new List<Cartas>();
    private List<Cartas> _cartasRingArea = new List<Cartas>();
    
    public Mazo(List<Cartas> cartasMazo, SuperStar superstar)
    {
        superestar = superstar;
        cartasArsenal.AddRange(cartasMazo);
    }
    
    public SuperStar superestar
    {
        get => _superestar;
        set => _superestar = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Cartas> cartasArsenal
    {
        get => _cartasArsenal;
        set => _cartasArsenal = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Cartas> cartasHand
    {
        get => _cartasHand;
        set => _cartasHand = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Cartas> cartasRingSide
    {
        get => _cartasRingSide;
        set => _cartasRingSide = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<Cartas> cartasRingArea
    {
        get => _cartasRingArea;
        set => _cartasRingArea = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void robarCarta()
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
        foreach (Cartas carta in _cartasRingArea)
        {
            fortitudRating += int.Parse(carta.Fortitude);
        }
        return fortitudRating;
    }
    
    public bool EsValidoElMazo() // Aplicar Clean Code
    {   

        if (_cartasArsenal.Count() != 60)
        {
            return false;
        }

        if (_superestar.Name == null)
        {   
            return false;
        }

        Dictionary<string, int> dictCount = new Dictionary<string, int>();
        bool isHeel = false;
        bool isFace = false;
        
        foreach (var carta in _cartasArsenal)
        {
            if (dictCount.ContainsKey(carta.Title))
            {
                dictCount[carta.Title]++;
                if (carta.ContieneSubtipoUnique() && dictCount[carta.Title] > 1)
                {   
                    return false;
                }

                if (!carta.ContieneSubtipoSetUp() && dictCount[carta.Title] > 3)
                {   
                    return false;
                }
            }
            else
                dictCount[carta.Title] = 1;

            if (carta.ContieneSubtipoHeel())
            {
                isHeel = true;
            }
            else if (carta.ContieneSubtipoFace())
            {
                isFace = true;
            }

            if (isHeel && isFace)
            {
                return false;
            }
        }
        
        List<String> nameSuperStars = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        foreach (var carta in _cartasArsenal)
        {   
            foreach (var logo in nameSuperStars)
            {   
                if (carta.ContieneLogoSuperStar(logo) && _superestar.Logo != logo)
                {   
                    return false;
                }
            }
        }

        return true;
    }
    
    public CardInfoImplementation CrearIViewableCardInfo(Cartas carta)
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
    
    private string ObtenerStringCarta(Cartas carta)
    {
        CardInfoImplementation cardInfo = CrearIViewableCardInfo(carta);
        string formattedCard = Formatter.CardToString(cardInfo);
        return formattedCard;
    }
    
    public List<string> CrearListaStringCartas(List<Cartas> cartasConjuntoSeleccionado)
    {
        List<string> stringCartas = new List<string>();

        foreach (var carta in cartasConjuntoSeleccionado)
        {
            stringCartas.Add(ObtenerStringCarta(carta));
        }

        return stringCartas;
    }
    
}