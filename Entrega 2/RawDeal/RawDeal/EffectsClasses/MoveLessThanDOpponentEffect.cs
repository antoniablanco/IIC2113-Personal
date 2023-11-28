using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class MoveLessThanDOpponentEffect: EffectsUtils
{
    private PlayerController opponentPlayerController;
    private PlayerController currentPlayerController;
    private (List<String>, List<CardController>) posibleCards;
    
    public MoveLessThanDOpponentEffect(PlayerController currentPlayerController, 
        PlayerController opponentPlayerController, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.opponentPlayerController = opponentPlayerController;
        this.currentPlayerController = currentPlayerController;
        Apply();
    }
    
    private void Apply()
    {   
        int maximumDamage = currentPlayerController.FortitudeRating();
        posibleCards = opponentPlayerController.GetCardsFromRingAreaThatMeetDCriteria(maximumDamage);
        if (IsPositive(posibleCards .Item1.Count()))
        {
            int selectedCardIndex = gameStructureInfo.View.AskPlayerToSelectACardToDiscardFromRingArea(
                currentPlayerController.GetNameOfSuperStar(),
                opponentPlayerController.GetNameOfSuperStar(), posibleCards.Item1);
            DiscardACardOfPlayerChoice(selectedCardIndex);
        }
        else
            gameStructureInfo.View.SayThatNoCardMeetsTheConditionsToBeRemoved();
    }
    
    private void DiscardACardOfPlayerChoice(int selectedCardIndex)
    {   
        var discardCardController = posibleCards.Item2[selectedCardIndex];
        
        var playerWhoDiscardCard = opponentPlayerController == gameStructureInfo.ControllerCurrentPlayer
            ? gameStructureInfo.GetCurrentPlayer()
            : gameStructureInfo.GetOpponentPlayer();
        
        gameStructureInfo.CardMovement.TransferSelectedCardFromRingAreaToRingSide(playerWhoDiscardCard,
            discardCardController);
        
    }
}