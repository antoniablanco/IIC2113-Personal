using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class TakeDamageEffectUtils: EffectsUtils
{
    private int  totalDamage;
    private PlayerController controllerPlayer;
    private Player player;
    
    public TakeDamageEffectUtils(PlayerController controllerPlayer, int  totalDamage, GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this. totalDamage = totalDamage;
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        TakeDamage();
    }

    private void TakeDamage()
    {
        if (IsPositive(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.GetNameOfSuperStar(), totalDamage);
            ApplyDamageToPlayer();
        }
    }

    private void ApplyDamageToPlayer()
    {
        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
        {
            var flippedCardController =
                gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
            var flippedCardString = flippedCardController.GetStringCardInfo();

            gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage + 1, totalDamage);
        }
    }
}