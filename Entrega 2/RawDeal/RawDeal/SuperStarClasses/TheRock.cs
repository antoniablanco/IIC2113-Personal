using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {  
        if (CanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer))
        {
            gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
            if (View.DoesPlayerWantToUseHisAbility(Name))
            {
                View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
                gameStructureInfo.Effects.AddingCardFromRingSideToArsenal(gameStructureInfo
                    .ControllerCurrentPlayer);
            }
        }
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return currentPlayer.NumberOfCardsInRingSide() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn();
    }
    
    public override void BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
    }
}