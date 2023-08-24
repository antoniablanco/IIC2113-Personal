using System.Reflection;

namespace RawDeal;

// Cartas myDeserializedClass = JsonConvert.DeserializeObject<List<Cartas>>(myJson);

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

public class Cartas
{
    private string _Title;
    private List<string> _Types = new List<string>();
    private List<string> _Subtypes = new List<string>();
    private string _Fortitude;
    private string _Damage;
    private string _StunValue;
    private string _CardEffect;

    public Cartas(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
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
    public bool IsUnique() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Unique")
                return true;
        }
        return false;
    }
    
    public bool IsSetUp() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="SetUp")
                return true;
        }
        return false;
    }
    
    public bool IsHeel() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Heel")
                return true;
        }
        return false;
    }
    
    public bool IsFace() 
    {
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i]=="Face")
                return true;
        }
        return false;
    }

    public bool containsLogoSuperStar(string superStarLogo)
    {   
        for (int i = 0; i < Subtypes.Count; i++)
        {   
            if (Subtypes[i].Contains(superStarLogo))
                return true;
        }
        return false;
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class SuperStarJSON
{
    public string Name { get; set; }
    public string Logo { get; set; }
    public int HandSize { get; set; }
    public int SuperstarValue { get; set; }
    public string SuperstarAbility { get; set; }
}

public class SuperStar
{
    private string _Name;
    private string _Logo;
    private int _HandSize;
    private int _SuperstarValue;
    private string _SuperstarAbility;

    public SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility)
    {
        _Name = name;
        _Logo = logo;
        _HandSize = handSize;
        _SuperstarValue = superstarValue;
        _SuperstarAbility = superstarAbility;
    }

    public string Name
    {
        get => _Name;
        set => _Name = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Logo
    {
        get => _Logo;
        set => _Logo = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int HandSize
    {
        get => _HandSize;
        set => _HandSize = value;
    }
    
    public int SuperstarValue
    {
        get => _SuperstarValue;
        set => _SuperstarValue = value;
    }
    
    public string SuperstarAbility
    {
        get => _SuperstarAbility;
        set => _SuperstarAbility = value ?? throw new ArgumentNullException(nameof(value));
    }
}