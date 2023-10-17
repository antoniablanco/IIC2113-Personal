using System.Data;
using RawDeal.CardClass;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayCard
{

    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public PlayCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void ActionPlayCard()
    {
        int selectedCard = gameStructureInfo.View.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (IsValidIndexOfCard(selectedCard))
            StartPlayCardAction(selectedCard);
        else
            gameStructureInfo.BonusManager.AddingOneTurnJockeyingForPosition();
    }
    
    private List<string> GetPossibleCardsToPlayString()
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<string> cardsStrings = gameStructureInfo.CardsVisualizor.CreateStringPlayedCardListForNotReversalType(possibleCardsToPlay, gameStructureInfo.ControllerCurrentPlayer);
        return cardsStrings;
    }
    
    private bool IsValidIndexOfCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private void StartPlayCardAction(int selectedCard)
    {
        System.Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
        CheckingJockeyForPosition(playedCardController.Item1);
        SetLastPlayedCardInfo(playedCardController);
        SayThatTheyAreGoingToPlayACard(playedCardController.Item1, playedCardController.Item2);
        VerifyIfIsUsedAReversalCard(playedCardController);
    }
    
    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        List<CardController> possibleCardsToPlay = gameStructureInfo.ControllerCurrentPlayer.CardsAvailableToPlay();
        List<Tuple<CardController, int>> allCardsAndTheirTypes = gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayAndTheirTypeIndex(possibleCardsToPlay);

        return allCardsAndTheirTypes[indexSelectedCard];

    }
    
    private void CheckingJockeyForPosition(CardController cardController)
    {
        if (JockeyingForPositionEffectShouldNotBeActive(cardController))
            DesactivateJockeyForPositionEffect();
    }

    private bool JockeyingForPositionEffectShouldNotBeActive(CardController cardController)
    {
        return (!cardController.ContainsSubtype("Grapple") || 
                gameStructureInfo.TurnCounterForJokeyingForPosition <= 0 || 
                (gameStructureInfo.WhoActivateJockeyingForPosition != gameStructureInfo.ControllerCurrentPlayer && 
                 gameStructureInfo.WhoActivateJockeyingForPosition != null));
    }
    
    private void DesactivateJockeyForPositionEffect()
    {
        gameStructureInfo.BonusManager.DesactivateJockeyingForPositionBonusDamage();
        gameStructureInfo.BonusManager.DesactivateJockeyingForPositionBonusFortitud();
    }
    
    private void SetLastPlayedCardInfo(Tuple<CardController, int> playedCardController)
    {
        gameStructureInfo.LastPlayedCard = playedCardController.Item1;
        gameStructureInfo.LastPlayedType = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
    }
    
    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        string playedCardString = playedCardController.GetStringPlayedInfo(indexType);
        string nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar();
        gameStructureInfo.View.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }
    
    private void VerifyIfIsUsedAReversalCard(Tuple<CardController, int> playedCardController)
    {
        PlayReversalHandCard playReversalHandCard = new PlayReversalHandCard(gameStructureInfo);
        if (!playReversalHandCard.IsUserUsingReversalCard())
        {   
            gameStructureInfo.View.SayThatPlayerSuccessfullyPlayedACard();
            PlayCardByType(playedCardController);
        }
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
        PlayManeuverCard playManeuverCard = new PlayManeuverCard(gameStructureInfo);
        playManeuverCard.PlayCard(playedCardController);
    }
    
    private void PlayActionCard(CardController playedCardController)
    {   
        PlayActionCard playActionCard = new PlayActionCard(gameStructureInfo);
        playActionCard.PlayCard(playedCardController);
    }
    
    public int GetDamageProducedCheckingMankindSuperStarAbility(int damage, PlayerController playerController)
    {
        if (gameStructureInfo.Effects.IsTheCardWeAreReversalOfMankindSuperStart(playerController))
            damage -= 1;
        return damage;
    }
}