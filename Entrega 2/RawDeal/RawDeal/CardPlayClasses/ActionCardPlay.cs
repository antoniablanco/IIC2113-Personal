using RawDeal.CardClasses;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class ActionCardPlay
{
    private GameStructureInfo gameStructureInfo;

    public ActionCardPlay(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void PlayCard(CardController playedCardController)
    {
        playedCardController.ApplyActionEffect();
        playedCardController.ApplyBonusEffect();
    }
}