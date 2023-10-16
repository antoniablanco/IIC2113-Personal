using RawDeal.CardClass;
using RawDeal.GameClasses;

namespace RawDeal.PlayerClasses;

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
        gameStructureInfo.CardMovement.TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand, false);
    }
    
    public string NameOfSuperStar()
    {
        return player.Superestar.Name;
    }

    public void DrawInitialHandCards()
    {
        for (var i = 0; i < player.Superestar.HandSize; i++)
        {
            DrawCard();
        }
    }
    
    public int FortitudRating()
    {
        return player.CardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
    }

    public bool IsTheSuperStarMankind()
    {
        return NameOfSuperStar() == "MANKIND";
    }

    public int GetSuperStarValue()
    {
        return player.Superestar.SuperstarValue;
    }
    
    public bool AreThereCardsLeftInTheArsenal()
    {
        return player.CardsArsenal.Count() > 0;
    }
    
    public List<CardController> CardsAvailableToPlay()
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) <= FortitudRating() && card.HasAnyTypeDifferentOfReversal() && card.CanThisCardBePlayed())
            .ToList();
    }
    
    public List<Tuple<CardController, int>> GetPosiblesCardsToPlay(List<CardController> cardsInSelectedSet)
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();

        foreach (var cardController in cardsInSelectedSet)
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) != "Reversal" && cardController.GetCardFortitude(cardController.GetCardType(index)) <= FortitudRating() 
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
    }
    
    public List<CardController> CardsAvailableToReversal() 
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + gameStructureInfo.BonusFortitude*gameStructureInfo.IsJockeyingForPositionBonusFortitudActive <= FortitudRating() && card.IsReversalType() && CanReversalPlayedCard(card))
            .ToList();
    }

    private bool CanReversalPlayedCard(CardController card) 
    {   
        return card.GetIfCardCanReversalPlayedCard();
    }
    
    public bool HasCardsInArsenal()
    {
        return (player.CardsArsenal.Count > 0);
    }
    
    public bool TheirSuperStarCanUseSuperAbility(PlayerController currentPlayer)
    {
        return player.Superestar.CanUseSuperAbility(currentPlayer);
    }

    public void UsingElectiveSuperAbility()
    {
        player.Superestar.UsingElectiveSuperAbility(gameStructureInfo);
    }
        
    public void UsingAutomaticSuperAbility()
    {
        player.Superestar.UsingAutomaticSuperAbilityAtTheStartOfTheTurn(gameStructureInfo);
    }
    
    public void BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn()
    {
        player.Superestar.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn(gameStructureInfo);
    }
    
    public void TheSuperStarHasUsedHisSuperAbilityThisTurn()
    {
        player.TheHabilityHasBeenUsedThisTurn = true;
    }

    public void TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility()
    {
        player.TheHabilityHasBeenUsedThisTurn = false;
    }

    public int NumberOfCardsInTheArsenal()
    {
        return player.CardsArsenal.Count();
    }
    
    public int NumberOfCardsInTheHand()
    {
        return player.CardsHand.Count();
    }
    
    public int NumberOfCardsInRingSide()
    {
        return player.CardsRingSide.Count();
    }
    
    public CardController GetSpecificCardFromHand(int index)
    {
        return player.CardsHand[index];
    }
    
    public CardController GetSpecificCardFromRingSide(int index)
    {
        return player.CardsRingSide[index];
    }
    
    public List<String> StringCardsHand()
    {
        List<String> stringCardSet = gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsHand);
        return stringCardSet;
    }

    public (List<String>, List<CardController>) HandCardsButNotTheCardIsBeingPlayed(CardController cardController)
    {
        List<CardController> cardOptions = player.CardsHand.Where(card => card != cardController).ToList();
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizor.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
    
    public List<String> StringCardsRingArea()
    {
        List<String> stringCardSet = gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingArea);
        return stringCardSet;
    }
    
    public List<String> StringCardsRingSide()
    {
        List<String> stringCardSet = gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingSide);
        return stringCardSet;
    }

    public bool HasTheSuperAbilityBeenUsedThisTurn()
    {
        return player.TheHabilityHasBeenUsedThisTurn;
    }
 
}