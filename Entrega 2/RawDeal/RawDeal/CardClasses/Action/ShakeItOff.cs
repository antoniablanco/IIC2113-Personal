using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class ShakeItOff: Card
{
    public ShakeItOff(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {
        return gameStructureInfo.ControllerCurrentPlayer.FortitudeRating() <
               gameStructureInfo.ControllerOpponentPlayer.FortitudeRating();
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        new MoveLessThanDOpponentEffect(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo);
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer());
    }
}