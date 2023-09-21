namespace RawDeal;

public class ValidateDeck
{   
    private Player player;
    
    public ValidateDeck(Player player)
    {
        this.player = player;
    }
    public bool IsValidDeck()
    {   
        return (DeckSizeComplies() && HasSuperStar() && MeetsSubtypeConditions() && DeckSatisfiesSuperStarLogo());
    }
    
    private bool DeckSizeComplies()
    {
        return (player.cardsArsenal.Count() == 60);
    }

    private bool HasSuperStar()
    {   
        return (player.superestar != null);
    }

    private bool MeetsSubtypeConditions()
    {
        bool isDeckHeel = DeckContainsIsHeel();
        bool isDeckFace = DeckContainsIsFace();
        bool uniqueCardsMeetInfiniteCriteria = DeckMeetsUniqueQuantity();
        bool noSetUpCardsMeetCriteria = DeckMeetsNoSetUpQuantity();

        return (!(isDeckHeel && isDeckFace) && uniqueCardsMeetInfiniteCriteria && noSetUpCardsMeetCriteria);
    }

    private bool DeckMeetsUniqueQuantity()
    {
        var dictionaryNumberByCards = new Dictionary<string, int>();
        
        foreach (var card in player.cardsArsenal)
        {
            dictionaryNumberByCards.TryGetValue(card.Title, out int count);
            dictionaryNumberByCards[card.Title] = count + 1;

            if (card.ContainsUniqueSubtype() && count > 0)
                return false;
        }
        return true;
    }

    private bool DeckMeetsNoSetUpQuantity()
    {
        var dictionaryNumberByCards = new Dictionary<string, int>();
        
        foreach (var card in player.cardsArsenal)
        {
            dictionaryNumberByCards.TryGetValue(card.Title, out int count);
            dictionaryNumberByCards[card.Title] = count + 1;

            if (!card.ContainsSetUpSubtype() && count > 2)
                return false;
        }

        return true;
    }

    private bool DeckContainsIsHeel()
    {
        return player.cardsArsenal.Any(carta => carta.ContainsHeelSubtype());
    }

    private bool DeckContainsIsFace()
    {
        return player.cardsArsenal.Any(card => card.ContainsFaceSubtype());
    }

    private bool DeckSatisfiesSuperStarLogo()
    {
        return player.cardsArsenal.All(card => ThisCardSatisfiesSuperStarLogo(card, player.superestar.Logo));
    }

    private bool ThisCardSatisfiesSuperStarLogo(Card card, string logoSuperStar)
    {   
        List<String> superstarLogos = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        return superstarLogos.All(logo => !card.ContainsSuperStarLogo(logo) || logoSuperStar == logo);
    }
    
}