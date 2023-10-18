using RawDeal.CardClasses;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class PlayActionCard
{
    private GameStructureInfo gameStructureInfo;

    public PlayActionCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void PlayCard(CardController playedCardController)
    {
        playedCardController.ApplyActionEffect();
    }
}