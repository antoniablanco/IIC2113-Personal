using System.Reflection;
using RawDealView.Formatters;
using RawDealView;

namespace RawDeal.SuperStarClases;

public abstract class SuperStar
{
    private string _Name;
    private string _Logo;
    private int _HandSize;
    private int _SuperstarValue;
    private string _SuperstarAbility;
    protected View _view;
    protected VisualisarCartas visualisarCartas = new VisualisarCartas();

    public SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
    {
        _Name = name;
        _Logo = logo;
        _HandSize = handSize;
        _SuperstarValue = superstarValue;
        _SuperstarAbility = superstarAbility;
        _view = view;
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

    public virtual bool PuedeUtilizarSuperAbility(Player jugadorCotrario, Player jugadorActual)
    {
        return false;
    }

    public virtual void UtilizandoSuperHabilidad(Player jugadorActual, Player jugadorCotrario)
    {
        
    }
}