using RawDeal.DecksBehavior;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public abstract class SuperStar
{
    protected CardsVisualizor CardsVisualizor = new CardsVisualizor();
    public int HandSize;
    public string Logo;
    public string Name;
    public string SuperstarAbility;
    public int SuperstarValue;
    protected View View;

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

    public virtual void BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }
}