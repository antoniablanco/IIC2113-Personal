using RawDeal.SuperStarClases;
using RawDealView;

namespace RawDeal;

public class Player
{
    private SuperStar _superestar;
    private List<Card> _cardsArsenal = new List<Card>();
    private List<Card> _cardsHand = new List<Card>();
    private List<Card> _cardsRingSide = new List<Card>();
    private List<Card> _cardsRingArea = new List<Card>();
    public bool theHabilityHasBeenUsedThisTurn = false;

    public Player(List<Card> cardsPlayer, SuperStar superstar)
    {
        superestar = superstar;
        cardsArsenal.AddRange(cardsPlayer);
    }

    public SuperStar superestar
    {
        get => _superestar;
        set => _superestar = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Card> cardsArsenal
    {
        get => _cardsArsenal;
        set => _cardsArsenal = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Card> cardsHand
    {
        get => _cardsHand;
        set => _cardsHand = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Card> cardsRingSide
    {
        get => _cardsRingSide;
        set => _cardsRingSide = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Card> cardsRingArea
    {
        get => _cardsRingArea;
        set => _cardsRingArea = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void DrawCard()
    {
        TransferOfUnselectedCard(cardsArsenal, cardsHand);
    }
    
    public string NameOfSuperStar()
    {
        return superestar.Name;
    }

    public void DrawInitialHandCards()
    {
        for (var i = 0; i < superestar.HandSize; i++)
        {
            DrawCard();
        }
    }

    public int FortitudRating()
    {
        return cardsRingArea.Sum(card => int.Parse(card.Damage));
    }

    public List<Card> CardsAvailableToPlay()
    {
        return cardsHand
            .Where(carta => int.Parse(carta.Fortitude) <= FortitudRating() && !carta.IsReversalType())
            .ToList();
    }
    
    public Card? TransferOfUnselectedCard(List<Card> sourceList, List<Card> destinationList, bool moveToStart = false)
    {
        if (sourceList.Count == 0) return null;
    
        int index = moveToStart ? 0 : sourceList.Count - 1;
        Card cardMoved = sourceList[index];
    
        sourceList.RemoveAt(index);
        destinationList.Insert(moveToStart ? 0 : destinationList.Count, cardMoved);
    
        return cardMoved;
    }
    
    public void CardTransferChoosingWhichOneToChange(Card card, List<Card> sourceList, List<Card> destinationList, string moveToStart = "End")
    {   
        if (sourceList.Count > 0)
        {
            int index = (moveToStart == "Start") ? 0 : destinationList.Count;
            destinationList.Insert(index, card);
            sourceList.Remove(card);
        }
    }

    public bool HasCardsInArsenal()
    {
        return (cardsArsenal.Count > 0);
    }
    
    public bool TheirSuperStarCanUseSuperAbility(Player currentPlayer)
    {
        return superestar.CanUseSuperAbility(currentPlayer);
    }

    public void UtilizandoSuperHabilidadElectiva(Player currentPlayer, Player oppositePlayer)
    {
        superestar.UsingElectiveSuperAbility(currentPlayer, oppositePlayer);
    }
    
    public void UtilizandoSuperHabilidadAutomatica(Player currentPlayer, Player oppositePlayer)
    {
        superestar.UsingAutomaticSuperAbilityAtTheStartOfTheTurn( currentPlayer, oppositePlayer);
    }
}