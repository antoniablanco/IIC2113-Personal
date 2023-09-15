namespace RawDeal.SuperStarClases;
using RawDealView;

public class Kane: SuperStar // Hay que escribir superhabilidad
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidadAutomaticaAlInicioDelTurno(Player jugadorActual, Player jugadorCotrario)
    {   
        Carta cartaVolteada = jugadorCotrario.TraspasoDeCartaSinSeleccionar(jugadorCotrario.cartasArsenal, jugadorCotrario.cartasRingSide);
        string cartaVolteadaString = visualisarCartas.ObtenerStringCartaInfo(cartaVolteada);
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        _view.SayThatSuperstarWillTakeSomeDamage(jugadorCotrario.superestar.Name, 1);
        _view.ShowCardOverturnByTakingDamage(cartaVolteadaString, 1, 1);
    }
}