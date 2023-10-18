using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class DamageEffects
{   
    private readonly GameStructureInfo gameStructureInfo = new();

    public DamageEffects(GameStructureInfo gameStructureInfo)
    {
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void ProduceSeveralDamage(int totalDamage, PlayerController controllerOpponentPlayer, Player player)
    {
        if (HasDamageToApply(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(),
                totalDamage);

            for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
                InflictADamage(totalDamage, currentDamage, controllerOpponentPlayer, player);
        }
    }

    private bool InflictADamage(int totalDamage, int currentDamage, PlayerController controllerOpponentPlayer,
        Player player)
    {
        if (CheckIfThePlayerCanReceiveDamage(controllerOpponentPlayer))
            ShowOneFaceDownCard(currentDamage + 1, totalDamage, player);
        else
        {
            gameStructureInfo.GetSetGameVariables.SetVariablesAfterWinning(controllerOpponentPlayer);
            return false;
        }

        return true;
    }

    private bool CheckIfThePlayerCanReceiveDamage(PlayerController controllerPlayer)
    {
        return controllerPlayer.HasCardsInArsenal();
    }

    private string ShowOneFaceDownCard(int currentDamage, int totalDamage, Player player)
    {
        var flippedCardController = gameStructureInfo.CardMovement.TranferUnselectedCardFromArsenalToRingSide(player);
        var flippedCardString = flippedCardController.GetStringCardInfo();
        gameStructureInfo.View.ShowCardOverturnByTakingDamage(flippedCardString, currentDamage, totalDamage);
        return flippedCardString;
    }

    public void ColateralDamage(PlayerController controllerOpponentPlayer, Player player, int totalDamage = 1)
    {
        gameStructureInfo.View.SayThatPlayerDamagedHimself(controllerOpponentPlayer.NameOfSuperStar(), totalDamage);
        gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerOpponentPlayer.NameOfSuperStar(),
            totalDamage);

        for (var currentDamage = 0; currentDamage < totalDamage; currentDamage++)
            if (!InflictADamage(totalDamage, currentDamage, controllerOpponentPlayer, player))
                gameStructureInfo.View.SayThatPlayerLostDueToSelfDamage(controllerOpponentPlayer.NameOfSuperStar());
    }

    public void GetBackDamage(PlayerController controllerPlayer, Player player, int recoveredDamage = 1)
    {
        var ringSideAsString = controllerPlayer.StringCardsFrom("RingSide");

        recoveredDamage = LimitRecoveryToAvailableDamage(recoveredDamage, ringSideAsString);

        RecoverDamageFromRingSide(controllerPlayer, player, recoveredDamage, ringSideAsString);
    }

    private int LimitRecoveryToAvailableDamage(int recoveredDamage, List<string> ringSideAsString)
    {
        if (ringSideAsString.Count() < recoveredDamage)
            recoveredDamage = ringSideAsString.Count();

        return recoveredDamage;
    }

    private void RecoverDamageFromRingSide(PlayerController controllerPlayer, Player player, int recoveredDamage,
        List<string> ringSideAsString)
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

    public void TakeDamage(PlayerController controllerPlayer, Player player, int totalDamage)
    {
        if (HasDamageToApply(totalDamage))
        {
            gameStructureInfo.View.SayThatSuperstarWillTakeSomeDamage(controllerPlayer.NameOfSuperStar(), totalDamage);
            ApplyDamageToPlayer(player, totalDamage);
        }
    }

    private bool HasDamageToApply(int totalDamage)
    {
        return totalDamage > 0;
    }

    private void ApplyDamageToPlayer(Player player, int totalDamage)
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