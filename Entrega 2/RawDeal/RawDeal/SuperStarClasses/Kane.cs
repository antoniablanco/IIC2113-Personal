using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Kane: SuperStar 
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        
    }

    public override void UseAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {   
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        const int totalDamage = 1;
        new TakeDamageEffectUtils(gameStructureInfo.ControllerOpponentPlayer,totalDamage, gameStructureInfo);
    }
}