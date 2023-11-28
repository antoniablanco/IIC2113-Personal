using RawDeal.CardClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class CardPlay
{
    private readonly GameStructureInfo gameStructureInfo = new();

    public CardPlay(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }

    public void PlayCardAction()
    {   
        gameStructureInfo.GetSetGameVariables.OneRoundMoreInTurn();
        var selectedCard = gameStructureInfo.View.AskUserToSelectAPlay(GetPossibleCardsToPlayString());
        
        if (HasSelectedAValidCard(selectedCard))
            StartToPlayACardAction(selectedCard);
        else
        { 
            gameStructureInfo.BonusManager.AddOneTurnFromBonusCounter();
            gameStructureInfo.GetSetGameVariables.OneRoundLessInTurn();
        }
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
        CheckIfBonusesShouldBeActive(playedCardController.Item1, playedCardController.Item1.GetCardTypes()[playedCardController.Item2]);
        SayThatTheyAreGoingToPlayACard(playedCardController.Item1, playedCardController.Item2);
        SetLastPlayedCardInfo(playedCardController);
        VerifyIfIsUsedAReversalCard(playedCardController);
    }

    private Tuple<CardController, int> GetCardPlayed(int indexSelectedCard)
    {
        var allCardsAndTheirTypes =
            gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsToPlayWithTheirTypeIndex();
        return allCardsAndTheirTypes[indexSelectedCard];
    }

    private void CheckIfBonusesShouldBeActive(CardController cardController, string type)
    {
        gameStructureInfo.BonusManager.CheckIfBonusesShouldBeActive(gameStructureInfo.ControllerCurrentPlayer,
            cardController, type );
        gameStructureInfo.BonusManager.CheckIfReversalBonusShouldBeActive(gameStructureInfo.ControllerCurrentPlayer);
    }
    
    private void SetLastPlayedCardInfo(Tuple<CardController, int> playedCardController)
    {
        if (gameStructureInfo.CardBeingPlayed != null)
        {
            gameStructureInfo.LastCardBeingPlayedTitle = gameStructureInfo.CardBeingPlayed.GetCardTitle();
            gameStructureInfo.LastCardBeingPlayedType = gameStructureInfo.CardBeingPlayedType;
        }
        gameStructureInfo.CardBeingPlayed = playedCardController.Item1;
        gameStructureInfo.CardBeingPlayedType = playedCardController.Item1.GetCardTypes()[playedCardController.Item2];
    }

    private void SayThatTheyAreGoingToPlayACard(CardController playedCardController, int indexType)
    {
        var playedCardString = playedCardController.GetStringPlayedInfo(indexType);
        var nameSuperStar = gameStructureInfo.ControllerCurrentPlayer.GetNameOfSuperStar();
        gameStructureInfo.View.SayThatPlayerIsTryingToPlayThisCard(nameSuperStar, playedCardString);
    }

    private void VerifyIfIsUsedAReversalCard(Tuple<CardController, int> playedCardController)
    {
        var playReversalHandCard = new ReversalHandCardPlay(gameStructureInfo, GetDamageProduced(playedCardController.Item1));
        try
        {
            playReversalHandCard.IsUserUsingReversalCard();
            gameStructureInfo.View.SayThatPlayerSuccessfullyPlayedACard();
            PlayCardByType(playedCardController);
            
        }
        catch (UserPlayReversalCardException e) { }
    }
    
    private int GetDamageProduced(CardController playedCardController)
    {   
        var damage = playedCardController.GetDamageProducedByTheCard() +
                     gameStructureInfo.BonusManager.GetNexPlayCardDamageBonus() +
                     gameStructureInfo.BonusManager.GetTurnDamageBonus(playedCardController)+ 
                     gameStructureInfo.BonusManager.EternalDamage(playedCardController, gameStructureInfo.ControllerCurrentPlayer);
        var totalDamage =
            gameStructureInfo.CardPlay.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(damage,
                gameStructureInfo.ControllerOpponentPlayer);
        return totalDamage;
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
        var playManeuverCard = new ManeuverCardPlay(gameStructureInfo);
        playManeuverCard.PlayCard(playedCardController);
    }

    private void PlayActionCard(CardController playedCardController)
    {
        var playActionCard = new ActionCardPlay(gameStructureInfo);
        playActionCard.PlayCard(playedCardController);
    }

    public int ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(int damage, PlayerController playerController)
    {
        if (gameStructureInfo.EffectsUtils.IsTheSuperStarMankind(playerController))
            damage -= 1;
        return damage;
    }
}