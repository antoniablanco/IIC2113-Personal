using RawDealView.Formatters;

namespace RawDeal.CardClasses;

public class CardInfoImplementation : IViewableCardInfo
{
    public CardInfoImplementation(string title, string fortitude, string damage, string stunValue, List<string> types,
        List<string> subtypes, string cardEffect)
    {
        Title = title;
        Fortitude = fortitude;
        Damage = damage;
        StunValue = stunValue;
        Types = types;
        Subtypes = subtypes;
        CardEffect = cardEffect;
    }

    public string Title { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string CardEffect { get; set; }
}