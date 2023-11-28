using RawDeal.EffectsClasses;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses.UnspecifiedType;

public class RockBottom : Card
{
    public RockBottom(string title, List<string> types, List<string> subtypes, string fortitude, string damage,
        string stunValue, string cardEffect)
        : base(title, types, subtypes, fortitude, damage, stunValue, cardEffect)
    {

    }

    public override bool CanReversalThisCard(CardController playedCardController, GameStructureInfo gameStructureInfo,
        string reverseBy, int totaldamage)
    {
        int numberOfCardsInHand = gameStructureInfo.ControllerOpponentPlayer.NumberOfCardIn("Hand");
        return playedCardController.ContainsSubtype("Grapple")
               && reverseBy == "Hand" && playedCardController.VerifyIfTheLastPlayedTypeIs("Maneuver")
               && numberOfCardsInHand > 1 &&
               gameStructureInfo.BonusManager.CanReversal(gameStructureInfo, reverseBy, totaldamage);;
    }

    public override void ApplyReversalEffect(GameStructureInfo gameStructureInfo)
    {
        const int numberOfCardToDiscard = 1;
        new HandToRingSideDiscardEffect(gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo.ControllerOpponentPlayer, numberOfCardToDiscard, gameStructureInfo);
        new FindAndMoveCard(cardTitle:"The People's Elbow", gameStructureInfo.ControllerOpponentPlayer,
            gameStructureInfo);
    }
    
    public override void ApplyManeuverEffect(GameStructureInfo gameStructureInfo, CardController playedCardController)
    {
        new FindAndMoveCard(cardTitle:"The People's Elbow", gameStructureInfo.ControllerCurrentPlayer,
            gameStructureInfo);
    }
}