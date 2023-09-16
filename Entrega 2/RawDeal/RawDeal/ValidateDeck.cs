namespace RawDeal;

public class ValidateDeck
{
    private bool DeckSizeComplies(Player player)
    {
        return (player.cardsArsenal.Count() == 60);
    }

    private bool HasSuperStar(Player player)
    {   
        return (player.superestar != null);
    }

    private bool MeetsSubtypeConditions(Player player)
    {
        bool isDeckHeel = DeckContainsIsHeel(player);
        bool isDeckFace = DeckContainsIsFace(player);
        bool uniqueCardsMeetInfiniteCriteria = DeckMeetsUniqueQuantity(player);
        bool noSetUpCardsMeetCriteria = DeckMeetsNoSetUpQuantity(player);

        return (!(isDeckHeel && isDeckFace) && uniqueCardsMeetInfiniteCriteria && noSetUpCardsMeetCriteria);
    }

    private bool DeckMeetsUniqueQuantity(Player player)
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

    private bool DeckMeetsNoSetUpQuantity(Player player)
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

    private bool DeckContainsIsHeel(Player player)
    {
        return player.cardsArsenal.Any(carta => carta.ContainsHeelSubtype());
    }

    private static bool DeckContainsIsFace(Player player)
    {
        return player.cardsArsenal.Any(card => card.ContainsFaceSubtype());
    }

    private bool DeckSatisfiesSuperStarLogo(Player player)
    {
        return player.cardsArsenal.All(card => ThisCardSatisfiesSuperStarLogo(card, player.superestar.Logo));
    }

    private bool ThisCardSatisfiesSuperStarLogo(Card card, string logoSuperStar)
    {   
        List<String> superstarLogos = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        return superstarLogos.All(logo => !card.ContainsSuperStarLogo(logo) || logoSuperStar == logo);
    }
    
    public bool IsValidDeck(Player player)
    {   
        return (DeckSizeComplies(player) && HasSuperStar(player) && MeetsSubtypeConditions(player) && DeckSatisfiesSuperStarLogo(player));
    }
}