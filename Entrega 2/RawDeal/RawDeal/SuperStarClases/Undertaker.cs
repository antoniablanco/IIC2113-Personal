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

        DescartandoCartasDeHandAlRingSide(jugadorActual);
        AgregandoCartaHandDelRingSide(jugadorActual);
    }
    
    public void DescartandoCartasDeHandAlRingSide(Player jugadorActual)
    {
        for (int i = 0; i < 2; i++)
        {   
            List<string> handFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasHand);
            int cartaSeleccionada =_view.AskPlayerToSelectACardToDiscard(handFormatoString, Name, Name, 2);
            
            if (cartaSeleccionada != -1)
            {
                Carta cartaDescartada = jugadorActual.cartasHand[cartaSeleccionada];
                jugadorActual.TraspasoDeCartas(cartaDescartada, jugadorActual.cartasHand, jugadorActual.cartasRingSide);
            }
        }
    }
    
    public void AgregandoCartaHandDelRingSide(Player jugadorActual)
    {
        List<string> ringSideFormatoString = visualisarCartas.CrearListaStringCarta(jugadorActual.cartasRingSide);
        int cartaSeleccionada =_view.AskPlayerToSelectCardsToPutInHisHand(Name, 1, ringSideFormatoString);
        
        if (cartaSeleccionada != -1)
        {
            Carta cartaAgregada = jugadorActual.cartasRingSide[cartaSeleccionada];
            jugadorActual.TraspasoDeCartas(cartaAgregada, jugadorActual.cartasRingSide, jugadorActual.cartasHand);
        }
    }

    public override bool PuedeUtilizarSuperAbility(Player jugadorCotrario, Player jugadorActual)
    {
        return (jugadorActual.cartasHand.Count > 1 && !jugadorActual.HabilidadUtilizada);
    }
}