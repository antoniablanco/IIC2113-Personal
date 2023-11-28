using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class GetBackDamageEffectUtils: EffectsUtils
{
    private int recoveredDamage;
    private PlayerController controllerPlayer;
    private Player player;
    private List<string> ringSideAsString;
    
    public GetBackDamageEffectUtils(PlayerController controllerPlayer, GameStructureInfo gameStructureInfo, int recoveredDamage = 1)
        : base(gameStructureInfo)
    {
        this.recoveredDamage = recoveredDamage;
        this.controllerPlayer = controllerPlayer;
        player = gameStructureInfo.ControllerOpponentPlayer == controllerPlayer ? 
            gameStructureInfo.GetOpponentPlayer() : gameStructureInfo.GetCurrentPlayer();
        
        GetBackDamage();
    }

    private void GetBackDamage()
    {
        ringSideAsString = controllerPlayer.GetStringCardsFrom("RingSide");

        recoveredDamage = LimitRecoveryToAvailableDamage();

        RecoverDamageFromRingSide();
    }

    private int LimitRecoveryToAvailableDamage()
    {
        if (ringSideAsString.Count() < recoveredDamage)
            recoveredDamage = ringSideAsString.Count();

        return recoveredDamage;
    }

    private void RecoverDamageFromRingSide()
    {
        for (var currentDamage = 0; currentDamage < recoveredDamage; currentDamage++)
        {
            ringSideAsString = controllerPlayer.GetStringCardsFrom("RingSide");
            var selectedCardIndex =
                gameStructureInfo.View.AskPlayerToSelectCardsToRecover(controllerPlayer.GetNameOfSuperStar(),
                    recoveredDamage - currentDamage, ringSideAsString);
            var discardedCardController = controllerPlayer.RetrieveCardFromDeckAtPosition("RingSide", selectedCardIndex);

            gameStructureInfo.CardMovement.TransferSelectedCardFromRingSideToStartOfArsenal(player, discardedCardController);
        }
    }
}