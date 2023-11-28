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
        for (var i = 0; i < player.Superstar.HandSize; i++)
            DrawCard();
    }

    public void DrawCard()
    {   
        gameStructureInfo.CardMovement.TransferOfUnselectedCard(player.CardsArsenal, player.CardsHand);
    }

    public string GetNameOfSuperStar()
    {
        return player.Superstar.Name;
    }

    public int GetSuperStarValue()
    {
        return player.Superstar.SuperstarValue;
    }

    public bool HasCardsInArsenal()
    {   
        return (player.CardsArsenal.Count > 0);
    }

    private List<CardController> GetCardsAvailableToPlay()
    {
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + card.GetPlusFortitudeAfterSpecificCard() <= FortitudeRating() 
                           && card.HasAnyTypeDifferentOfReversal() && card.CanThisCardBePlayed())
            .ToList();
    }

    public List<CardController> GetCardsAvailableToReversal(int totalDamage) 
    {   
        return player.CardsHand
            .Where(card => card.GetCardFortitude(card.GetCardTypes()[0]) + 
                gameStructureInfo.BonusManager.GetFortitudeBonus(gameStructureInfo.CardBeingPlayedType) <= FortitudeRating() 
                && card.IsReversalType() && CanReversalPlayedCard(card, "Hand", totalDamage))
            .ToList();
    }

    public List<Tuple<CardController, int>> GetPossibleCardsToPlayWithTheirTypeIndex()
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();

        foreach (var cardController in GetCardsAvailableToPlay())
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) 
                    != "Reversal" && cardController.CanThisCardBePlayed(cardController.GetCardType(index)) &&
                    cardController.GetCardFortitude(cardController.GetCardType(index)) + cardController.GetPlusFortitudeAfterSpecificCard()
                    <= FortitudeRating() && cardController.CanThisCardBePlayed()
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
    }

    public List<Tuple<CardController, int>> GetPossiblesCardsForReversalWithTheirTypeIndex(int totalDamage)
    {   
        List<Tuple<CardController, int>> allTypesForCard = new List<Tuple<CardController, int>>();
        
        foreach (var cardController in GetCardsAvailableToReversal(totalDamage))
        {
            int[] indexes = Enumerable.Range(0, cardController.GetCardTypes().Count()).ToArray();
            allTypesForCard.AddRange(from index in indexes where cardController.GetCardType(index) == "Reversal" 
                select new Tuple<CardController, int>(cardController, index));
        }

        return allTypesForCard;
        
    }
    
    public int FortitudeRating()
    {
        return player.CardsRingArea.Sum(card => card.GetDamageProducedByTheCard());
    }

    private bool CanReversalPlayedCard(CardController card, string reverseBy, int totalDamage)
    {   
        return card.DoesTheCardCanReversalPlayedCard(reverseBy, totalDamage);
    }

    public bool CheckIfSuperStarCanUseSuperAbility(PlayerController currentPlayer)
    {
        return player.Superstar.CanUseSuperAbility(currentPlayer);
    }

    public void UseElectiveSuperAbility()
    {
        player.Superstar.UseElectiveSuperAbility(gameStructureInfo);
    }

    public void UseAutomaticSuperAbility()
    {
        player.Superstar.UseAutomaticSuperAbilityAtTheStartOfTheTurn(gameStructureInfo);
    }

    public void BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn()
    {
        player.Superstar.BlockSuperAbilityBecauseIsJustAtTheStartOfTheTurn(gameStructureInfo);
    }

    public bool HasTheSuperAbilityBeenUsedThisTurn()
    {
        return player.TheAbilityHasBeenUsedThisTurn;
    }

    public void MarkSuperAbilityAsUsedInThisTurn()
    {
        player.TheAbilityHasBeenUsedThisTurn = true;
    }

    public void MarkSuperAbilityAsUnusedInThisTurn()
    {
        player.TheAbilityHasBeenUsedThisTurn = false;
    }

    public int GetNumberOfCardIn(string deck)
    {
        return deck switch
        {
            "Hand" => player.CardsHand.Count(),
            "RingSide" => player.CardsRingSide.Count(),
            "Arsenal" => player.CardsArsenal.Count(),
            _ => throw new CardNotFoundException("Deck Not Implemented")
        };
    }

    public CardController RetrieveCardFromDeckAtPosition(string deck, int index)
    {
        return deck switch
        {
            "Hand" => player.CardsHand[index],
            "RingSide" => player.CardsRingSide[index],
            "Arsenal" => player.CardsArsenal[index],
            "RingArea" => player.CardsRingArea[index],
            _ => throw new CardNotFoundException("Deck Not Implemented")
        };
    }
    
    public CardController GetCardInDeckByName(string deck, string cardName)
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

    public List<String> GetStringCardsFrom(string deck)
    {
        return deck switch
        {
            "Hand" => gameStructureInfo.CardsVisualizer.CreateStringCardList(player.CardsHand),
            "RingSide" => gameStructureInfo.CardsVisualizer.CreateStringCardList(player.CardsRingSide),
            "RingArea" => gameStructureInfo.CardsVisualizer.CreateStringCardList(player.CardsRingArea),
            "Arsenal" => gameStructureInfo.CardsVisualizer.CreateStringCardList(player.CardsArsenal),
            _ => throw new CardNotFoundException("Deck Not Implemented")
        };
    }

    public (List<String>, List<CardController>) GetHandCardsButNotTheCardIsBeingPlayed(CardController cardController)
    {
        List<CardController> cardOptions = player.CardsHand.Where(card => card != cardController).ToList();
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizer.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
    
    public (List<String>, List<CardController>) GetCardsFromRingAreaThatMeetDCriteria(int maximumDamage)
    {
        List<CardController> cardOptions = player.CardsRingArea
            .Where(card => card.GetDamageProducedByTheCard() <= maximumDamage)
            .ToList();
        
        List<String> stringCardOptions = gameStructureInfo.CardsVisualizer.CreateStringCardList(cardOptions);
        return (stringCardOptions, cardOptions);
    }
    
    public int GetNumberOfCardsInRingAreaWithTheWord(string word)
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
        return card.GetCardTitle().ToLower().Split(' ').Contains(word.ToLower()) && card.DoesTheCardContainType("Maneuver");
    }
}