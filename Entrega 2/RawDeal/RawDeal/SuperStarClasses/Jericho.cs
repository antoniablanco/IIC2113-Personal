using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Jericho: SuperStar 
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        
    }

    public override void UseElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUsedInThisTurn();
        
        const int numberOfCardsToDiscard = 1;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerOpponentPlayer, 
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardsToDiscard, gameStructureInfo);
        
    }

    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.GetNumberOfCardIn("Hand") > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}