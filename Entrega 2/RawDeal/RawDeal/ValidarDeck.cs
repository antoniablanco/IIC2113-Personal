namespace RawDeal;

public class ValidarDeck
{
    public bool CumpleTamanoMazo(Mazo mazo)
    {
        return (mazo.cartasArsenal.Count() == 60);
    }
    
    public bool TieneSuperStar(Mazo mazo)
    {
        return (mazo.superestar != null);
    }

    public bool CumpleSubtipos(Mazo mazo)
    {
        Dictionary<string, int> dictCount = new Dictionary<string, int>();
        bool isHeel = false;
        bool isFace = false;
        
        foreach (var carta in mazo.cartasArsenal)
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

        return true;
    }
    
    public bool ContieneLogoSuperStar(Mazo mazo)
    {   
        List<String> nameSuperStars = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        foreach (var carta in mazo.cartasArsenal)
        {   
            foreach (var logo in nameSuperStars)
            {   
                if (carta.ContieneLogoSuperStar(logo) && mazo.superestar.Logo != logo)
                {   
                    return false;
                }
            }
        }

        return true;
    }
    
    public bool EsValidoMazo(Mazo mazo)
    {
        return (CumpleTamanoMazo(mazo) && TieneSuperStar(mazo) && CumpleSubtipos(mazo) && ContieneLogoSuperStar(mazo));
    }
}