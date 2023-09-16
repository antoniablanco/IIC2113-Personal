namespace RawDeal.SuperStarClases;
using RawDealView;

public class Undertaker: SuperStar
{
    public Undertaker(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(Player currentPlayer, Player opponentPlayer)
    {
        currentPlayer.theHabilityHasBeenUsedThisTurn = true;
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        for (int currentDamageNumber = 0; currentDamageNumber < 2; currentDamageNumber++)
        {   
            DiscardingCardsFromHandToRingSide(currentPlayer, 2-currentDamageNumber);
        }
        AddingCardFromRingSideToHand(currentPlayer);
    }
    

    public override bool CanUseSuperAbility(Player currentPlayer)
    {
        return (currentPlayer.cardsHand.Count > 1 && !currentPlayer.theHabilityHasBeenUsedThisTurn);
    }
}