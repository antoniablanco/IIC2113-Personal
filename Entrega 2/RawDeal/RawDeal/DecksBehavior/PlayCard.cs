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
            Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
            PlayCardByType(playedCardController);
        }
    }
    
    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard) 
    {   
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<Tuple<CardController, int>> allCardsAndTheirTypes = gameStructureInfo.VisualizeCards.GetPosiblesCardsToPlay(possibleCardsToPlay);

        return allCardsAndTheirTypes[indexSelectedCard];

    }
    
    private void PlayCardByType(Tuple<CardController, int> playedCardController)
    {
        if (playedCardController.Item1.GetCardTypes()[playedCardController.Item2] == "Maneuver")
        {
            PlayManeuverCard(playedCardController.Item1, playedCardController.Item2);
        }
        else if (playedCardController.Item1.GetCardTypes()[playedCardController.Item2] == "Action")
        {
            PlayActionCard(playedCardController.Item1, playedCardController.Item2);
        }
        else 
            Console.WriteLine("No se encuentra el tipo de carta");
    }
    
    private void PlayManeuverCard(CardController playedCardController, int indexType)
    {
        PrintActionPlayCard(playedCardController, indexType);
        gameStructureInfo.GameLogic.AddCardPlayedToRingArea(playedCardController);
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

    private void PrintActionPlayCard(CardController playedCardController, int indexType)
    {   
        SayThatTheyAreGoingToPlayACard(playedCardController, indexType);
        int totalDamage = GetDamageProduced(playedCardController);
        if (totalDamage > 0)
            SayThatTheyAreGoingToReceiveDamage(totalDamage);
        CauseDamageActionPlayCard(totalDamage);
    }

    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        string playedCardString = gameStructureInfo.VisualizeCards.GetStringPlayedInfo(playedCardController, indexType);
        string nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
        gameStructureInfo.view.SayThatPlayerSuccessfullyPlayedACard();
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
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning();
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

    private void PlayActionCard(CardController playedCardController, int indexType)
    {
        SayThatTheyAreGoingToPlayACard(playedCardController, indexType);
        gameStructureInfo.view.SayThatPlayerMustDiscardThisCard(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar(), playedCardController.GetCardTitle());
        gameStructureInfo.view.SayThatPlayerDrawCards(gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar(), 1);
        DiscardActionCard(playedCardController);
        StealCard();
    }

    private void DiscardActionCard(CardController playedCardController)
    {
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(player, playedCardController);
    }
    
    private void StealCard()
    {   
        Player player = gameStructureInfo.GetCurrentPlayer();
        gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToHand(player);
    }
    
}