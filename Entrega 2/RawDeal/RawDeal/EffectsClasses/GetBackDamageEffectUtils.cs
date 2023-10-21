using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class GetBackDamageEffectUtils: EffectsUtils
{
    private int recoveredDamage;
    private PlayerController controllerPlayer;
    private Player player;
    private List<string> ringSideAsString;
    
    public GetBackDamageEffectUtils(PlayerController controllerPlayer, Player player, GameStructureInfo gameStructureInfo, int recoveredDamage = 1)
        : base(gameStructureInfo)
    {
        this.recoveredDamage = recoveredDamage;
        this.controllerPlayer = controllerPlayer;
        this.player = player;
        GetBackDamage();
    }

    private void GetBackDamage()
    {
        ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");

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
            ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");
            var selectedCardIndex =
                gameStructureInfo.View.AskPlayerToSelectCardsToRecover(controllerPlayer.NameOfSuperStar(),
                    recoveredDamage - currentDamage, ringSideAsString);
            var discardedCardController = controllerPlayer.GetSpecificCardFrom("RingSide", selectedCardIndex);

            gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToArsenal(player, discardedCardController,
                "Start");
        }
    }
}