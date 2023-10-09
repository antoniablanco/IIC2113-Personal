using RawDeal.CardClass;
using RawDeal.PlayerClass;

namespace RawDeal.DecksBehavior;

public class PlayCard
{

    public GameStructureInfo gameStructureInfo = new GameStructureInfo();
    private PlayReversal PlayReversal = new PlayReversal();
    public bool isUserReversalDeckCard = false;
    public bool isStunValueUsed = false;

    public void ActionPlayCard()
    {
        int selectedCard = gameStructureInfo.view.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (IsValidIndexOfCard(selectedCard))
        {
            Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
            ShouldIDesactivateJockeyingForPositionEfectt(playedCardController.Item1);
            SetLastPlayedCardInfo(playedCardController);
            PlayReversal.gameStructureInfo = gameStructureInfo;
            VerifinIfIsUsedAReversalCard(playedCardController);
            gameStructureInfo.ContadorTurnosJokeyingForPosition -= 1;
        }
    }
    
    private void ShouldIDesactivateJockeyingForPositionEfectt(CardController cardController)
    {   
        if (!cardController.VerifyIfContainSubtype("Grapple") || gameStructureInfo.ContadorTurnosJokeyingForPosition <= 0)
        {
            DesactivatingJockeyForPositionEffect();
        }
    }

    private void DesactivatingJockeyForPositionEffect()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamage = 0;
        gameStructureInfo.IsJockeyingForPositionBonusFortitud = 0;
    }
    
    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<Tuple<CardController, int>> allCardsAndTheirTypes =
            gameStructureInfo.VisualizeCards.GetPosiblesCardsToPlay(possibleCardsToPlay);

        return allCardsAndTheirTypes[indexSelectedCard];

    }
    
    private void VerifinIfIsUsedAReversalCard(Tuple<CardController, int> playedCardController)
    {
        if (!PlayReversal.IsUserUsingReversalCard())
        {   
            gameStructureInfo.view.SayThatPlayerSuccessfullyPlayedACard();
            PlayCardByType(playedCardController);
        }
    }
    
    private void SetLastPlayedCardInfo(Tuple<CardController, int> playedCardController)
    {
        gameStructureInfo.LastPlayedCard = playedCardController.Item1;
        gameStructureInfo.LastPlayedType = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
        SayThatTheyAreGoingToPlayACard(playedCardController.Item1, playedCardController.Item2);
    }
    
    private void PlayCardByType(Tuple<CardController, int> playedCardController)
    {   
        if (playedCardController.Item1.GetCardTypes()[playedCardController.Item2] == "Maneuver")
        {
            PlayManeuverCard(playedCardController.Item1);
        }
        else if (playedCardController.Item1.GetCardTypes()[playedCardController.Item2] == "Action")
        {
            PlayActionCard(playedCardController.Item1);
        }
        else
            Console.WriteLine("No se encuentra el tipo de carta");
        
    }

    private void PlayManeuverCard(CardController playedCardController)
    {
        PrintActionPlayCard(playedCardController);
        if (!isUserReversalDeckCard)
            gameStructureInfo.GameLogic.AddCardPlayedToRingArea(playedCardController, gameStructureInfo.GetCurrentPlayer());
        else
            gameStructureInfo.GameLogic.AddCardPlayedToRingArea(playedCardController, gameStructureInfo.GetOpponentPlayer());
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
        int totalDamage = GetDamageProduced(playedCardController) + gameStructureInfo.bonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamage;
        if (totalDamage > 0)
            SayThatTheyAreGoingToReceiveDamage(totalDamage);
        CauseDamageActionPlayCard(totalDamage, gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
    }
    
    public void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        isUserReversalDeckCard = false;
        isStunValueUsed = false;
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckCanReceiveDamage(controllerOpponentPlayer) && !isUserReversalDeckCard)
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
            else if (isUserReversalDeckCard && gameStructureInfo.LastPlayedCard.TheCardHadStunValue() && !isStunValueUsed)
                UseStunValueOpcion();
            else if (!CheckCanReceiveDamage(controllerOpponentPlayer))
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning();
        }
    }

    private void UseStunValueOpcion()
    {   
        isStunValueUsed = true;
        int numberOfCardsToSteal = gameStructureInfo.view.AskHowManyCardsToDrawBecauseOfStunValue(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), gameStructureInfo.LastPlayedCard.GetCardStunValue());
        gameStructureInfo.CardEffects.StealCard(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
    }
    
    private bool CheckCanReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player, PlayerController controllerOpponentPlayer)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.VisualizeCards.GetStringCardInfo(flippedCardController);
        gameStructureInfo.view.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        DeckReversal(flippedCardController, controllerOpponentPlayer);
    }

    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer)
    {
        isUserReversalDeckCard = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer);
        if (isUserReversalDeckCard)
        {
            gameStructureInfo.view.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
            gameStructureInfo.CardEffects.EndTurn();
        }
    }
    
    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        string playedCardString = gameStructureInfo.VisualizeCards.GetStringPlayedInfo(playedCardController, indexType);
        string nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private int GetDamageProduced(CardController playedCardController)
    {
        int totalDamage = playedCardController.GetDamageProducedByTheCard() + gameStructureInfo.bonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamage;
        if (gameStructureInfo.ControllerOpponentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void PlayActionCard(CardController playedCardController)
    {
        gameStructureInfo.CardEffects.DiscardCard(playedCardController, gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
        gameStructureInfo.CardEffects.StealCard( gameStructureInfo.ControllerCurrentPlayer, gameStructureInfo.GetCurrentPlayer());
    }
}