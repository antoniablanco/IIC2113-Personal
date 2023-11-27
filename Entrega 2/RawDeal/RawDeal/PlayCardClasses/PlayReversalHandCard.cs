using RawDeal.CardClasses;
using RawDeal.EffectsClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.DecksBehavior;

public class PlayReversalHandCard
{
    private readonly GameStructureInfo gameStructureInfo = new();
    private readonly int totalDamage;

    public PlayReversalHandCard(GameStructureInfo gameStructureInfo, int totalDamage)
    {
        this.gameStructureInfo = gameStructureInfo;
        this.totalDamage = totalDamage;
    }


    public void IsUserUsingReversalCard()
    {
        var possibleReversals = gameStructureInfo.ControllerOpponentPlayer.CardsAvailableToReversal(totalDamage);

        if (possibleReversals.Count > 0)
            AskWhichReversalCardWantsToUse(possibleReversals);
    }

    private void AskWhichReversalCardWantsToUse(List<CardController> possibleReversals)
    {
        var indexReversalCard = UserSelectReversalCard();
        if (gameStructureInfo.PlayCard.HasSelectedAValidCard(indexReversalCard))
        {
            PlayingReversalCard(indexReversalCard, possibleReversals);
            gameStructureInfo.EffectsUtils.EndTurn();
            throw new UserPlayReversalCardException("The User has played a reversal card");
        }
        
    }
    

    private int UserSelectReversalCard()
    {
        var possibleCardsAndTheirTypes = gameStructureInfo.ControllerOpponentPlayer
            .GetPosiblesCardsForReveralWithTheirReversalTypeIndex(totalDamage);
        var possibleReversalsString =
            gameStructureInfo.CardsVisualizor.GetStringCardsForSpecificType(possibleCardsAndTheirTypes);
        var indexReversalCard =
            gameStructureInfo.View.AskUserToSelectAReversal(
                gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), possibleReversalsString);
        return indexReversalCard;
    }

    private void PlayingReversalCard(int indexReversalCard, List<CardController> possibleReversals)
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
            gameStructureInfo.ControllerOpponentPlayer.NameOfSuperStar(), reversalString);
    }

    private void MoveCardsImplicateInReversal(CardController cardController)
    {
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(gameStructureInfo.GetCurrentPlayer(),
            gameStructureInfo.CardBeingPlayed);
        gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingArea(gameStructureInfo.GetOpponentPlayer(),
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
            damageProduce = gameStructureInfo.EffectsUtils.GetDamageProducedByReversalCardWithNotEspecificDamage() +
                            gameStructureInfo.BonusManager.EternalDamage(gameStructureInfo.CardBeingPlayed, 
                                gameStructureInfo.ControllerCurrentPlayer);
        else
            damageProduce =
                gameStructureInfo.PlayCard.ObtainDamageByCheckingIfTheCardBelongsToMankindSuperStar(
                    cardController.GetDamageProducedByTheCard(),
                    damagedPlayerController);
        damageProduce += gameStructureInfo.CardBeingPlayed.ExtraReversalDamage();
        return damageProduce;
    }
}