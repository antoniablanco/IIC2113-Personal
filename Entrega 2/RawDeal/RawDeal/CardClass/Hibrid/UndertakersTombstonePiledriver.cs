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
    
    public override int GetDamage(string type)
    {   
        if (type == "Maneuver")
            return 0;
        else if (type == "Action")
            return 25;
        else
            return int.Parse(Damage);
    }
    
    public override void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        gameStructureInfo.CardEffects.DiscardActionCardWithNoEfect(playedCardController, gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.CardEffects.StealCards( gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
    }
}