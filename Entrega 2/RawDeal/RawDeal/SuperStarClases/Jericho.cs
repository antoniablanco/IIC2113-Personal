namespace RawDeal.SuperStarClases;
using RawDealView;

public class Jericho: SuperStar // Implementar SuperHabilidad
{
    public Jericho(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidadElectiva(Player jugadorActual, Player jugadorCotrario)
    {
        jugadorActual.HabilidadUtilizada = true;
        DescartandoCartasDeHandAlRingSide(jugadorActual);
        DescartandoCartasDeHandAlRingSide(jugadorCotrario);
    }
    
    public override bool PuedeUtilizarSuperAbility(Player jugadorActual)
    {
        return (jugadorActual.cartasHand.Count > 0 && !jugadorActual.HabilidadUtilizada);
    }
}