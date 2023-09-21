namespace RawDeal;

public class PlayerController
{
    private Player player;
    private VisualizeCards VisualizeCards = new VisualizeCards();
    
    public PlayerController(Player player)
    {
        this.player = player;
    }
    
    public void DrawCard()
    {
        TransferOfUnselectedCard(player.cardsArsenal, player.cardsHand, false);
    }
    
    public string NameOfSuperStar()
    {
        return player.superestar.Name;
    }

    public void DrawInitialHandCards()
    {
        for (var i = 0; i < player.superestar.HandSize; i++)
        {
            DrawCard();
        }
    }
    
    public int FortitudRating()
    {
        return player.cardsRingArea.Sum(card => int.Parse(card.Damage));
    }

    public bool IsTheSuperStarMankind()
    {
        return NameOfSuperStar() == "MANKIND";
    }

    public int GetSuperStarValue()
    {
        return player.superestar.SuperstarValue;
    }
    
    public bool AreThereCardsLeftInTheArsenal()
    {
        return player.cardsArsenal.Count() > 0;
    }
    
    public List<Card> CardsAvailableToPlay()
    {
        return player.cardsHand
            .Where(card => int.Parse(card.Fortitude) <= FortitudRating() && !card.IsReversalType())
            .ToList();
    }

    public bool HasCardsInArsenal()
    {
        return (player.cardsArsenal.Count > 0);
    }
    
    public bool TheirSuperStarCanUseSuperAbility(PlayerController currentPlayer)
    {
        return player.superestar.CanUseSuperAbility(currentPlayer);
    }

    public void UsingElectiveSuperAbility(PlayerController currentPlayer, PlayerController oppositePlayer)
    {
        player.superestar.UsingElectiveSuperAbility(currentPlayer, oppositePlayer);
    }
    
    public void UsingAutomaticSuperAbility(PlayerController currentPlayer, PlayerController oppositePlayer)
    {
        player.superestar.UsingAutomaticSuperAbilityAtTheStartOfTheTurn( currentPlayer, oppositePlayer);
    }
    
    public Card? TransferOfUnselectedCard(List<Card> sourceList, List<Card> destinationList, bool moveToStart)
    {
        if (sourceList.Count == 0) return null;
    
        int index = moveToStart ? 0 : sourceList.Count - 1;
        Card cardMoved = sourceList[index];
    
        sourceList.RemoveAt(index);
        destinationList.Insert(moveToStart ? 0 : destinationList.Count, cardMoved);
    
        return cardMoved;
    }
    
    public Card? TranferUnselectedCardFromArsenalToRingArea(bool moveToStart = false)
    {
        return TransferOfUnselectedCard(player.cardsArsenal, player.cardsRingArea, moveToStart);
    }
    
    public Card? TranferUnselectedCardFromArsenalToHand(bool moveToStart = false)
    {
        return TransferOfUnselectedCard(player.cardsArsenal, player.cardsHand, moveToStart);
    }
    
    public Card? TranferUnselectedCardFromArsenalToRingSide(bool moveToStart = false)
    {
        return TransferOfUnselectedCard(player.cardsArsenal, player.cardsRingSide, moveToStart);
    }
    
    public void CardTransferChoosingWhichOneToChange(Card card, List<Card> sourceList, List<Card> destinationList, string moveToStart)
    {   
        if (sourceList.Count > 0)
        {
            int index = (moveToStart == "Start") ? 0 : destinationList.Count;
            destinationList.Insert(index, card);
            sourceList.Remove(card);
        }
    }

    public void TheSuperStarHasUsedHisSuperAbilityThisTurn()
    {
        player.theHabilityHasBeenUsedThisTurn = true;
    }

    public void TransferChoosinCardFromHandToRingArea(Card card, string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(card, player.cardsHand, player.cardsRingArea, moveToStart);
    }
    
    public void TransferChoosinCardFromHandToArsenal(Card card, string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(card, player.cardsHand, player.cardsArsenal, moveToStart);
    }
    
    public void TransferChoosinCardFromHandToRingSide(Card card, string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(card, player.cardsHand, player.cardsRingSide, moveToStart);
    }
    
    public void TransferChoosinCardFromRingSideToHand(Card card, string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(card, player.cardsRingSide, player.cardsHand, moveToStart);
    }
    
    public void TransferChoosinCardFromRingSideToArsenal(Card card, string moveToStart = "End")
    {
        CardTransferChoosingWhichOneToChange(card, player.cardsRingSide, player.cardsArsenal, moveToStart);
    }
    
    public void TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility()
    {
        player.theHabilityHasBeenUsedThisTurn = false;
    }

    public int NumberOfCardsInTheArsenal()
    {
        return player.cardsArsenal.Count();
    }
    
    public int NumberOfCardsInTheHand()
    {
        return player.cardsHand.Count();
    }
    
    public int NumberOfCardsInRingSide()
    {
        return player.cardsRingSide.Count();
    }
    
    public Card GetSpecificCardFromHand(int index)
    {
        return player.cardsHand[index];
    }
    
    public Card GetSpecificCardFromRingSide(int index)
    {
        return player.cardsRingSide[index];
    }
    
    public List<String> StringCardsHand()
    {
        List<String> stringCardSet = VisualizeCards.CreateStringCardList(player.cardsHand);
        return stringCardSet;
    }
    
    public List<String> StringCardsRingArea()
    {
        List<String> stringCardSet = VisualizeCards.CreateStringCardList(player.cardsRingArea);
        return stringCardSet;
    }
    
    public List<String> StringCardsRingSide()
    {
        List<String> stringCardSet = VisualizeCards.CreateStringCardList(player.cardsRingSide);
        return stringCardSet;
    }

    public bool HasTheSuperAbilityBeenUsedThisTurn()
    {
        return player.theHabilityHasBeenUsedThisTurn;
    }
    
}