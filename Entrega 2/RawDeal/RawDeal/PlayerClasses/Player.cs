using RawDeal.CardClass;
using RawDeal.SuperStarClasses;

namespace RawDeal.PlayerClasses;

public class Player
{   
    
    public SuperStar? Superestar;
    public List<CardController> CardsArsenal = new List<CardController>();
    public List<CardController> CardsHand = new List<CardController>();
    public List<CardController> CardsRingSide = new List<CardController>();
    public List<CardController> CardsRingArea = new List<CardController>();
    public bool TheHabilityHasBeenUsedThisTurn = false;

    public Player(List<CardController> cardsPlayer, SuperStar? superstar)
    {
        Superestar = superstar;
        CardsArsenal.AddRange(cardsPlayer);
    }
}