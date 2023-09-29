namespace RawDeal.CardClass.UnspecifiedType;

public class Pedigree: Card
{
    public Pedigree(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
}