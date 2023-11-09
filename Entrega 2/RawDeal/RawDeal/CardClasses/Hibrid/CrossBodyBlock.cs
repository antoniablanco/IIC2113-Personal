using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class CrossBodyBlock: Card
{
    public CrossBodyBlock(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totaldamage)
    {   
        return gameStructureInfo.LastCardBeingPlayedTitle == "Irish Whip" 
               && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver") &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totaldamage);;
    }
    
    public override bool CheckIfCardCanBePlayed(GameStructureInfo gameStructureInfo, string type = "Maneuver")
    {   
        if (gameStructureInfo.CardBeingPlayed == null)
            return false;
        return gameStructureInfo.CardBeingPlayed.GetCardTitle() == "Irish Whip";
    }
    
}