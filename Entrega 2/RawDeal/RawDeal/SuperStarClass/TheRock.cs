using RawDeal.CardClass;
using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;
using RawDealView;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {  
        if (CanUseSuperAbility(gameStructureInfo.ControllerCurrentPlayer))
        {
            gameStructureInfo.ControllerCurrentPlayer.TheSuperStarHasUsedHisSuperAbilityThisTurn();
            if (_view.DoesPlayerWantToUseHisAbility(Name))
            {
                AddingCardFromRingSideToArsenal(gameStructureInfo);
            }
        }
    }
    
    private void AddingCardFromRingSideToArsenal(GameStructureInfo gameStructureInfo)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        List<string> ringAreaAsString = gameStructureInfo.ControllerCurrentPlayer.StringCardsRingSide();
        int selectedCardIndex = _view.AskPlayerToSelectCardsToRecover(Name, 1, ringAreaAsString);
        CardController discardedCardController = gameStructureInfo.ControllerCurrentPlayer.GetSpecificCardFromRingSide(selectedCardIndex);
        
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(player, discardedCardController, "Start");
    }
    
    public override bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return currentPlayer.NumberOfCardsInRingSide() > 0 && !currentPlayer.HasTheSuperAbilityBeenUsedThisTurn();
    }
}