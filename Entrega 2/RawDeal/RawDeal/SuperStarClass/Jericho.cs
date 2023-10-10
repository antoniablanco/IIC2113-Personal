using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;
using RawDealView;

public class Jericho: SuperStar 
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        gameStructureInfo.CardEffects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerCurrentPlayer,1);
        gameStructureInfo.CardEffects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.ControllerOpponentPlayer,1);
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheHand() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}