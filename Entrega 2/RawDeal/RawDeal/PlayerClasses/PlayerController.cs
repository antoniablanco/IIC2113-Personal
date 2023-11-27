using RawDeal.CardClasses;
using RawDeal.Exceptions;
using RawDeal.GameClasses;

namespace RawDeal.PlayerClasses;

public class PlayerController
{
    private GameStructureInfo gameStructureInfo;
    private Player player;

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

    private List<CardController> CardsAvailableToPlay()
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + card.PlusFornitudAfterEspecificCard() <= FortitudRating() 
                           && card.HasAnyTypeDifferentOfReversal() && card.CanThisCardBePlayed())
            .ToList();
    }

    public List<CardController> CardsAvailableToReversal(int totalDamage) 
    {   
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + 
                gameStructureInfo.BonusManager.GetFortitudBonus(gameStructureInfo.CardBeingPlayedType) <= FortitudRating() 
                && card.IsReversalType() && CanReversalPlayedCard(card, "Hand", totalDamage))
            .ToList();
    }

    public List<Tuple<CardController, int>> GetPosiblesCardsToPlayWithTheirTypeIndex()
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();

        foreach (var cardController in CardsAvailableToPlay())
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) 
                    != "Reversal" && cardController.CanThisCardBePlayed(cardController.GetCardType(index)) &&
                    cardController.GetCardFortitude(cardController.GetCardType(index)) + cardController.PlusFornitudAfterEspecificCard()
                    <= FortitudRating() && cardController.CanThisCardBePlayed()
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
    }

    public List<Tuple<CardController, int>> GetPosiblesCardsForReveralWithTheirReversalTypeIndex(int totalDamage)
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();
        
        foreach (var cardController in CardsAvailableToReversal(totalDamage))
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) == "Reversal" 
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
        
    }
    
    public int FortitudRating()
    {
        return player.CardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
    }

    private bool CanReversalPlayedCard(CardController card, string reverseBy, int totalDamage)
    {   
        return card.GetIfCardCanReversalPlayedCard(reverseBy, totalDamage);
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
            "Arsenal" => player.CardsArsenal[index],
            "RingArea" => player.CardsRingArea[index],
            _ => player.CardsHand[index]
        };
    }
    
    public CardController FindCardCardFrom(string deck, string cardName)
    {   
        CardController foundCard = deck switch
        {
            "Hand" => player.CardsHand.Find(card => card.GetCardTitle() == cardName),
            "RingSide" => player.CardsRingSide.Find(card => card.GetCardTitle() == cardName),
            "Arsenal" => player.CardsArsenal.Find(card => card.GetCardTitle() == cardName),
            "RingArea" => player.CardsRingArea.Find(card => card.GetCardTitle() == cardName),
            _ => throw new CardNotFoundException("The card is not in this deck")
        };

        if (foundCard == null)
            throw new CardNotFoundException("The card is not in this deck");

        return foundCard;
    }

    public List<String> StringCardsFrom(string deck)
    {
        return deck switch
        {
            "Hand" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsHand),
            "RingSide" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingSide),
            "RingArea" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsRingArea),
            "Arsenal" => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsArsenal),
            _ => gameStructureInfo.CardsVisualizor.CreateStringCardList(player.CardsHand)
        };
    }

    public (List<String>, List<CardController>) HandCardsButNotTheCardIsBeingPlayed(CardController cardController)
    {
        List<CardController> cardOptions = player.CardsHand.Where(card => card != cardController).ToList();
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizor.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
    
    public (List<String>, List<CardController>) GetCardsFromRingAreaThatMeetDCriteria(int maximumDamage)
    {
        List<CardController> cardOptions = player.CardsRingArea
            .Where(card => card.GetDamageProducedByTheCard() <= maximumDamage)
            .ToList();
        
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizor.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
    
    public int NumberOfCardsInRingAreaWithTheWord(string word)
    {
        int counter = 0;
        foreach (var card in player.CardsRingArea)
        {   
            if (DoesTheManeuverCardHaveTheWord(card, word))
                counter += 1;
        }
        return counter;
    }
    
    private bool DoesTheManeuverCardHaveTheWord(CardController card, string word)
    {
        return card.GetCardTitle().ToLower().Split(' ').Contains(word.ToLower()) && card.ContainType("Maneuver");
    }
}