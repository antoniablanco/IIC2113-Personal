using RawDeal.CardClass;
using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;
using RawDealView;

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
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo.GetCurrentPlayer(), 1);
        gameStructureInfo.Effects.DiscardingCardsFromHandToArsenal(gameStructureInfo.ControllerCurrentPlayer);
    }

    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheArsenal() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}