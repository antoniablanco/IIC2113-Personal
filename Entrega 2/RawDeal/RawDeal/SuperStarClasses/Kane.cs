using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Kane: SuperStar 
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {   
        Player player = gameStructureInfo.GetOpponentPlayer();
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.Effects.TakeDamage(gameStructureInfo.ControllerOpponentPlayer, player,1);
    }
}