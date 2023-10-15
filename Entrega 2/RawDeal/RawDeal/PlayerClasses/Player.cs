using RawDeal.CardClass;
using RawDeal.SuperStarClasses;

namespace RawDeal.PlayerClasses;

public class Player
{
    private SuperStar? _superestar;
    private List<CardController> _cardsArsenal = new List<CardController>();
    private List<CardController> _cardsHand = new List<CardController>();
    private List<CardController> _cardsRingSide = new List<CardController>();
    private List<CardController> _cardsRingArea = new List<CardController>();
    public bool theHabilityHasBeenUsedThisTurn = false;

    public Player(List<CardController> cardsPlayer, SuperStar? superstar)
    {
        superestar = superstar;
        cardsArsenal.AddRange(cardsPlayer);
    }

    public SuperStar? superestar
    {
        get => _superestar;
        set => _superestar = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<CardController> cardsArsenal
    {
        get => _cardsArsenal;
        set => _cardsArsenal = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<CardController> cardsHand
    {
        get => _cardsHand;
        set => _cardsHand = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<CardController> cardsRingSide
    {
        get => _cardsRingSide;
        set => _cardsRingSide = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<CardController> cardsRingArea
    {
        get => _cardsRingArea;
        set => _cardsRingArea = value ?? throw new ArgumentNullException(nameof(value));
    }
}