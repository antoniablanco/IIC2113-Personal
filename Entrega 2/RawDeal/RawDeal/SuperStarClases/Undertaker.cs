namespace RawDeal.SuperStarClases;
using RawDealView;

public class Undertaker: SuperStar
{
    public Undertaker(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidad(Player jugadorActual, Player jugadorCotrario)
    {   
        jugadorActual.HabilidadUtilizada = true;
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);

        DescartandoCartasDeHand(jugadorActual);
        AgregandoCartaHand(jugadorActual);
    }
    
    public void DescartandoCartasDeHand(Player jugadorActual)
    {
        for (int i = 0; i < 2; i++)
        {   
            List<string> handFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasHand);
            int cartaSeleccionada =_view.AskPlayerToSelectACardToDiscard(handFormatoString, Name, Name, 2);
            
            if (cartaSeleccionada != -1)
            {
                Carta cartaDescartada = jugadorActual.cartasHand[cartaSeleccionada];
                jugadorActual.CartaPasaDeHandAlRingSide(cartaDescartada);
            }
        }
    }
    
    public void AgregandoCartaHand(Player jugadorActual)
    {
        List<string> ringSideFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasRingSide);
        int cartaSeleccionada =_view.AskPlayerToSelectCardsToPutInHisHand(Name, 1, ringSideFormatoString);
        
        if (cartaSeleccionada != -1)
        {
            Carta cartaAgregada = jugadorActual.cartasRingSide[cartaSeleccionada];
            jugadorActual.CartaPasaDelRingSideAHand(cartaAgregada);
        }
    }

    public override bool PuedeUtilizarSuperAbility(Player jugadorCotrario, Player jugadorActual)
    {
        return (jugadorActual.cartasHand.Count > 1 && !jugadorActual.HabilidadUtilizada);
    }
}