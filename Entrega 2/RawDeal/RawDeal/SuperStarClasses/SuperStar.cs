using RawDeal.DecksBehavior;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public abstract class SuperStar
{
    protected CardsVisualizer CardsVisualizer = new CardsVisualizer();
    public int HandSize;
    public string Logo;
    public string Name;
    public string SuperstarAbility;
    public int SuperstarValue;
    protected View View;

    protected SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
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

    public virtual void UseElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        
    }

    public virtual void UseAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }

    public virtual void BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }
}