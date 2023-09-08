namespace RawDeal;

public class ValidarDeck
{
    public bool CumpleTamanoMazo(Mazo mazo)
    {
        return (mazo.cartasArsenal.Count() == 60);
    }
    
    public bool CumpleTenerSuperStar(Mazo mazo)
    {
        return (mazo.superestar != null);
    }

    public bool CumpleSubtipos(Mazo mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        bool isHeel = MazoContieneIsHeel(mazo);
        bool isFace = MazoContieneIsFace(mazo);
        bool cumpleUnique = MazoCumpleCantidadUnique(mazo);
        bool cumpleNoSetUp = MazoCumpleCantidadNoSetUp(mazo);

        return (!(isHeel && isFace) && cumpleUnique && cumpleNoSetUp);
    }

    public bool MazoCumpleCantidadUnique(Mazo mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        
        foreach (var carta in mazo.cartasArsenal)
        {
            dictNumeroPorCartas.TryGetValue(carta.Title, out int count);
            dictNumeroPorCartas[carta.Title] = count + 1;

            if (carta.ContieneSubtipoUnique() && count > 0)
                return false;
        }

        return true;
    }
    
    public bool MazoCumpleCantidadNoSetUp(Mazo mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        
        foreach (var carta in mazo.cartasArsenal)
        {
            dictNumeroPorCartas.TryGetValue(carta.Title, out int count);
            dictNumeroPorCartas[carta.Title] = count + 1;

            if (!carta.ContieneSubtipoSetUp() && count > 2)
                return false;
        }

        return true;
    }

    public bool MazoContieneIsHeel(Mazo mazo)
    {
        foreach (var carta in mazo.cartasArsenal)
        {
            if (carta.ContieneSubtipoHeel())
                return true;
        }

        return false;
    }
    
    public bool MazoContieneIsFace(Mazo mazo)
    {
        foreach (var carta in mazo.cartasArsenal)
        {
            if (carta.ContieneSubtipoFace())
                return true;
        }

        return false;
    }
    
    public bool MazoCumpleLogoSuperStar(Mazo mazo)
    {   
        foreach (var carta in mazo.cartasArsenal)
        {   
            if (!EstaCartaCumpleLogoSuperStar(carta, mazo.superestar.Logo))
                return false;
        }
        return true;
    }

    public bool EstaCartaCumpleLogoSuperStar(Carta carta, string logoSuperStar)
    {   
        List<String> logoSuperStars = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        foreach (var logo in logoSuperStars)
        {   
            if (carta.ContieneLogoSuperStar(logo) && logoSuperStar != logo)
                return false;
        }
        return true;
    }
    
    public bool EsValidoMazo(Mazo mazo)
    {
        return (CumpleTamanoMazo(mazo) && CumpleTenerSuperStar(mazo) && CumpleSubtipos(mazo) && MazoCumpleLogoSuperStar(mazo));
    }
}