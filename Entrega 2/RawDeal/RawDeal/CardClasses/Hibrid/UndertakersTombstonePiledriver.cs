using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Hibrid;

public class UndertakersTombstonePiledriver: Card
{
    public UndertakersTombstonePiledriver(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }

    public override int GetFortitude(string type)
    {
        return type switch
        {
            "Maneuver" => 30,
            "Action" => 0,
            _ => int.Parse(Fortitude)
        };
    }

    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.EffectsUtils.DiscardCardFromHandNotifying(playedCardController,
            gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards();
    }
}