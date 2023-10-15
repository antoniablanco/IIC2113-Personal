using RawDeal.DecksBehavior;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public abstract class SuperStar
{
    public string Name;
    public string Logo;
    public int HandSize;
    public int SuperstarValue;
    public string SuperstarAbility;
    protected View View;
    protected CardsVisualizor CardsVisualizor = new CardsVisualizor();

    public SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
    {
        Name = name;
        Logo = logo;
        HandSize = handSize;
        SuperstarValue = superstarValue;
        SuperstarAbility = superstarAbility;
        View = view;
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