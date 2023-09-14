namespace RawDeal.SuperStarClases;
using RawDealView;

public class TheRock: SuperStar
{
    public TheRock(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public virtual void UtilizandoSuperHabilidadAutomaticaAlInicioDelTurno(Player jugadorActual, Player jugadorCotrario)
    {
        if (_view.DoesPlayerWantToUseHisAbility(Name)) ;
        {
            _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
            List<string> ringAreaFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasRingSide);
            _view.AskPlayerToSelectCardsToRecover(Name, 1, ringAreaFormatoString);
        }
    }
}