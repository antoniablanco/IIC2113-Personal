using RawDeal.CardClasses;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class ActionCardPlay
{
    public void PlayCard(CardController playedCardController)
    {
        playedCardController.ApplyActionEffect();
        playedCardController.ApplyBonusEffect();
    }
}