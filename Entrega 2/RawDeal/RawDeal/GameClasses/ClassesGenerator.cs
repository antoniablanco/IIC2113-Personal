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
        CreateCardMovementClass();
        CreatePlayCardClass();
        CreateEffectsClass();
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
        EffectsUtils effectsUtils = new EffectsUtils(gameStructureInfo);
        gameStructureInfo.EffectsUtils = effectsUtils;
    }
    
    private void CreateViewDecksClass()
    {
        ViewDecks viewDecks = new ViewDecks(gameStructureInfo);
        gameStructureInfo.ViewDecks = viewDecks;
    }

    private void CreateBonusManagerClass()
    {
        BonusManager bonusManager = new BonusManager(gameStructureInfo.BonusStructureInfo);
        gameStructureInfo.BonusManager = bonusManager;
    }

    private void CreateEndTurnManagerClass()
    {
        EndTurnManager endTurnManager = new EndTurnManager(gameStructureInfo);
        gameStructureInfo.EndTurnManager = endTurnManager;
    }
    
    private void CreateCardMovementClass()
    {
        CardMovement cardMovement = new CardMovement(gameStructureInfo);
        gameStructureInfo.CardMovement = cardMovement;
    }
}