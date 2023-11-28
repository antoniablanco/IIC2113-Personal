using RawDeal.CardClasses;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class CardMovement
{   
    private readonly GameStructureInfo gameStructureInfo;

    public CardMovement(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    private void CardTransferToStartChoosingWhichOneToChange(CardController cardController, List<CardController> sourceList,
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

    private void CardTransferChoosingWhichOneToChange(CardController cardController, List<CardController> sourceList,
        List<CardController> destinationList)
    {
        if (sourceList.Count > 0)
        {
            int index = destinationList.Count;
            destinationList.Insert(index, cardController);
            sourceList.Remove(cardController);
        }
    }

    public CardController? TranferUnselectedCardFromArsenalToHand(Player player)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand);
    }

    public CardController? TranferUnselectedCardFromArsenalToRingSide(Player player)
    {
        return TransferOfUnselectedCard(player.CardsArsenal, player.CardsRingSide);
    }

    public void TransferChoosinCardFromHandToRingArea(Player player, CardController cardController)
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsRingArea);
    }
    
    public void TransferChoosinCardFromHandToStartOfArsenal(Player player, CardController cardController)
    {   
        CardTransferToStartChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsArsenal);
    }

    public void TransferChoosinCardFromHandToRingSide(Player player, CardController cardController)
    {   
        CardTransferChoosingWhichOneToChange(cardController, player.CardsHand, player.CardsRingSide);
    }
    
    public void TransferChoosinCardFromRingAreaToRingSide(Player player, CardController cardController)
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsRingArea, player.CardsRingSide);
    }

    public void TransferChoosinCardFromRingSideToHand(Player player, CardController cardController)
    {   
        CardTransferChoosingWhichOneToChange(cardController, player.CardsRingSide, player.CardsHand);
    }
    
    public void TransferChoosinCardFromRingSideToStartOfArsenal(Player player, CardController cardController)
    {
        CardTransferToStartChoosingWhichOneToChange(cardController, player.CardsRingSide, player.CardsArsenal);
    }
    
    public void TransferChoosinCardFromArsenalToHand(Player player, CardController cardController)
    {
        CardTransferChoosingWhichOneToChange(cardController, player.CardsArsenal, player.CardsHand);
    }
}