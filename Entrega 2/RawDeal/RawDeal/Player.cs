using RawDeal.SuperStarClases;
using RawDealView;

namespace RawDeal;

public class Player
{
    private SuperStar _superestar;
    private List<Carta> _cartasArsenal = new List<Carta>();
    private List<Carta> _cartasHand = new List<Carta>();
    private List<Carta> _cartasRingSide = new List<Carta>();
    private List<Carta> _cartasRingArea = new List<Carta>();
    public bool HabilidadUtilizada = false;

    public Player(List<Carta> cartasPlayer, SuperStar superstar)
    {
        superestar = superstar;
        cartasArsenal.AddRange(cartasPlayer);
    }

    public SuperStar superestar
    {
        get => _superestar;
        set => _superestar = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Carta> cartasArsenal
    {
        get => _cartasArsenal;
        set => _cartasArsenal = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Carta> cartasHand
    {
        get => _cartasHand;
        set => _cartasHand = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Carta> cartasRingSide
    {
        get => _cartasRingSide;
        set => _cartasRingSide = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Carta> cartasRingArea
    {
        get => _cartasRingArea;
        set => _cartasRingArea = value ?? throw new ArgumentNullException(nameof(value));
    }

    public void RobarCarta()
    {
        TraspasoDeCartaSinSeleccionar(cartasArsenal, cartasHand);
        
    }

    public void RobarCartasHandInicial()
    {
        for (int i = 0; i < superestar.HandSize; i++)
        {
            RobarCarta();
        }
    }

    public int FortitudRating()
    {
        int fortitudRating = 0;
        foreach (Carta carta in cartasRingArea)
        {
            fortitudRating += int.Parse(carta.Damage);
        }

        return fortitudRating;
    }

    public List<Carta> CartasPosiblesDeJugar()
    {
        return cartasHand
            .Where(carta => int.Parse(carta.Fortitude) <= FortitudRating())
            .ToList();
    }
    
    public Carta? TraspasoDeCartaSinSeleccionar(List<Carta> listaOrigen, List<Carta> listaDestino, string posicion = "Final")
    {
        int lastIndex = listaOrigen.Count - 1;
        if (listaOrigen.Count > 0)
        {
            Carta cartaMovida = listaOrigen[lastIndex];
            if (posicion == "Inicio")
            {
                listaDestino.Insert(0, cartaMovida);
            }
            else
            {
                listaDestino.Add(cartaMovida);
            }
            listaOrigen.RemoveAt(lastIndex);
            return cartaMovida;
        }

        return null;
    }

    public void TraspasoDeCartasEscogiendoCualSeQuiereCambiar(Carta carta, List<Carta> listaOrigen, List<Carta> listaDestino, string posicion = "Final")
    {   
        if (listaOrigen.Count > 0)
        {
            if (posicion == "Inicio")
            {
                listaDestino.Insert(0, carta);
                listaOrigen.Remove(carta);
            }
            else
            {
                listaDestino.Add(carta);
                listaOrigen.Remove(carta);
            }
        }
    }

    public bool TieneCartasEnArsenal()
    {
        return (cartasArsenal.Count > 0);
    }
    
    public bool SuSuperStarPuedeUtilizarSuperAbility(Player jugadorActual, Player jugadorCotrario)
    {
        return superestar.PuedeUtilizarSuperAbility(jugadorCotrario, jugadorActual);
    }

    public void UtilizandoSuperHabilidadDelSuperStar(Player jugadorActual, Player jugadorCotrario)
    {
        superestar.UtilizandoSuperHabilidad(jugadorCotrario, jugadorActual);
    }
}