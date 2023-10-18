using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class Undertaker: SuperStar
{
    public Undertaker(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        const int numberOfCardsToDiscard = 2;
        gameStructureInfo.Effects.DiscardCardsFromHandToRingSide(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard);
        gameStructureInfo.Effects.AddingChoosingCardFromRingSideToHand(gameStructureInfo.ControllerCurrentPlayer);
    }
    

    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardIn("Hand") > 1 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}