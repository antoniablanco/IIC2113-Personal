namespace RawDeal;

public class Mazo
{
    private SuperStar _superestar;
    private List<Cartas> _cartasArsenal = new List<Cartas>();
    private List<Cartas> _cartasHand = new List<Cartas>();

    public Mazo(List<Cartas> cartasMazo, SuperStar superstar)
    {

        _superestar = superstar;
        foreach (Cartas carta in cartasMazo) 
        {_cartasArsenal.Add(carta);}

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

    public void robarCarta()
    {
        int lastIndex = _cartasArsenal.Count - 1;
        _cartasHand.Add(_cartasArsenal[lastIndex]);
        _cartasArsenal.RemoveAt(lastIndex);
    }
    
    public int FortitudRating()
    {
        int fortitudRating = 0;
        foreach (Cartas carta in _cartasArsenal)
        {
            fortitudRating += int.Parse(carta.Fortitude);
        }
        return fortitudRating;
    }
    
    public bool IsValid() // Crear Validación (Caso borde, hay mas de un superstar)
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
                if (carta.IsUnique() && dictCount[carta.Title] > 1)
                {   
                    return false;
                }

                if (!carta.IsSetUp() && dictCount[carta.Title] > 3)
                {   
                    return false;
                }
            }
            else
                dictCount[carta.Title] = 1;

            if (carta.IsHeel())
            {
                isHeel = true;
            }
            else if (carta.IsFace())
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
                if (carta.containsLogoSuperStar(logo) && _superestar.Logo != logo)
                {   
                    return false;
                }
            }
        }

        return true;
    }
}