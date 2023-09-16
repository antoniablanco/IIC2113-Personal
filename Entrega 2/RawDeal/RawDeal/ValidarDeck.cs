namespace RawDeal;

public class ValidarDeck
{
    public bool CumpleTamanoMazo(Player mazo)
    {
        return (mazo.cardsArsenal.Count() == 60);
    }
    
    public bool CumpleTenerSuperStar(Player mazo)
    {   
        return (mazo.superestar != null);
    }

    public bool CumpleSubtipos(Player mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        bool isHeel = MazoContieneIsHeel(mazo);
        bool isFace = MazoContieneIsFace(mazo);
        bool cumpleUnique = MazoCumpleCantidadUnique(mazo);
        bool cumpleNoSetUp = MazoCumpleCantidadNoSetUp(mazo);

        return (!(isHeel && isFace) && cumpleUnique && cumpleNoSetUp);
    }

    public bool MazoCumpleCantidadUnique(Player mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        
        foreach (var carta in mazo.cardsArsenal)
        {
            dictNumeroPorCartas.TryGetValue(carta.Title, out int count);
            dictNumeroPorCartas[carta.Title] = count + 1;

            if (carta.ContieneSubtipoUnique() && count > 0)
                return false;
        }

        return true;
    }
    
    public bool MazoCumpleCantidadNoSetUp(Player mazo)
    {
        Dictionary<string, int> dictNumeroPorCartas = new Dictionary<string, int>();
        
        foreach (var carta in mazo.cardsArsenal)
        {
            dictNumeroPorCartas.TryGetValue(carta.Title, out int count);
            dictNumeroPorCartas[carta.Title] = count + 1;

            if (!carta.ContieneSubtipoSetUp() && count > 2)
                return false;
        }

        return true;
    }

    public bool MazoContieneIsHeel(Player mazo)
    {
        foreach (var carta in mazo.cardsArsenal)
        {
            if (carta.ContieneSubtipoHeel())
                return true;
        }
        return false;
    }
    
    public bool MazoContieneIsFace(Player mazo)
    {
        foreach (var carta in mazo.cardsArsenal)
        {
            if (carta.ContieneSubtipoFace())
                return true;
        }

        return false;
    }
    
    public bool MazoCumpleLogoSuperStar(Player mazo)
    {   
        foreach (var carta in mazo.cardsArsenal)
        {
            if (!EstaCartaCumpleLogoSuperStar(carta, mazo.superestar.Logo))
            {
                return false;
            }
        }
        return true;
    }

    public bool EstaCartaCumpleLogoSuperStar(Card card, string logoSuperStar)
    {   
        List<String> logoSuperStars = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        foreach (var logo in logoSuperStars)
        {   
            if (card.ContieneLogoSuperStar(logo) && logoSuperStar != logo)
            {   
                return false;
            }
        }
        return true;
    }
    
    public bool EsValidoMazo(Player mazo)
    {   
        return (CumpleTamanoMazo(mazo) && CumpleTenerSuperStar(mazo) && CumpleSubtipos(mazo) && MazoCumpleLogoSuperStar(mazo));
    }
}