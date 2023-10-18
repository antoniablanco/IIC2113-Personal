using RawDeal.DecksBehavior;
using RawDeal.EffectsClasses;

namespace RawDeal.GameClasses;

public class ClassesGenerator
{
    private GameStructureInfo gameStructureInfo;

    public ClassesGenerator(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
        Create();
    }

    private void Create()
    {
        CreateGetSetGameVariablesClass();
        CreatePlayCardClass();
        CreateEffectsClass();
        CreateDamageEffectsClass();
        CreateViewDecksClass();
        CreateBonusManagerClass();
        CreateEndTurnManagerClass();
    }

    private void CreateGetSetGameVariablesClass()
    {
        GetSetGameVariables getSetGameVariables = new GetSetGameVariables(gameStructureInfo);
        gameStructureInfo.GetSetGameVariables = getSetGameVariables;
    }

    private void CreatePlayCardClass()
    {
        PlayCard playCard = new PlayCard(gameStructureInfo);
        gameStructureInfo.PlayCard = playCard;
    }

    private void CreateEffectsClass()
    {
        Effects effects = new Effects(gameStructureInfo);
        gameStructureInfo.Effects = effects;
    }
    
    private void CreateDamageEffectsClass()
    {
        DamageEffects damageffects = new DamageEffects(gameStructureInfo);
        gameStructureInfo.DamageEffects = damageffects;
    }
    
    private void CreateViewDecksClass()
    {
        ViewDecks viewDecks = new ViewDecks(gameStructureInfo);
        gameStructureInfo.ViewDecks = viewDecks;
    }

    private void CreateBonusManagerClass()
    {
        BonusManager bonusManager = new BonusManager(gameStructureInfo);
        gameStructureInfo.BonusManager = bonusManager;
    }

    private void CreateEndTurnManagerClass()
    {
        EndTurnManager endTurnManager = new EndTurnManager(gameStructureInfo);
        gameStructureInfo.EndTurnManager = endTurnManager;
    }
}