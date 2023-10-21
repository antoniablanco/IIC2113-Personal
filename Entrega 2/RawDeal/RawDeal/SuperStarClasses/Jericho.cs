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
        // Constructor de la clase base
    }

    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        
        const int numberOfCardsToDiscard = 1;
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);
        new DiscardCardsFromHandToRingSideEffect(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.ControllerOpponentPlayer, numberOfCardsToDiscard, gameStructureInfo);
        
    }

    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardIn("Hand") > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}

public class DiscardCardsFromHandToRingSide
{
    public DiscardCardsFromHandToRingSide(PlayerController controllerCurrentPlayer, PlayerController playerController, int numberOfCardsToDiscard, GameStructureInfo gameStructureInfo)
    {
        throw new NotImplementedException();
    }
}