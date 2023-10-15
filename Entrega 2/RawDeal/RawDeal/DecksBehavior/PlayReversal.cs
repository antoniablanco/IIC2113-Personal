using RawDeal.CardClass;
using RawDeal.GameClasses;

namespace RawDeal.DecksBehavior;

public class PlayReversal
{   
    public GameStructureInfo gameStructureInfo = new GameStructureInfo();

    public bool IsUserUsingReversalCard()
    {   
        List<CardController> possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal();
        if (possibleReversals.Count() > 0)
        {
            int indexReversalCard = UserSelectReversalCard(possibleReversals);
            if (indexReversalCard != -1)
            {
                PlayingReversalCard(indexReversalCard, possibleReversals);
                return true;
            }
        }
        return false;
    }

    public int UserSelectReversalCard(List<CardController> possibleReversals)
    {
        List<String> possibleReversalsString =
            gameStructureInfo.CardsVisualizor.CreateStringPlayedCardListForReversalType(possibleReversals);
        int indexReversalCard = gameStructureInfo.view.AskUserToSelectAReversal(
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }
    
    public void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals) 
    {
        if (indexReversalCard != -1)
        {   
            CardController cardController = possibleReversals[indexReversalCard];
            MoveAndPrintCardForReversal(cardController);
            cardController.ReversalEffect();
        }
    }

    public void MoveAndPrintCardForReversal(CardController cardController)
    {
        int indexType = cardController.GetIndexForType("Reversal");
        string reversalString = gameStructureInfo.CardsVisualizor.GetStringPlayedInfo(cardController, indexType);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),gameStructureInfo.LastPlayedCard);
        gameStructureInfo.view.SayThatPlayerReversedTheCard(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), cardController);
    }
}