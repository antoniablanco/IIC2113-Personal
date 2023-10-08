using RawDeal.CardClass;
using RawDeal.PlayerClass;

namespace RawDeal.DecksBehavior;

public class PlayCard
{

    public GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public void ActionPlayCard()
    {
        int selectedCard = gameStructureInfo.view.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (IsValidIndexOfCard(selectedCard))
        {
            Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
            PlayCardByType(playedCardController);
        }
    }

    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<Tuple<CardController, int>> allCardsAndTheirTypes =
            gameStructureInfo.VisualizeCards.GetPosiblesCardsToPlay(possibleCardsToPlay);

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
        
        gameStructureInfo.LastPlayedCard = playedCardController.Item1;
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
        gameStructureInfo.CardEffects.CauseDamageActionPlayCard(totalDamage);
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
        return totalDamage;
    }

    private void SayThatTheyAreGoingToReceiveDamage(int totalDamage)
    {
        string opposingSuperStarName = gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar();
        gameStructureInfo.view.SayThatSuperstarWillTakeSomeDamage(opposingSuperStarName, totalDamage);
    }

    private void PlayActionCard(CardController playedCardController, int indexType)
    {
        SayThatTheyAreGoingToPlayACard(playedCardController, indexType);
        gameStructureInfo.CardEffects.DiscardCard(playedCardController);
        gameStructureInfo.CardEffects.StealCard();
    }
}