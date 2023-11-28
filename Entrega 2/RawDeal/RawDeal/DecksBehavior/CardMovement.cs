using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class CardMovement
{   
    private void TransferSelectedCardToStart(CardController cardController, List<CardController> sourceList,
        List<CardController> destinationList)
    {
        if (sourceList.Count > 0)
        {
            int index = 0;
            destinationList.Insert(index, cardController);
            sourceList.Remove(cardController);
        }
    }
    
    public CardController TransferOfUnselectedCard(List<CardController> sourceList,
        List<CardController> destinationList)
    {
        if (sourceList.Count > 0)
        {
            var index = sourceList.Count - 1;
            var cardControllerMoved = sourceList[index];

            sourceList.RemoveAt(index);
            destinationList.Insert(destinationList.Count, cardControllerMoved);

            return cardControllerMoved;
        }

        throw new InvalidOperationException("No se puede transferir una tarjeta porque la lista de origen está vacía.");
    }

    private void TransferSelectedCard(CardController cardController, List<CardController> sourceList,
        List<CardController> destinationList)
    {
        if (sourceList.Count > 0)
        {
            int index = destinationList.Count;
            destinationList.Insert(index, cardController);
            sourceList.Remove(cardController);
        }
    }

    public CardController? TransferUnselectedCardFromArsenalToHand(Player player)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand);
    }

    public CardController? TransferUnselectedCardFromArsenalToRingSide(Player player)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsRingSide);
    }

    public void TransferSelectedCardFromHandToRingArea(Player player, CardController cardController)
    {
        TransferSelectedCard(cardController, player.CardsHand, player.CardsRingArea);
    }
    
    public void TransferSelectedCardFromHandToStartOfArsenal(Player player, CardController cardController)
    {   
        TransferSelectedCardToStart(cardController, player.CardsHand, player.CardsArsenal);
    }

    public void TransferSelectedCardFromHandToRingSide(Player player, CardController cardController)
    {   
        TransferSelectedCard(cardController, player.CardsHand, player.CardsRingSide);
    }
    
    public void TransferSelectedCardFromRingAreaToRingSide(Player player, CardController cardController)
    {
        TransferSelectedCard(cardController, player.CardsRingArea, player.CardsRingSide);
    }

    public void TransferSelectedCardFromRingSideToHand(Player player, CardController cardController)
    {   
        TransferSelectedCard(cardController, player.CardsRingSide, player.CardsHand);
    }
    
    public void TransferSelectedCardFromRingSideToStartOfArsenal(Player player, CardController cardController)
    {
        TransferSelectedCardToStart(cardController, player.CardsRingSide, player.CardsArsenal);
    }
    
    public void TransferSelectedCardFromArsenalToHand(Player player, CardController cardController)
    {
        TransferSelectedCard(cardController, player.CardsArsenal, player.CardsHand);
    }
}