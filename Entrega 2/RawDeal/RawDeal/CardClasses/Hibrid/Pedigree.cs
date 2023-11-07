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
        string reverseBy, int totaldamage)
    {
        return playedCardController.GetCardTitle() == "Back Body Drop";
    }
    
    public override void ApplyBonusEffect(GameStructureInfo gameStructureInfo)
    {
        gameStructureInfo.BonusManager.ApplyTurnBonusEffect("Pedigree", bonusValue:2);
    }
    
    public override int ExtraDamage(GameStructureInfo gameStructureInfo)
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
        try { cardController = gameStructureInfo.ControllerCurrentPlayer.FindCardCardFrom(
            "RingArea", gameStructureInfo.LastCardBeingPlayedTitle); }
        catch (CardNotFoundException) { cardController = gameStructureInfo.ControllerCurrentPlayer.FindCardCardFrom(
            "RingSide", gameStructureInfo.LastCardBeingPlayedTitle); }

        if (cardController != null)
        {
            if (cardController.ContainsSubtype("Strike") && gameStructureInfo.LastCardBeingPlayedType == "Maneuver" &&
                gameStructureInfo.GetSetGameVariables.GetRoundsInTurn() > 1)
                return 2;
        }

        return 0;
    }
    
}