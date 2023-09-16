namespace RawDeal.SuperStarClases;
using RawDealView;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(Player currentPlayer, Player opponentPlayer)
    {
        currentPlayer.theHabilityHasBeenUsedThisTurn = true;
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        AddingCardFromArsenalToHand(currentPlayer);
        DiscardingCardsFromHandToArsenal(currentPlayer);
    }

    private void AddingCardFromArsenalToHand(Player currentPlayer)
    {
        _view.SayThatPlayerDrawCards(Name, 1);
        currentPlayer.TransferOfUnselectedCard(currentPlayer.cardsArsenal, currentPlayer.cardsHand);
    }

    private void DiscardingCardsFromHandToArsenal(Player currentPlayer)
    {
        List<string> handCardsAsString = visualisarCartas.CreateStringCardList(currentPlayer.cardsHand);
        int selectedCard = _view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, handCardsAsString);
        
        Card discardedCard = currentPlayer.cardsHand[selectedCard];
        currentPlayer.CardTransferChoosingWhichOneToChange(discardedCard,  currentPlayer.cardsHand,currentPlayer.cardsArsenal, "Start");
    }
    
    public override bool CanUseSuperAbility(Player currentPlayer)
    {
        return (currentPlayer.cardsArsenal.Count > 0 && !currentPlayer.theHabilityHasBeenUsedThisTurn);
    }
}