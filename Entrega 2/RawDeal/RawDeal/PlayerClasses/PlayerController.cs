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
    
    public void DrawInitialHandCards()
    {
        for (var i = 0; i < player.Superestar.HandSize; i++)
        {
            DrawCard();
        }
    }
    
    public void DrawCard()
    {   
        gameStructureInfo.CardMovement.TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand, false);
    }
    
    public string NameOfSuperStar()
    {
        return player.Superestar.Name;
    }
    
    public int GetSuperStarValue()
    {
        return player.Superestar.SuperstarValue;
    }
    
    public bool HasCardsInArsenal()
    {
        return (player.CardsArsenal.Count > 0);
    }
    
    public List<CardController> CardsAvailableToPlay()
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) <= FortitudRating() && card.HasAnyTypeDifferentOfReversal() && card.CanThisCardBePlayed())
            .ToList();
    }
    
    public List<CardController> CardsAvailableToReversal() 
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + gameStructureInfo.GetSetGameVariables.AddBonusFortitud() <= FortitudRating() && card.IsReversalType() && CanReversalPlayedCard(card))
            .ToList();
    }
    
    public List<Tuple<CardController, int>> GetPosiblesCardsToPlayAndTheirTypeIndex(List<CardController> cardsInSelectedSet)
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();

        foreach (var cardController in cardsInSelectedSet)
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) != "Reversal" && cardController.GetCardFortitude(cardController.GetCardType(index)) <= FortitudRating() && cardController.CanThisCardBePlayed()
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
    }
    
    public int FortitudRating()
    {
        return player.CardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
    }
    
    private bool CanReversalPlayedCard(CardController card) 
    {   
        return card.GetIfCardCanReversalPlayedCard();
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
    
    public bool HasTheSuperAbilityBeenUsedThisTurn()
    {
        return player.TheHabilityHasBeenUsedThisTurn;
    }
    
    public void TheSuperStarHasUsedHisSuperAbilityThisTurn()
    {
        player.TheHabilityHasBeenUsedThisTurn = true;
    }

    public void TheTurnHasJustStartTheSuperStarHasNotUsedHisSuperAbility()
    {
        player.TheHabilityHasBeenUsedThisTurn = false;
    }
    
    public int NumberOfCardIn(string deck)
    {
        return deck switch
        {
            "Hand" => player.CardsHand.Count(),
            "RingSide" => player.CardsRingSide.Count(),
            "Arsenal" => player.CardsArsenal.Count(),
            _ => player.CardsHand.Count()
        };
    }
    
    public CardController GetSpecificCardFrom(string deck, int index)
    {
        return deck switch
        {
            "Hand" => player.CardsHand[index],
            "RingSide" => player.CardsRingSide[index],
            _ => player.CardsHand[index]
        };
    }

    public List<String> StringCardsFrom(string deck)
    {
        return deck switch
        {
            "Hand" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsHand),
            "RingSide" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingSide),
            "RingArea" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingArea),
            _ => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsHand)
        };
    }

    public (List<String>, List<CardController>) HandCardsButNotTheCardIsBeingPlayed(CardController cardController)
    {
        List<CardController> cardOptions = player.CardsHand.Where(card => card != cardController).ToList();
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizor.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
 
}