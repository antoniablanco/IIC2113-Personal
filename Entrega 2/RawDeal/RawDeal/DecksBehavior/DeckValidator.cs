using RawDeal.CardClass;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class DeckValidator
{   
    private Player player;
    
    public DeckValidator(Player player)
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
        bool cardsMeetsQuantityCriteria = DeckMeetsQuantityCriteria();

        return (!(isDeckHeel && isDeckFace) && cardsMeetsQuantityCriteria);
    }

    private bool DeckMeetsQuantityCriteria()
    {
        var dictionaryNumberByCards = new Dictionary<string, int>();
        
        foreach (var card in player.cardsArsenal)
        {
            dictionaryNumberByCards.TryGetValue(card.GetCardTitle(), out int count);
            dictionaryNumberByCards[card.GetCardTitle()] = count + 1;

            if (SatisfiedCount(card, count))
                return false;
        }
        return true;
    }

    private bool SatisfiedCount(CardController card, int count)
    {
        return (card.ContainsUniqueSubtype() && count > 0) || (!card.ContainsSetUpSubtype() && count > 2);
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

    private bool ThisCardSatisfiesSuperStarLogo(CardController cardController, string logoSuperStar)
    {   
        List<String> superstarLogos = new List<string> {"StoneCold", "Undertaker","Mankind", "HHH","TheRock","Kane","Jericho"};
        return superstarLogos.All(logo => !cardController.ContainsSuperStarLogo(logo) || logoSuperStar == logo);
    }
    
}