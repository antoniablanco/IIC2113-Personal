namespace RawDeal.SuperStarClases;
using RawDealView;

public class Mankind: SuperStar
{
    public Mankind(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidad(Player jugadorActual, Player jugadorCotrario)
    {
        if (jugadorActual.cartasArsenal.Count > 0)
            jugadorActual.RobarCarta();
    }
}