using RawDeal.Exceptions;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class Pedigree: Card
{
    public Pedigree(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo, 
        string reverseBy, int totalDamage)
    {
        return playedCardController.GetCardTitle() == "Back Body Drop" &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totalDamage);;
    }
    
    public override int GetExtraDamage(GameStructureInfo gameStructureInfo)
    {
        int damage = 0;
        if (gameStructureInfo.LastCardBeingPlayedTitle != null)
        {
            damage += IsNotTheFirstCard(gameStructureInfo);
        }
        return damage;
    }

    private int IsNotTheFirstCard(GameStructureInfo gameStructureInfo)
    {
        CardController cardController;
        try { cardController = gameStructureInfo.ControllerCurrentPlayer.GetCardInDeckByName(
            "RingArea", gameStructureInfo.LastCardBeingPlayedTitle); }
        catch (CardNotFoundException) { cardController = gameStructureInfo.ControllerCurrentPlayer.GetCardInDeckByName(
            "RingSide", gameStructureInfo.LastCardBeingPlayedTitle); }

        if (cardController != null)
        {
            if (cardController.DoesTheCardContainsSubtype("Strike") && gameStructureInfo.LastCardBeingPlayedType == "Maneuver" &&
                gameStructureInfo.GetSetGameVariables.GetRoundsInTurn() > 1)
                return 2;
        }

        return 0;
    }
    
}