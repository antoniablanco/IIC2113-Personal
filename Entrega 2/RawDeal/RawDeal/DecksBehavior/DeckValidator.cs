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
        bool isDeckHeel = DeckContainsSubType("Heel");
        bool isDeckFace = DeckContainsSubType("Face");
        bool cardsMeetsQuantityCriteria = DeckMeetsQuantityCriteria();

        return (!(isDeckHeel && isDeckFace) && cardsMeetsQuantityCriteria);
    }

    private bool DeckMeetsQuantityCriteria()
    {
        var dictionaryNumberByCards = new Dictionary<string, int>();
        
        foreach (var card in player.cardsArsenal)
        {
            dictionaryNumberByCards.TryGetValue(card.GetCardTitle(), out int numberOfCardsOfThisTitle);
            dictionaryNumberByCards[card.GetCardTitle()] = numberOfCardsOfThisTitle + 1;

            if (SatisfiedCount(card, numberOfCardsOfThisTitle))
                return false;
        }
        return true;
    }

    private bool SatisfiedCount(CardController card, int numberOfCardsOfThisTitle)
    {
        return (card.ContainsSubtype("Unique") && numberOfCardsOfThisTitle > 0) || (!card.ContainsSubtype("SetUp") && numberOfCardsOfThisTitle > 2);
    }
    
    private bool DeckContainsSubType(string subType)
    {
        return player.cardsArsenal.Any(card => card.ContainsSubtype(subType));
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