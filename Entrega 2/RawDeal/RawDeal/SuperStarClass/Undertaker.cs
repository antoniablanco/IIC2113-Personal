namespace RawDeal.SuperStarClass;
using RawDealView;

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
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        for (int currentDamageNumber = 0; currentDamageNumber < 2; currentDamageNumber++)
        {   
            DiscardingCardsFromHandToRingSide(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer, 2-currentDamageNumber);
        }
        AddingCardFromRingSideToHand(gameStructureInfo, gameStructureInfo.ControllerCurrentPlayer);
    }
    

    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheHand() > 1 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}