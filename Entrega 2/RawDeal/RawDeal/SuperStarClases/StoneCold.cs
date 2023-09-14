namespace RawDeal.SuperStarClases;
using RawDealView;

public class StoneCold: SuperStar
{
    public StoneCold(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidadElectiva(Player jugadorActual, Player jugadorCotrario)
    {
        jugadorActual.HabilidadUtilizada = true;
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        
        AgregandoCartaDelArsenalAHand(jugadorActual);
        DescartandoCartasDeHandAlArsenal(jugadorActual);
    }
    
    public void AgregandoCartaDelArsenalAHand(Player jugadorActual)
    {
        _view.SayThatPlayerDrawCards(Name, 1);

        jugadorActual.TraspasoDeCartaSinSeleccionar(jugadorActual.cartasArsenal, jugadorActual.cartasHand);
    }
    
    public void DescartandoCartasDeHandAlArsenal(Player jugadorActual)
    {
        
        List<string> cartasHandFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasHand);
        int cartaSeleccionada = _view.AskPlayerToReturnOneCardFromHisHandToHisArsenal(Name, cartasHandFormatoString);
        
        if (cartaSeleccionada != -1)
        {
            Carta cartaDescartada = jugadorActual.cartasHand[cartaSeleccionada];
            jugadorActual.TraspasoDeCartasEscogiendoCualSeQuiereCambiar(cartaDescartada,  jugadorActual.cartasHand,jugadorActual.cartasArsenal, "Inicio");
        }
        
    }
    
    public override bool PuedeUtilizarSuperAbility(Player jugadorCotrario, Player jugadorActual)
    {
        return (jugadorActual.cartasArsenal.Count > 0 && !jugadorActual.HabilidadUtilizada);
    }
}