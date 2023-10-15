using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;
using RawDealView;

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
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.Effects.TakeDamage(gameStructureInfo.ControllerOpponentPlayer, player,1);
    }
}