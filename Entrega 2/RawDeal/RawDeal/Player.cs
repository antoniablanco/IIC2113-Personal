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

    public Player(List<Card> cartasPlayer, SuperStar superstar)
    {
        superestar = superstar;
        cardsArsenal.AddRange(cartasPlayer);
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

    public void RobarCartasHandInicial()
    {
        for (int i = 0; i < superestar.HandSize; i++)
        {
            DrawCard();
        }
    }

    public int FortitudRating()
    {
        int fortitudRating = 0;
        foreach (Card carta in cardsRingArea)
        {
            fortitudRating += int.Parse(carta.Damage);
        }

        return fortitudRating;
    }

    public List<Card> CartasPosiblesDeJugar()
    {
        return cardsHand
            .Where(carta => int.Parse(carta.Fortitude) <= FortitudRating() && !carta.EsTipoReversal())
            .ToList();
    }
    
    public Card? TransferOfUnselectedCard(List<Card> listaOrigen, List<Card> listaDestino, string posicion = "End")
    {
        int lastIndex = listaOrigen.Count - 1;
        if (listaOrigen.Count > 0)
        {
            Card cardMovida = listaOrigen[lastIndex];
            if (posicion == "Start")
            {
                listaDestino.Insert(0, cardMovida);
            }
            else
            {
                listaDestino.Add(cardMovida);
            }
            listaOrigen.RemoveAt(lastIndex);
            return cardMovida;
        }

        return null;
    }

    public void CardTransferChoosingWhichOneToChange(Card card, List<Card> listaOrigen, List<Card> listaDestino, string posicion = "End")
    {   
        if (listaOrigen.Count > 0)
        {
            if (posicion == "Start")
            {
                listaDestino.Insert(0, card);
                listaOrigen.Remove(card);
            }
            else
            {
                listaDestino.Add(card);
                listaOrigen.Remove(card);
            }
        }
    }

    public bool TieneCartasEnArsenal()
    {
        return (cardsArsenal.Count > 0);
    }
    
    public bool SuSuperStarPuedeUtilizarSuperAbility(Player jugadorActual, Player jugadorCotrario)
    {
        return superestar.CanUseSuperAbility(jugadorActual);
    }

    public void UtilizandoSuperHabilidadElectiva(Player jugadorActual, Player jugadorCotrario)
    {
        superestar.UsingElectiveSuperAbility(jugadorActual, jugadorCotrario);
    }
    
    public void UtilizandoSuperHabilidadAutomatica(Player jugadorActual, Player jugadorCotrario)
    {
        superestar.UsingAutomaticSuperAbilityAtTheStartOfTheTurn( jugadorActual, jugadorCotrario);
    }
}