namespace RawDeal.SuperStarClass;
using RawDealView;

public class Jericho: SuperStar 
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(PlayerController currentPlayer, PlayerController opponentPlayer)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        currentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        DiscardingCardsFromHandToRingSide(currentPlayer, 1);
        DiscardingCardsFromHandToRingSide(opponentPlayer, 1);
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheHand() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}