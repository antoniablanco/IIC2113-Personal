using RawDeal.GameClasses;

namespace RawDeal.CardClass;

public abstract class Card
{
    public string Title;
    public List<string> Types = new List<string>();
    public List<string> Subtypes = new List<string>();
    public string Fortitude;
    public string Damage;
    public string StunValue;
    public string CardEffect;

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
    

    public virtual bool CanReversalThisCard(CardController playedCardController)
    {
        return true;
    }

    public virtual void ReversalEffect(GameStructureInfo gameStructureInfo)
    {
        
    }

    public virtual void ActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
    
    public virtual void ManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
    
    public virtual int GetFortitude(string type)
    {
        return int.Parse(Fortitude);
    }

    public virtual bool CardCanBeReverted()
    {
        return true;
    }
    
    public virtual bool CardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        return true;
    }
}