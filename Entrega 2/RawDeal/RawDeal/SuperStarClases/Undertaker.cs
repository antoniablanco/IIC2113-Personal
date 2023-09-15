namespace RawDeal.SuperStarClases;
using RawDealView;

public class Undertaker: SuperStar
{
    public Undertaker(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidadElectiva(Player jugadorActual, Player jugadorCotrario)
    {
        jugadorActual.HabilidadUtilizada = true;
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
        for (int i = 0; i < 2; i++)
        {   
            DescartandoCartasDeHandAlRingSide(jugadorActual, 2-i);
        }
        AgregandoCartaDelRingSideaHand(jugadorActual);
    }
    

    public override bool PuedeUtilizarSuperAbility(Player jugadorActual)
    {
        return (jugadorActual.cartasHand.Count > 1 && !jugadorActual.HabilidadUtilizada);
    }
}