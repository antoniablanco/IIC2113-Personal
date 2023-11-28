using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class CollateralDamageEffectUtils: EffectsUtils
{
    private int totalDamage;
    private PlayerController controllerPlayer;
    private Player player;
    
    public CollateralDamageEffectUtils(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo, int totalDamage = 1)
        : base(gameStructureInfo)
    {   
        this.totalDamage = totalDamage;
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        ProduceCollateralDamage();
    }
    
    private void ProduceCollateralDamage()
    {
        gameStructureInfo.View.SayThatPlayerDamagedHimself(controllerPlayer.GetNameOfSuperStar(), totalDamage);
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.GetNameOfSuperStar(),
            totalDamage);

        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            InflictADamage(currentDamage);
    }
    
    private void InflictADamage(int currentDamage)
    {
        if (CheckIfThePlayerHasCardInArsenal(controllerPlayer))
            ShowOneFaceDownCard(currentDamage + 1);
        else
        {
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerPlayer);
            gameStructureInfo.View.SayThatPlayerLostDueToSelfDamage(controllerPlayer.GetNameOfSuperStar());
        }
    }
    
    private void ShowOneFaceDownCard(int currentDamage)
    {
        var flippedCardController = gameStructureInfo.CardMovement.TransferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
    }
    
}