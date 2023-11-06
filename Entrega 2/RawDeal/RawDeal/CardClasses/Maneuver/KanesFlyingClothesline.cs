using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class KanesFlyingClothesline: Card
{
    public KanesFlyingClothesline(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {
        return gameStructureInfo.LastDamageComited >= 4 && gameStructureInfo.CardBeingPlayedType == "Maneuver";
    }
    
    public override int ExtraReversalDamage(GameStructureInfo gameStructureInfo)
    {
        return 6;
    }
}