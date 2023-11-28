using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Action;

public class SpitAtOpponent: Card
{
    public SpitAtOpponent(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }

    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        const int numberOfCardToDiscardCurrentPlayer = 1;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardToDiscardCurrentPlayer, gameStructureInfo);

        const int numberOfCardToDiscardOpponentPlayer = 4;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerOpponentPlayer, 
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscardOpponentPlayer, gameStructureInfo);

        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }

    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {
        return gameStructureInfo.ControllerCurrentPlayer.GetNumberOfCardIn("Hand") >= 2;
    }
}