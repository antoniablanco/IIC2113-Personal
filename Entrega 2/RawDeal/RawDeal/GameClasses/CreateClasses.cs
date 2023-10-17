using RawDeal.DecksBehavior;

namespace RawDeal.GameClasses;

public class CreateClasses
{
    
    private GameStructureInfo gameStructureInfo;
    
    public CreateClasses(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
        Create();
    }
    
    private void Create()
    {
        CreateGetSetGameVariablesClass();
        CreatePlayCardClass();
        CreateEffectsClass();
        CreateGameLogicClass();
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

    private void CreateGameLogicClass()
    {
        GameLogic gameLogic = new GameLogic(gameStructureInfo);
        gameStructureInfo.GameLogic = gameLogic;
    }

}