using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class UndertakersFlyingClothesline: Card
{
    public UndertakersFlyingClothesline(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {
        return gameStructureInfo.LastDamageCommitted >= 5 && gameStructureInfo.CardBeingPlayedType == "Maneuver";
    }
    
    public override int GetExtraReversalDamage()
    {
        return 6;
    }
}