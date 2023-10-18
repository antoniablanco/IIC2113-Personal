using RawDeal.CardClass;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class PlayReversalHandCard
{   
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public PlayReversalHandCard(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    
    public bool IsUserUsingReversalCard()
    {   
        List<CardController> possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();

        if (possibleReversals.Count > 0)
            return AskWhichReversalCardWantsToUse(possibleReversals);

        return false;
    }

    private bool AskWhichReversalCardWantsToUse(List<CardController> possibleReversals)
    {
        int indexReversalCard = UserSelectReversalCard();
        if (gameStructureInfo.PlayCard.HasSelectedAValidCard(indexReversalCard))
        {
            PlayingReversalCard(indexReversalCard, possibleReversals);
            return true;
        }
        return false;
    }
    
    private int UserSelectReversalCard()
    {   
        List<Tuple<CardController, int>> possibleCardsAndTheirTypes = gameStructureInfo.ControllerOpponentPlayer.GetPosiblesCardsForReveralAndTheirReversalTypeIndex();
        List<string> possibleReversalsString = gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        int indexReversalCard = gameStructureInfo.View.AskUserToSelectAReversal(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals) 
    {
        CardController cardController = possibleReversals[indexReversalCard];
        SayTheReversalCardIsPlayed(cardController);
        MoveCardsImplicateInReversal(cardController);
        cardController.ApplyReversalEffect();
    }

    private void SayTheReversalCardIsPlayed(CardController cardController)
    {
        int indexType = cardController.GetIndexForType("Reversal");
        string reversalString = cardController.GetStringPlayedInfo(indexType);
        gameStructureInfo.View.SayThatPlayerReversedTheCard(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
    }

    private void MoveCardsImplicateInReversal(CardController cardController)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),gameStructureInfo.LastPlayedCard);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), cardController);
    }
}