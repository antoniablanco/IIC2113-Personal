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

    public virtual void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        
    }

    public virtual void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        
    }
    
    public virtual void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
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