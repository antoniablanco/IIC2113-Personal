using RawDealView.Options;

namespace RawDeal.CardClass.Reversal;

public class JockeyingForPosition: Card
{
    public JockeyingForPosition(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        :base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {
         
    }
    
    public override void ReversalEffect(GameStructureInfo gameStructureInfo)
    {   
        GetSelectedEffect(gameStructureInfo);
        gameStructureInfo.ContadorTurnosJokeyingForPosition = 2;
        gameStructureInfo.CardEffects.EndTurn();
    }
    
    public void GetSelectedEffect(GameStructureInfo gameStructureInfo)
    {   
        SelectedEffect effectToPerform = gameStructureInfo.view.AskUserToSelectAnEffectForJockeyForPosition(gameStructureInfo.ControllerCurrentPlayer
            .NameOfSuperStar());
        switch (effectToPerform)
        {
            case SelectedEffect.NextGrappleIsPlus4D:
                gameStructureInfo.IsJockeyingForPositionBonusDamage = 1;
                break;
            case SelectedEffect.NextGrapplesReversalIsPlus8F:
                gameStructureInfo.IsJockeyingForPositionBonusFortitud= 1;
                break;
        }
    }
}