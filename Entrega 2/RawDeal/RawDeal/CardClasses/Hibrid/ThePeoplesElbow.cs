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
            gameStructureInfo.ControllerCurrentPlayer.NameOfSuperStar(), Title);
        
        gameStructureInfo.EffectsUtils.DiscardActionCardToRingAreButNotSaying(playedCardController,
            gameStructureInfo.GetCurrentPlayer(), moveTo:"Start");
        
        const int numberOfCardsToSteal = 2;
        new DrawCardEffect(gameStructureInfo.ControllerCurrentPlayer, 
            gameStructureInfo).StealCards(numberOfCardsToSteal);
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo)
    {
        try
        {
            gameStructureInfo.ControllerCurrentPlayer.FindCardCardFrom("RingArea", "Rock Bottom");
            return true;
        }
        catch (CardNotFoundException e) { return false; }
        
    }
}