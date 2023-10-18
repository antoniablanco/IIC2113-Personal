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
    
    public void PlayCardAction()
    {
        int selectedCard = gameStructureInfo.View.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        if (HasSelectedAValidCard(selectedCard))
            StartToPlayACardAction(selectedCard);
        else
            gameStructureInfo.BonusManager.AddingOneTurnJockeyingForPosition();
    }
    
    private List<string> GetPossibleCardsToPlayString()
    {   
        List<Tuple<CardController, int>> possibleCardsAndTheirTypes = gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayWithTheyTypeIndex();
        List<string> cardsStrings = gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        return cardsStrings;
    }
    
    public bool HasSelectedAValidCard(int selectedCard)
    {
        return selectedCard != -1;
    }

    private void StartToPlayACardAction(int selectedCard)
    {
        System.Tuple<CardController, int> playedCardController = GetCardPlayed(selectedCard);
        CheckingJockeyForPosition(playedCardController.Item1);
        SetLastPlayedCardInfo(playedCardController);
        SayThatTheyAreGoingToPlayACard(playedCardController.Item1, playedCardController.Item2);
        VerifyIfIsUsedAReversalCard(playedCardController);
    }
    
    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        List<Tuple<CardController, int>> allCardsAndTheirTypes = gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayWithTheyTypeIndex();
        
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