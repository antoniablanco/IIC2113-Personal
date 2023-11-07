using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class StoneColdStunner: Card
{
    public StoneColdStunner(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override int PlusFornitudAfterEspecificCard(GameStructureInfo gameStructureInfo)
    {
        int fornitud = 0;
        if (gameStructureInfo.CardBeingPlayed != null)
        {
            if (gameStructureInfo.CardBeingPlayed.GetCardTitle() == "Kick" && gameStructureInfo.GetSetGameVariables.GetRoundsInTurn() > 1)
                fornitud = -6;
        }
        return fornitud;
    }
}