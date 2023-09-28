using RawDeal.DecksBehavior;

namespace RawDeal;

public class PlayerController
{
    private Player player;
    private VisualizeCards VisualizeCards = new VisualizeCards();
    private CardMovement CardMovement;
    
    public PlayerController(Player player)
    {
        this.player = player;
        CardMovement = new CardMovement(player);
    }
    
    public void DrawCard()
    {   
        CardMovement.TransferOfUnselectedCard(player.cardsArsenal, player.cardsHand, false);
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
        return player.cardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
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
    
    public List<CardController> CardsAvailableToPlay()
    {
        return player.cardsHand
            .Where(card => card.GetCardFortitude() <= FortitudRating() && !card.IsReversalType())
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
    
    public void TheSuperStarHasUsedHisSuperAbilityThisTurn()
    {
        player.theHabilityHasBeenUsedThisTurn = true;
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
    
    public CardController GetSpecificCardFromHand(int index)
    {
        return player.cardsHand[index];
    }
    
    public CardController GetSpecificCardFromRingSide(int index)
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
    
    
    
    public CardController? TranferUnselectedCardFromArsenalToRingArea(bool moveToStart = false)
    {
        return CardMovement.TransferOfUnselectedCard(player.cardsArsenal, player.cardsRingArea, moveToStart);
    }
    
    public CardController? TranferUnselectedCardFromArsenalToHand(bool moveToStart = false)
    {
        return CardMovement.TransferOfUnselectedCard(player.cardsArsenal, player.cardsHand, moveToStart);
    }
    
    public CardController? TranferUnselectedCardFromArsenalToRingSide(bool moveToStart = false)
    {
        return CardMovement.TransferOfUnselectedCard(player.cardsArsenal, player.cardsRingSide, moveToStart);
    }
    
    
    public void TransferChoosinCardFromHandToRingArea(CardController cardController, string moveToStart = "End")
    {
        CardMovement.CardTransferChoosingWhichOneToChange(cardController, player.cardsHand, player.cardsRingArea, moveToStart);
    }
    
    public void TransferChoosinCardFromHandToArsenal(CardController cardController, string moveToStart = "End")
    {
        CardMovement.CardTransferChoosingWhichOneToChange(cardController, player.cardsHand, player.cardsArsenal, moveToStart);
    }
    
    public void TransferChoosinCardFromHandToRingSide(CardController cardController, string moveToStart = "End")
    {
        CardMovement.CardTransferChoosingWhichOneToChange(cardController, player.cardsHand, player.cardsRingSide, moveToStart);
    }
    
    public void TransferChoosinCardFromRingSideToHand(CardController cardController, string moveToStart = "End")
    {
        CardMovement.CardTransferChoosingWhichOneToChange(cardController, player.cardsRingSide, player.cardsHand, moveToStart);
    }
    
    public void TransferChoosinCardFromRingSideToArsenal(CardController cardController, string moveToStart = "End")
    {
        CardMovement.CardTransferChoosingWhichOneToChange(cardController, player.cardsRingSide, player.cardsArsenal, moveToStart);
    }
    
}