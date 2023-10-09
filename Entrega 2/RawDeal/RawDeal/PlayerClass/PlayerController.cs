using RawDeal.CardClass;
using RawDeal.DecksBehavior;

namespace RawDeal.PlayerClass;

public class PlayerController
{
    private Player player;
    private GameStructureInfo gameStructureInfo;
    
    public PlayerController(Player player, GameStructureInfo gameStructureInfo)
    {
        this.player = player;
        this.gameStructureInfo = gameStructureInfo;
    }
    
    public void DrawCard()
    {   
        gameStructureInfo.CardMovement.TransferOfUnselectedCard(player.cardsArsenal, player.cardsHand, false);
    }
    
    public string NameOfSuperStar()
    {
        return player.superestar.Name;
    }

    public void DrawInitialHandCards()
    {
        for (var i = 0; i < player.superestar.HandSize; i++)
        {
            DrawCard();
        }
    }
    
    public int FortitudRating()
    {
        return player.cardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
    }

    public bool IsTheSuperStarMankind()
    {
        return NameOfSuperStar() == "MANKIND";
    }

    public int GetSuperStarValue()
    {
        return player.superestar.SuperstarValue;
    }
    
    public bool AreThereCardsLeftInTheArsenal()
    {
        return player.cardsArsenal.Count() > 0;
    }
    
    public List<CardController> CardsAvailableToPlay()
    {
        return player.cardsHand
            .Where(card => card.GetCardFortitude() <= FortitudRating() && card.HasAnyTypeDifferentOfReversal())
            .ToList();
    }
    
    public List<CardController> CardsAvailableToReversal() 
    {
        return player.cardsHand
            .Where(card => card.GetCardFortitude() + gameStructureInfo.bonusFortitude*gameStructureInfo.IsJockeyingForPositionBonusFortitud <= FortitudRating() && card.IsReversalType() && CanReversalPlayedCard(card))
            .ToList();
    }

    private bool CanReversalPlayedCard(CardController card) 
    {
        return card.GetIfCardCanReversalPlayedCard();
    }
    
    public bool HasCardsInArsenal()
    {
        return (player.cardsArsenal.Count > 0);
    }
    
    public bool TheirSuperStarCanUseSuperAbility(PlayerController currentPlayer)
    {
        return player.superestar.CanUseSuperAbility(currentPlayer);
    }

    public void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        player.superestar.UsingElectiveSuperAbility(gameStructureInfo);
    }
        
    public void UsingAutomaticSuperAbility(GameStructureInfo gameStructureInfo)
    {
        player.superestar.UsingAutomaticSuperAbilityAtTheStartOfTheTurn(gameStructureInfo);
    }
    
    public void BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        player.superestar.BlockinSuperAbilityBecauseIsJustAtTheStartOfTheTurn(gameStructureInfo);
    }
    
    public void TheSuperStarHasUsedHisSuperAbilityThisTurn()
    {
        player.theHabilityHasBeenUsedThisTurn = true;
    }

    public void TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility()
    {
        player.theHabilityHasBeenUsedThisTurn = false;
    }

    public int NumberOfCardsInTheArsenal()
    {
        return player.cardsArsenal.Count();
    }
    
    public int NumberOfCardsInTheHand()
    {
        return player.cardsHand.Count();
    }
    
    public int NumberOfCardsInRingSide()
    {
        return player.cardsRingSide.Count();
    }
    
    public CardController GetSpecificCardFromHand(int index)
    {
        return player.cardsHand[index];
    }
    
    public CardController GetSpecificCardFromRingSide(int index)
    {
        return player.cardsRingSide[index];
    }
    
    public List<String> StringCardsHand()
    {
        List<String> stringCardSet = gameStructureInfo.VisualizeCards.CreateStringCardList(player.cardsHand);
        return stringCardSet;
    }
    
    public List<String> StringCardsRingArea()
    {
        List<String> stringCardSet = gameStructureInfo.VisualizeCards.CreateStringCardList(player.cardsRingArea);
        return stringCardSet;
    }
    
    public List<String> StringCardsRingSide()
    {
        List<String> stringCardSet = gameStructureInfo.VisualizeCards.CreateStringCardList(player.cardsRingSide);
        return stringCardSet;
    }

    public bool HasTheSuperAbilityBeenUsedThisTurn()
    {
        return player.theHabilityHasBeenUsedThisTurn;
    }
 
}