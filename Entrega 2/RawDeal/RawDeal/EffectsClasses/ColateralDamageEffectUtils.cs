using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class ColateralDamageEffectUtils: EffectsUtils
{
    private int totalDamage;
    private PlayerController controllerPlayer;
    private Player player;
    
    public ColateralDamageEffectUtils(PlayerController controllerPlayer, Player player, GameStructureInfo gameStructureInfo, int totalDamage = 1)
        : base(gameStructureInfo)
    {   
        this.totalDamage = totalDamage;
        this.controllerPlayer = controllerPlayer;
        this.player = player;
        ColateralDamage();
    }
    
    private void ColateralDamage()
    {
        gameStructureInfo.View.SayThatPlayerDamagedHimself(controllerPlayer.NameOfSuperStar(), totalDamage);
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(),
            totalDamage);

        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            if (!InflictADamage(currentDamage))
                gameStructureInfo.View.SayThatPlayerLostDueToSelfDamage(controllerPlayer.NameOfSuperStar());
    }
    
    private bool InflictADamage(int currentDamage)
    {
        if (CheckIfThePlayerHasCardInArsenal(controllerPlayer))
            ShowOneFaceDownCard(currentDamage + 1);
        else
        {
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerPlayer);
            return false;
        }

        return true;
    }


    private string ShowOneFaceDownCard(int currentDamage)
    {
        var flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }
    
}