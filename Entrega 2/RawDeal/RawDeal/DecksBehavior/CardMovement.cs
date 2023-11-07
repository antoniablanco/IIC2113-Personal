using RawDeal.CardClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class CardMovement
{
    public CardController? TransferOfUnselectedCard(List<CardController> sourceList,
        List<CardController> destinationList, bool moveToStart)
    {
        if (sourceList.Count > 0)
        {
            var index = moveToStart ? 0 : sourceList.Count - 1;
            var cardControllerMoved = sourceList[index];

            sourceList.RemoveAt(index);
            destinationList.Insert(moveToStart ? 0 : destinationList.Count, cardControllerMoved);

            return cardControllerMoved;
        }

        throw new InvalidOperationException("No se puede transferir una tarjeta porque la lista de origen está vacía.");
    }

    private void CardTransferChoosingWhichOneToChange(CardController cardController, List<CardController> sourceList,
        List<CardController> destinationList, string moveToStart)
    {
        if (sourceList.Count > 0)
        {
            var index = moveToStart == "Start" ? 0 : destinationList.Count;
            destinationList.Insert(index, cardController);
            sourceList.Remove(cardController);
        }
    }

    public CardController? TranferUnselectedCardFromArsenalToHand(Player player, bool moveToStart = false)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand, moveToStart);
    }

    public CardController? TranferUnselectedCardFromArsenalToRingSide(Player player, bool moveToStart = false)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsRingSide, moveToStart);
    }

    public void TransferChoosinCardFromHandToRingArea(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsRingArea, moveToStart);
    }

    public void TransferChoosinCardFromHandToArsenal(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsArsenal, moveToStart);
    }

    public void TransferChoosinCardFromHandToRingSide(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsRingSide, moveToStart);
    }
    
    public void TransferChoosinCardFromRingAreaToRingSide(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsRingArea, player.CardsRingSide, moveToStart);
    }

    public void TransferChoosinCardFromRingSideToHand(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsRingSide, player.CardsHand, moveToStart);
    }

    public void TransferChoosinCardFromRingSideToArsenal(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsRingSide, player.CardsArsenal, moveToStart);
    }
    
    public void TransferChoosinCardArsenalToHand(Player player, CardController cardController,
        string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsArsenal, player.CardsHand, moveToStart);
    }
}