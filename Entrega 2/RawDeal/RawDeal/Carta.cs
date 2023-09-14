using System.Reflection;
using RawDealView.Formatters;

namespace RawDeal;


public class CartasJson
{
    public string Title { get; set; }
    public List<string> Types { get; set; }
    public List<string> Subtypes { get; set; }
    public string Fortitude { get; set; }
    public string Damage { get; set; }
    public string StunValue { get; set; }
    public string CardEffect { get; set; }
}

public class Carta
{
    private string _Title;
    private List<string> _Types = new List<string>();
    private List<string> _Subtypes = new List<string>();
    private string _Fortitude;
    private string _Damage;
    private string _StunValue;
    private string _CardEffect;

    public Carta(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
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
    
    public bool ContieneSubtipoUnique() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Unique")
                return true;
        }
        return false;
    }
    
    public bool ContieneSubtipoSetUp()
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="SetUp")
                return true;
        }
        return false;
    }
    
    public bool ContieneSubtipoHeel() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Heel")
                return true;
        }
        return false;
    }
    
    public bool ContieneSubtipoFace() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Face")
                return true;
        }
        return false;
    }

    public bool ContieneLogoSuperStar(string superStarLogo)
    {   
        for (int i = 0; i < Subtypes.Count; i++)
        {
            if (Subtypes[i].Contains(superStarLogo))
            {
                return true;
            }
        }
        return false;
    }
}
