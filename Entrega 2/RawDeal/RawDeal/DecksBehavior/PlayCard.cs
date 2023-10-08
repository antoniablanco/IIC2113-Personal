using RawDeal.CardClass;
using RawDeal.PlayerClass;

namespace RawDeal.DecksBehavior;

public class PlayCard
{   
    
    public GameStructureInfo gameStructureInfo= new GameStructureInfo();
    
    public void ActionPlayCard() 
    {
        int selectedCard = gameStructureInfo.view.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if ( IsValidIndexOfCard(selectedCard))
        {   
            CardController playedCardController = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay()[selectedCard];
            PrintActionPlayCard(playedCardController);
            gameStructureInfo.GameLogic.AddCardPlayedToRingArea(playedCardController);
        }
    }

    private bool IsValidIndexOfCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private List<string> GetPossibleCardsToPlayString()
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<string> cardsStrings = gameStructureInfo.VisualizeCards.CreateStringPlayedCardList(possibleCardsToPlay);
        return cardsStrings;
    }

    private void PrintActionPlayCard(CardController playedCardController)
    {   
        SayThatTheyAreGoingToPlayACard(playedCardController);
        gameStructureInfo.view.SayThatPlayerSuccessfullyPlayedACard();
        int totalDamage = GetDamageProduced(playedCardController);
        SayThatTheyAreGoingToReceiveDamage(totalDamage);
        CauseDamageActionPlayCard(totalDamage);
    }

    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController)
    {
        string playedCardString = gameStructureInfo.VisualizeCards.GetStringPlayedInfo(playedCardController);
        string nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private int GetDamageProduced(CardController playedCardController)
    {
        int totalDamage = playedCardController.GetDamageProducedByTheCard();
        if (gameStructureInfo.ControllerOpponentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return  totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {   
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void CauseDamageActionPlayCard(int totalDamage) 
    {
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckCanReceiveDamage())
                ShowOneFaceDownCard(currentDamage+1, totalDamage);
            else
                gameStructureInfo.GameLogic.SetVariablesAfterWinning();
        }
    }

    private bool CheckCanReceiveDamage()
    {
        return gameStructureInfo.ControllerOpponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage) 
    {
        Player player = gameStructureInfo.GetOpponentPlayer();
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }
    
}