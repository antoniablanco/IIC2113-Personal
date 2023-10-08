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
        
        AddingCardFromArsenalToHand(gameStructureInfo);
        DiscardingCardsFromHandToArsenal(gameStructureInfo);
    }

    private void AddingCardFromArsenalToHand(GameStructureInfo gameStructureInfo)
    {
        _view.SayThatPlayerDrawCards(Name, 1);
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
    }

    private void DiscardingCardsFromHandToArsenal(GameStructureInfo gameStructureInfo)
    {
        List<string> handCardsAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsHand();
        int selectedCard = _view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, handCardsAsString);
        
        CardController discardedCardController = gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFromHand(selectedCard);
        
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToArsenal(player, discardedCardController, "Start");
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return (currentPlayer.NumberOfCardsInTheArsenal() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn());
    }
}