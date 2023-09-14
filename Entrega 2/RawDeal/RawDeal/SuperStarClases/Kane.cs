namespace RawDeal.SuperStarClases;
using RawDealView;

public class Kane: SuperStar
{
    public Kane(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
        : base(name, logo, handSize, superstarValue, superstarAbility, view)
    {
        // Constructor de la clase base
    }
    
    public override void UtilizandoSuperHabilidad(Player jugadorActual, Player jugadorCotrario)
    {
        _view.SayThatPlayerIsGoingToUseHisAbility(Name, SuperstarAbility);
    }
    
}