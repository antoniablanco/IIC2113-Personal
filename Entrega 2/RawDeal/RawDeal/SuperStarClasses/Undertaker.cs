using RawDeal.EffectsClasses;
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

    public override void UseElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.MarkSuperAbilityAsUsedInThisTurn();
        View.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        const int numberOfCardsToDiscard = 2;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo.ControllerCurrentPlayer, numberOfCardsToDiscard, gameStructureInfo);

        new RingToHandEffectUtils(gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo);
    }


    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.GetNumberOfCardIn("Hand") > 1 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}