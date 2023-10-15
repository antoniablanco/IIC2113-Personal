using RawDeal.CardClass;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class PlayReversalHand
{   
    private GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public PlayReversalHand(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    public bool IsUserUsingReversalCard()
    {   
        List<CardController> possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();
        if (possibleReversals.Count() <= 0 ) return false;
        
        int indexReversalCard = UserSelectReversalCard(possibleReversals);
        if (indexReversalCard != -1)
        {
            PlayingReversalCard(indexReversalCard, possibleReversals);
            return true;
        }
        return false;
    }

    private int UserSelectReversalCard(List<CardController> possibleReversals)
    {
        List<String> possibleReversalsString =
            gameStructureInfo.CardsVisualizor.CreateStringPlayedCardListForReversalType(possibleReversals);
        int indexReversalCard = gameStructureInfo.View.AskUserToSelectAReversal(
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals) 
    {
        if (indexReversalCard != -1)
        {   
            CardController cardController = possibleReversals[indexReversalCard];
            MoveAndPrintCardForReversal(cardController);
            cardController.ReversalEffect();
        }
    }

    private void MoveAndPrintCardForReversal(CardController cardController)
    {
        int indexType = cardController.GetIndexForType("Reversal");
        string reversalString = gameStructureInfo.CardsVisualizor.GetStringPlayedInfo(cardController, indexType);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),gameStructureInfo.LastPlayedCard);
        gameStructureInfo.View.SayThatPlayerReversedTheCard(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), cardController);
    }
}