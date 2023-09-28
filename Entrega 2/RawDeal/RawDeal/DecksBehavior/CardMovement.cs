namespace RawDeal.DecksBehavior;

public class CardMovement
{   
    private Player player;
    
    public CardMovement(Player player)
    {
        this.player = player;
    }
    
    public CardController? TransferOfUnselectedCard(List<CardController> sourceList, List<CardController> destinationList, bool moveToStart)
    {
        if (sourceList.Count == 0) return null;
    
        int index = moveToStart ? 0 : sourceList.Count - 1;
        CardController cardControllerMoved = sourceList[index];
    
        sourceList.RemoveAt(index);
        destinationList.Insert(moveToStart ? 0 : destinationList.Count, cardControllerMoved);
    
        return cardControllerMoved;
    }
    
    public void CardTransferChoosingWhichOneToChange(CardController cardController, List<CardController> sourceList, List<CardController> destinationList, string moveToStart)
    {   
        if (sourceList.Count > 0)
        {
            int index = (moveToStart == "Start") ? 0 : destinationList.Count;
            destinationList.Insert(index, cardController);
            sourceList.Remove(cardController);
        }
    }
    
    
}