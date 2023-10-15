using System.Data;
using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayCard
{

    private GameStructureInfo gameStructureInfo = new GameStructureInfo();
    private bool isUserReversalDeckCard = false;
    private bool isStunValueUsed = false;

    public PlayCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void ActionPlayCard()
    {
        int selectedCard = gameStructureInfo.View.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (IsValidIndexOfCard(selectedCard))
        {
            Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
            CheckingJockeyForPosition(playedCardController.Item1);
            SetLastPlayedCardInfo(playedCardController);
            VerifinIfIsUsedAReversalCard(playedCardController);
        }
        else
            gameStructureInfo.GetSetGameVariables.AddingOneTurnJockeyingForPosition();
    }
    
    private void CheckingJockeyForPosition(CardController cardController)
    {
        if (JockeyingForPositionEffectShouldNotBeActive(cardController))
            DesactivateJockeyForPositionEffect();
    }

    private bool JockeyingForPositionEffectShouldNotBeActive(CardController cardController)
    {
        return (!cardController.ContainsSubtype("Grapple") || gameStructureInfo.TurnCounterForJokeyingForPosition <= 0 || (gameStructureInfo.WhoActivateJockeyingForPosition != gameStructureInfo.ControllerCurrentPlayer && gameStructureInfo.WhoActivateJockeyingForPosition != null));
    }
    

    private void DesactivateJockeyForPositionEffect()
    {
        gameStructureInfo.IsJockeyingForPositionBonusDamageActive = 0;
        gameStructureInfo.IsJockeyingForPositionBonusFortitudActive = 0;
    }
    
    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<Tuple<CardController, int>> allCardsAndTheirTypes =
            gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlay(possibleCardsToPlay);

        return allCardsAndTheirTypes[indexSelectedCard];

    }
    
    private void VerifinIfIsUsedAReversalCard(Tuple<CardController, int> playedCardController)
    {
        PlayReversalHand playReversalHand = new PlayReversalHand(gameStructureInfo);
        if (!playReversalHand.IsUserUsingReversalCard())
        {   
            gameStructureInfo.View.SayThatPlayerSuccessfullyPlayedACard();
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
        string typeCard = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
        switch (typeCard)
        {
            case "Maneuver":
                PlayManeuverCard(playedCardController.Item1);
                break;
            case "Action":
                PlayActionCard(playedCardController.Item1);
                break;
        }
    }

    private void PlayManeuverCard(CardController playedCardController)
    {
        PrintActionPlayCard(playedCardController);
        if (!isUserReversalDeckCard)
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetCurrentPlayer(), playedCardController);

        else
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), playedCardController);

    }

    private bool IsValidIndexOfCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private List<string> GetPossibleCardsToPlayString()
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<string> cardsStrings = gameStructureInfo.CardsVisualizor.CreateStringPlayedCardListForNotReversalType(possibleCardsToPlay, gameStructureInfo.ControllerCurrentPlayer);
        return cardsStrings;
    }

    private void PrintActionPlayCard(CardController playedCardController)
    {
        int totalDamage = GetDamageProduced(playedCardController);
        gameStructureInfo.LastDamageComited = totalDamage;
        playedCardController.ManeuverEffect(playedCardController);
        if (totalDamage > 0 && gameStructureInfo.IsTheGameStillPlaying)
        {
            SayThatTheyAreGoingToReceiveDamage(totalDamage);
            CauseDamageActionPlayCard(totalDamage, gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer());
        }
    }
    
    public void CauseDamageActionPlayCard(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        isUserReversalDeckCard = false;
        isStunValueUsed = false;
        for (int currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {   
            if (CheckCanReceiveDamage(controllerOpponentPlayer) && !isUserReversalDeckCard)
                ShowOneFaceDownCard(currentDamage + 1, totalDamage, player, controllerOpponentPlayer);
            else if (isUserReversalDeckCard && gameStructureInfo.LastPlayedCard.TheCardHadStunValue() &&
                     !isStunValueUsed)
            {   
                UseStunValueOpcion();
            }
            else if (!CheckCanReceiveDamage(controllerOpponentPlayer))
            {
                gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
                break;
            }
        }
    }

    private void UseStunValueOpcion()
    {   
        isStunValueUsed = true;
        int numberOfCardsToSteal = gameStructureInfo.View.AskHowManyCardsToDrawBecauseOfStunValue(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), gameStructureInfo.LastPlayedCard.GetCardStunValue());
        gameStructureInfo.Effects.StealCards(gameStructureInfo.ControllerOpponentPlayer, gameStructureInfo.GetOpponentPlayer(), numberOfCardsToSteal);
    }
    
    private bool CheckCanReceiveDamage(PlayerController controllerOpponentPlayer)
    {
        return controllerOpponentPlayer.AreThereCardsLeftInTheArsenal();
    }

    private void ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player, PlayerController controllerOpponentPlayer)
    {
        CardController flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        string flippedCardString = gameStructureInfo.CardsVisualizor.GetStringCardInfo(flippedCardController);
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        DeckReversal(flippedCardController, controllerOpponentPlayer);
    }
    
    private void DeckReversal(CardController flippedCardController, PlayerController controllerOpponentPlayer)
    {
        isUserReversalDeckCard = flippedCardController.CanUseThisReversalCard(controllerOpponentPlayer);
        if (isUserReversalDeckCard)
        {   
            Console.WriteLine(gameStructureInfo.IsTheGameStillPlaying);
            gameStructureInfo.Effects.EndTurn();
            gameStructureInfo.View.SayThatCardWasReversedByDeck(controllerOpponentPlayer.NameOfSuperStar());
        }
    }
    
    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        string playedCardString = gameStructureInfo.CardsVisualizor.GetStringPlayedInfo(playedCardController, indexType);
        string nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private int GetDamageProduced(CardController playedCardController)
    {   
        int totalDamage = playedCardController.GetDamageProducedByTheCard() + gameStructureInfo.BonusDamage*gameStructureInfo.IsJockeyingForPositionBonusDamageActive;
        if (gameStructureInfo.ControllerOpponentPlayer.IsTheSuperStarMankind())
            totalDamage -= 1;
        return totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void PlayActionCard(CardController playedCardController)
    {   
        playedCardController.ActionEffect();
    }
}