using RawDeal.SuperStarClases;
using RawDealView;

namespace RawDeal;

public class Player
{
    private SuperStar? _superestar;
    private List<Card> _cardsArsenal = new List<Card>();
    private List<Card> _cardsHand = new List<Card>();
    private List<Card> _cardsRingSide = new List<Card>();
    private List<Card> _cardsRingArea = new List<Card>();
    public bool theHabilityHasBeenUsedThisTurn = false;

    public Player(List<Card> cardsPlayer, SuperStar? superstar)
    {
        superestar = superstar;
        cardsArsenal.AddRange(cardsPlayer);
    }

    public SuperStar? superestar
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
}