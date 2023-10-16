using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Jericho: SuperStar 
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        
        const int numberOfCardsToDiscard = 1;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard);
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.ControllerOpponentPlayer, numberOfCardsToDiscard);
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheHand() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}