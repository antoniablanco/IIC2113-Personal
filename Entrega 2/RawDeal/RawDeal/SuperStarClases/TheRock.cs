namespace RawDeal.SuperStarClases;
using RawDealView;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidadAutomaticaAlInicioDelTurno(Player jugadorActual, Player jugadorCotrario)
    {  
        if (PuedeUtilizarSuperAbility(jugadorActual))
        {   
            jugadorActual.HabilidadUtilizada = true;
            if (_view.DoesPlayerWantToUseHisAbility(Name)) 
            {
                _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
                List<string> ringAreaFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasRingSide);
                int intCartaSeleccionada = _view.AskPlayerToSelectCardsToRecover(Name, 1, ringAreaFormatoString);
                Carta cartaDescartada = jugadorActual.cartasRingSide[intCartaSeleccionada];
                jugadorActual.TraspasoDeCartasEscogiendoCualSeQuiereCambiar(cartaDescartada, jugadorActual.cartasRingSide, jugadorActual.cartasArsenal);
            }
        }
    }
    
    public override bool PuedeUtilizarSuperAbility(Player jugadorActual)
    {
        return jugadorActual.cartasRingSide.Count > 0 && !jugadorActual.HabilidadUtilizada;
    }
}