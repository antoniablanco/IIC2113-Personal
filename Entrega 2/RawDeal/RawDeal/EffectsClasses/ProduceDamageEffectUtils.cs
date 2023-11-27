using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class ProduceDamageEffectUtils: EffectsUtils
{
    private int totalDamage;
    private PlayerController controllerPlayer;
    private Player player;
    
    public ProduceDamageEffectUtils(int totalDamage, PlayerController controllerPlayer, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {   
        this.totalDamage = totalDamage;
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        ProduceSeveralDamage();
    }

    private void ProduceSeveralDamage()
    {
        if (IsPositive(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(),
                totalDamage);

            for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
                InflictADamage(currentDamage);
        }
    }
    
    private void InflictADamage(int currentDamage)
    {
        if (CheckIfThePlayerHasCardInArsenal(controllerPlayer))
            ShowOneFaceDownCard(currentDamage + 1 );
        else
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerPlayer);
    }
    
    private string ShowOneFaceDownCard(int currentDamage)
    {
        var flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }
}