using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayCard
{
    private readonly GameStructureInfo gameStructureInfo = new();

    public PlayCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void PlayCardAction()
    {
        var selectedCard = gameStructureInfo.View.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (HasSelectedAValidCard(selectedCard))
            StartToPlayACardAction(selectedCard);
        else
            gameStructureInfo.BonusManager.AddingOneTurnJockeyingForPosition();
    }

    private List<string> GetPossibleCardsToPlayString()
    {
        var possibleCardsAndTheirTypes =
            gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayWithTheirTypeIndex();
        var cardsStrings = gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        return cardsStrings;
    }

    public bool HasSelectedAValidCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private void StartToPlayACardAction(int selectedCard)
    {
        var playedCardController = GetCardPlayed(selectedCard);
        CheckingJockeyForPosition(playedCardController.Item1);
        SetLastPlayedCardInfo(playedCardController);
        SayThatTheyAreGoingToPlayACard(playedCardController.Item1, playedCardController.Item2);
        VerifyIfIsUsedAReversalCard(playedCardController);
    }

    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        var allCardsAndTheirTypes =
            gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayWithTheirTypeIndex();
        return allCardsAndTheirTypes[indexSelectedCard];
    }

    private void CheckingJockeyForPosition(CardController cardController)
    {
        if (JockeyingForPositionEffectShouldNotBeActive(cardController))
            DeactivateJockeyForPositionEffect();
    }

    private bool JockeyingForPositionEffectShouldNotBeActive(CardController cardController)
    {
        return !cardController.ContainsSubtype("Grapple") ||
               gameStructureInfo.BonusManager.GetTurnCounterForJokeyingForPosition() <= 0 ||
               (gameStructureInfo.WhoActivateJockeyingForPosition != gameStructureInfo.ControllerCurrentPlayer &&
                gameStructureInfo.WhoActivateJockeyingForPosition != null);
    }

    private void DeactivateJockeyForPositionEffect()
    {
        gameStructureInfo.BonusManager.DeactivateBonus("JockeyingFortitud");
        gameStructureInfo.BonusManager.DeactivateBonus("JockeyingDamage");
    }

    private void SetLastPlayedCardInfo(Tuple<CardController, int> playedCardController)
    {
        gameStructureInfo.LastPlayedCard = playedCardController.Item1;
        gameStructureInfo.LastPlayedType = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
    }

    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        var playedCardString = playedCardController.GetStringPlayedInfo(indexType);
        var nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private void VerifyIfIsUsedAReversalCard(Tuple<CardController, int> playedCardController)
    {
        var playReversalHandCard = new PlayReversalHandCard(gameStructureInfo);
        if (!playReversalHandCard.IsUserUsingReversalCard())
        {
            gameStructureInfo.View.SayThatPlayerSuccessfullyPlayedACard();
            PlayCardByType(playedCardController);
        }
    }

    private void PlayCardByType(Tuple<CardController, int> playedCardController)
    {
        var typeCard = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
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
        var playManeuverCard = new PlayManeuverCard(gameStructureInfo);
        playManeuverCard.PlayCard(playedCardController);
    }

    private void PlayActionCard(CardController playedCardController)
    {
        var playActionCard = new PlayActionCard(gameStructureInfo);
        playActionCard.PlayCard(playedCardController);
    }

    public int ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int damage, PlayerController playerController)
    {
        if (gameStructureInfo.EffectsUtils.IsTheSuperStarMankind(playerController))
            damage -= 1;
        return damage;
    }
}