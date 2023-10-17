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
        List<Tuple<CardController, int>> possibleCardsAndTheirTypes = gameStructureInfo.ControllerCurrentPlayer.GetPosiblesCardsForReveralAndTheirReversalTypeIndex(possibleReversals);
        List<string> possibleReversalsString = gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        int indexReversalCard = gameStructureInfo.View.AskUserToSelectAReversal(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals) 
    {
        if (indexReversalCard != -1)
        {   
            CardController cardController = possibleReversals[indexReversalCard];
            MoveAndPrintCardForReversal(cardController);
            cardController.ApplyReversalEffect();
        }
    }

    private void MoveAndPrintCardForReversal(CardController cardController)
    {
        int indexType = cardController.GetIndexForType("Reversal");
        string reversalString = cardController.GetStringPlayedInfo(indexType);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),gameStructureInfo.LastPlayedCard);
        gameStructureInfo.View.SayThatPlayerReversedTheCard(gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(), cardController);
    }
}