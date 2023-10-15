using RawDeal.DecksBehavior;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public abstract class SuperStar
{
    private string name;
    private string logo;
    private int handSize;
    private int superstarValue;
    private string superstarAbility;
    public View View;
    protected CardsVisualizor CardsVisualizor = new CardsVisualizor();

    public SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
    {
        this.name = name;
        this.logo = logo;
        this.handSize = handSize;
        this.superstarValue = superstarValue;
        this.superstarAbility = superstarAbility;
        View = view;
    }

    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Logo
    {
        get => logo;
        set => logo = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int HandSize
    {
        get => handSize;
        set => handSize = value;
    }
    
    public int SuperstarValue
    {
        get => superstarValue;
        set => superstarValue = value;
    }
    
    public string SuperstarAbility
    {
        get => superstarAbility;
        set => superstarAbility = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return false;
    }

    public virtual void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        
    }
    
    public virtual void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }
    
    public virtual void BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }
    
}