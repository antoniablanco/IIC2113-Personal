using RawDeal.GameClasses;
using RawDealView.Formatters;

namespace RawDeal.CardClasses;

public abstract class Card : IViewableCardInfo
{
    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
    {
        Title = title;
        Types = types;
        Subtypes = subtypes;
        Fortitude = fortitude;
        Damage = damage;
        StunValue = stunValue;
        CardEffect = cardEffect;
    }

    public string Title { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string CardEffect { get; set; }

    
    
    public virtual bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, string reverseBy, int totaldamage)
    {
        return true;
    }
    
    public virtual void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
    }

    public virtual void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
    }

    public virtual void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
    }
    
    public virtual void ApplyBonusEffect(GameStructureInfo gameStructureInfo)
    {
    }
    
    

    public virtual int GetFortitude(string type)
    {
        return int.Parse(Fortitude);
    }

    public virtual bool CheckIfCardCanBeReverted()
    {
        return true;
    }

    public virtual bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    { 
        return true;
    }
}