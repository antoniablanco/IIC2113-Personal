namespace RawDeal.SuperStarClases;
using RawDealView;

public class Jericho: SuperStar 
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(Player currentPlayer, Player opponentPlayer)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        currentPlayer.theHabilityHasBeenUsedThisTurn = true;
        DiscardingCardsFromHandToRingSide(currentPlayer, 1);
        DiscardingCardsFromHandToRingSide(opponentPlayer, 1);
    }
    
    public override bool CanUseSuperAbility(Player currentPlayer)
    {
        return (currentPlayer.cardsHand.Count > 0 && !currentPlayer.theHabilityHasBeenUsedThisTurn);
    }
}