using RawDeal.CardClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class DeckValidator
{
    private readonly Player player;

    public DeckValidator(Player player)
    {
        this.player = player;
    }

    public bool IsValidDeck()
    {
        return DeckSizeComplies() && HasSuperStar() && MeetsSubtypeConditions() && DeckSatisfiesSuperStarLogo();
    }

    private bool DeckSizeComplies()
    {
        return player.CardsArsenal.Count() == 60;
    }

    private bool HasSuperStar()
    {
        return player.Superestar != null;
    }

    private bool MeetsSubtypeConditions()
    {
        var isDeckHeel = DeckContainsSubType("Heel");
        var isDeckFace = DeckContainsSubType("Face");
        var cardsMeetsQuantityCriteria = DeckMeetsQuantityCriteria();

        return !(isDeckHeel && isDeckFace) && cardsMeetsQuantityCriteria;
    }

    private bool DeckContainsSubType(string subType)
    {
        return player.CardsArsenal.Any(card => card.ContainsSubtype(subType));
    }

    private bool DeckMeetsQuantityCriteria()
    {
        var dictionaryNumberByCards = new Dictionary<string, int>();

        foreach (var card in player.CardsArsenal)
        {
            dictionaryNumberByCards.TryGetValue(card.GetCardTitle(), out var numberOfCardsOfThisTitle);
            dictionaryNumberByCards[card.GetCardTitle()] = numberOfCardsOfThisTitle + 1;

            if (ExceedsMaximumSubtypeQuantity(card, numberOfCardsOfThisTitle))
                return false;
        }

        return true;
    }

    private bool ExceedsMaximumSubtypeQuantity(CardController card, int numberOfCardsOfThisTitle)
    {
        return (card.ContainsSubtype("Unique") && numberOfCardsOfThisTitle > 0) ||
               (!card.ContainsSubtype("SetUp") && numberOfCardsOfThisTitle > 2);
    }


    private bool DeckSatisfiesSuperStarLogo()
    {
        return player.CardsArsenal.All(card => ThisCardSatisfiesSuperStarLogo(card, player.Superestar.Logo));
    }

    private bool ThisCardSatisfiesSuperStarLogo(CardController cardController, string logoSuperStar)
    {
        var superstarLogos = new List<string>
            { "StoneCold", "Undertaker", "Mankind", "HHH", "TheRock", "Kane", "Jericho" };
        return superstarLogos.All(logo => !cardController.ContainsSuperStarLogo(logo) || logoSuperStar == logo);
    }
}