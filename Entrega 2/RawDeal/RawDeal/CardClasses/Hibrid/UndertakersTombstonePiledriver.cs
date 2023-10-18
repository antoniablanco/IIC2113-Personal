using RawDeal.GameClasses;

namespace RawDeal.CardClass.Hibrid;

public class UndertakersTombstonePiledriver: Card
{
    public UndertakersTombstonePiledriver(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override int GetFortitude(string type)
    {   
        if (type == "Maneuver")
            return 30;
        else if (type == "Action")
            return 0;
        else
            return int.Parse(Fortitude);
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.Effects.DiscardCardFromHandNotifying(playedCardController, gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.Effects.StealCards( gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
    }
}