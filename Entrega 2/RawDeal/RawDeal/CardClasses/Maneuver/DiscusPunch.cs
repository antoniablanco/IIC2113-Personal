using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class DiscusPunch: Card
{
    public DiscusPunch(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override int GetExtraReversalDamage()
    {
        return 2;
    }
}