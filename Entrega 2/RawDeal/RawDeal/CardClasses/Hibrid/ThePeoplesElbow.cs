using RawDeal.EffectsClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class ThePeoplesElbow: Card
{
    public ThePeoplesElbow(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ApplyActionEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {   
        gameStructureInfo.View.SayThatPlayerPutsThisCardAtTheBottomOfHisArsenal(
            gameStructureInfo.ControllerCurrentPlayer.GetNameOfSuperStar(), Title);
        
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToStartOfArsenal(gameStructureInfo.GetCurrentPlayer(),
            playedCardController);
        
        const int numberOfCardsToSteal = 2;
        new CardDrawEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards(numberOfCardsToSteal);
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Action")
    { 
        if (type == "Maneuver")
            try
            {
                gameStructureInfo.ControllerCurrentPlayer.GetCardInDeckByName("RingArea", "Rock Bottom");
                return true;
            }
            catch (CardNotFoundException e) { return false; }

        return true;
    }
}