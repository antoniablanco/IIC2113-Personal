using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;
using RawDealView;

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
            if (_view.DoesPlayerWantToUseHisAbility(Name))
            {
                _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
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