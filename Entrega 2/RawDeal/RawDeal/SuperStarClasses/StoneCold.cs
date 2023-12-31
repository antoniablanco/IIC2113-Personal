using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        
    }

    public override void UseElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUsedInThisTurn();
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        const int numberOfCardsToSteal = 1;
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards(numberOfCardsToSteal);
        
        new HandToArsenalDiscardEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
    }


    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.GetNumberOfCardIn("Arsenal") > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}