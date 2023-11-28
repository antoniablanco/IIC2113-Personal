using RawDeal.EffectsClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        
    }

    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {  
        if (CanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer))
        {
            gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUsedInThisTurn();
            if (View.DoesPlayerWantToUseHisAbility(Name))
            {
                View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
                new RingToArsenalEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
            }
        }
    }

    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return currentPlayer.NumberOfCardIn("RingSide") > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn();
    }

    public override void BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUsedInThisTurn();
    }
}