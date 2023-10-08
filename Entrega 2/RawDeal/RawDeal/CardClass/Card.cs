namespace RawDeal.CardClass;

public abstract class Card
{
    private string _Title;
    private List<string> _Types = new List<string>();
    private List<string> _Subtypes = new List<string>();
    private string _Fortitude;
    private string _Damage;
    private string _StunValue;
    private string _CardEffect;

    public Card(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
    {
        _Title = title;
        _Types = types;
        _Subtypes = subtypes;
        _Fortitude = fortitude;
        _Damage = damage;
        _StunValue = stunValue;
        _CardEffect = cardEffect;
    }
    
    public string Title
    {
        get => _Title;
        set => _Title = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<string> Types
    {
        get => _Types;
        set => _Types = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public List<string> Subtypes
    {
        get => _Subtypes;
        set => _Subtypes = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Fortitude
    {
        get => _Fortitude;
        set => _Fortitude = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Damage
    {
        get => _Damage;
        set => _Damage = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string StunValue
    {
        get => _StunValue;
        set => _StunValue = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string CardEffect
    {
        get => _CardEffect;
        set => _CardEffect = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual bool CanReversalThisCard(CardController playedCardController)
    {
        return true;
    }
}