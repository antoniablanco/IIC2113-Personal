using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        const int numberOfCardsToSteal = 1;
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerCurrentPlayer,gameStructureInfo.GetCurrentPlayer(), numberOfCardsToSteal);
        gameStructureInfo.Effects.DiscardingCardsFromHandToArsenal(gameStructureInfo.ControllerCurrentPlayer);
    }

    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardIn("Arsenal") > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}