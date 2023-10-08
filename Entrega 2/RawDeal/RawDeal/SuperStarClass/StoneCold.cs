namespace RawDeal.SuperStarClass;
using RawDealView;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        AddingCardFromArsenalToHand(gameStructureInfo.ControllerCurrentPlayer);
        DiscardingCardsFromHandToArsenal(gameStructureInfo.ControllerCurrentPlayer);
    }

    private void AddingCardFromArsenalToHand(PlayerController currentPlayer)
    {
        _view.SayThatPlayerDrawCards(Name, 1);
        currentPlayer.TranferUnselectedCardFromArsenalToHand();
    }

    private void DiscardingCardsFromHandToArsenal(PlayerController currentPlayer)
    {
        List<string> handCardsAsString = currentPlayer.StringCardsHand();
        int selectedCard = _view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, handCardsAsString);
        
        CardController discardedCardController = currentPlayer.GetSpecificCardFromHand(selectedCard);
        currentPlayer.TransferChoosinCardFromHandToArsenal(discardedCardController, "Start");
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheArsenal() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}