using RawDeal.CardClasses;
using RawDeal.EffectsClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class ReversalHandCardPlay
{
    private readonly GameStructureInfo gameStructureInfo = new();
    private readonly int totalDamage;

    public ReversalHandCardPlay(GameStructureInfo gameStructureInfo, int totalDamage)
    {
        this.gameStructureInfo = gameStructureInfo;
        this.totalDamage = totalDamage;
    }
    
    public void IsUserUsingReversalCard()
    {
        var possibleReversals = gameStructureInfo.ControllerOpponentPlayer.GetCardsAvailableToReversal(totalDamage);

        if (possibleReversals.Count > 0)
            AskWhichReversalCardWantsToUse(possibleReversals);
    }

    private void AskWhichReversalCardWantsToUse(List<CardController> possibleReversals)
    {
        var indexReversalCard = GetIndexOfReversalCardUserSelect();
        if (gameStructureInfo.CardPlay.HasSelectedAValidCard(indexReversalCard))
        {
            PlayReversalCard(indexReversalCard, possibleReversals);
            gameStructureInfo.EffectsUtils.EndTurn();
            throw new UserPlayReversalCardException("The User has played a reversal card");
        }
        
    }

    private int GetIndexOfReversalCardUserSelect()
    {
        var possibleCardsAndTheirTypes = gameStructureInfo.ControllerOpponentPlayer
            .GetPossiblesCardsForReversalWithTheirTypeIndex(totalDamage);
        var possibleReversalsString =
            gameStructureInfo.CardsVisualizer.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        var indexReversalCard =
            gameStructureInfo.View.AskUserToSelectAReversal(
                gameStructureInfo.ControllerOpponentPlayer.GetNameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayReversalCard(int indexReversalCard, List<CardController> possibleReversals)
    {
        var cardController = possibleReversals[indexReversalCard];
        SayTheReversalCardIsPlayed(cardController);
        MoveCardsImplicateInReversal(cardController);
        cardController.ApplyReversalEffect();
        ApplyDamage(cardController);
    }

    private void SayTheReversalCardIsPlayed(CardController cardController)
    {
        var indexType = cardController.GetIndexForType("Reversal");
        var reversalString = cardController.GetStringPlayedInfo(indexType);
        gameStructureInfo.View.SayThatPlayerReversedTheCard(
            gameStructureInfo.ControllerOpponentPlayer.GetNameOfSuperStar(), reversalString);
    }

    private void MoveCardsImplicateInReversal(CardController cardController)
    {
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo.CardBeingPlayed);
        gameStructureInfo.CardMovement.TransferSelectedCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(),
            cardController);
    }

    private void ApplyDamage(CardController cardController)
    {
        var damagedPlayerController = gameStructureInfo.ControllerCurrentPlayer;
        int damageProduce = GetDamageProducedByReversalCard(cardController, damagedPlayerController);
        
        new ProduceDamageEffectUtils(damageProduce, damagedPlayerController,
            gameStructureInfo);
    }

    private int GetDamageProducedByReversalCard(CardController cardController, PlayerController damagedPlayerController)
    {
        int damageProduce;
        if (cardController.IsDamageHashtagType())
            damageProduce = gameStructureInfo.EffectsUtils.GetDamageProducedByReversalCardWithNotSpecificDamage() +
                            gameStructureInfo.BonusManager.GetEternalDamage(gameStructureInfo.CardBeingPlayed, 
                                gameStructureInfo.ControllerCurrentPlayer);
        else
            damageProduce =
                gameStructureInfo.CardPlay.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(
                    cardController.GetDamageProducedByTheCard(),
                    damagedPlayerController);
        damageProduce += gameStructureInfo.CardBeingPlayed.GetExtraReversalDamage();
        return damageProduce;
    }
}