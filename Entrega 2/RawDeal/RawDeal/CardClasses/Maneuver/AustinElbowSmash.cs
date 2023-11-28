using RawDeal.GameClasses;

namespace RawDeal.CardClasses.Maneuver;

public class AustinElbowSmash : Card
{
    public AustinElbowSmash(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
    }

    public override bool CheckIfCardCanBeReverted()
    {
        return false;
    }

    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {   
        return gameStructureInfo.LastDamageCommitted >= 5 && gameStructureInfo.CardBeingPlayedType == "Maneuver";
    }
}