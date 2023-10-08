using System.Reflection;
using RawDeal.CardClass;
using RawDealView.Formatters;
using RawDealView;
using RawDeal.DecksBehavior;
using RawDeal.PlayerClass;

namespace RawDeal.SuperStarClass;

public abstract class SuperStar
{
    private string _Name;
    private string _Logo;
    private int _HandSize;
    private int _SuperstarValue;
    private string _SuperstarAbility;
    public View _view;
    protected VisualizeCards VisualizeCards = new VisualizeCards();

    public SuperStar(string name, string logo, int handSize, int superstarValue, string superstarAbility, View view)
    {
        _Name = name;
        _Logo = logo;
        _HandSize = handSize;
        _SuperstarValue = superstarValue;
        _SuperstarAbility = superstarAbility;
        _view = view;
    }

    public string Name
    {
        get => _Name;
        set => _Name = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string Logo
    {
        get => _Logo;
        set => _Logo = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int HandSize
    {
        get => _HandSize;
        set => _HandSize = value;
    }
    
    public int SuperstarValue
    {
        get => _SuperstarValue;
        set => _SuperstarValue = value;
    }
    
    public string SuperstarAbility
    {
        get => _SuperstarAbility;
        set => _SuperstarAbility = value ?? throw new ArgumentNullException(nameof(value));
    }

    public virtual bool CanUseSuperAbility(PlayerController currentPlayer)
    {
        return false;
    }

    public virtual void UsingElectiveSuperAbility(GameStructureInfo gameStructureInfo)
    {
        
    }
    
    public virtual void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(GameStructureInfo gameStructureInfo)
    {
        
    }
    protected void DiscardingCardsFromHandToRingSide(GameStructureInfo gameStructureInfo, PlayerController player, int cardsToDiscardCount)
    {
        List<string> handFormatoString = player.StringCardsHand();
        int selectedCard =_view.AskPlayerToSelectACardToDiscard(handFormatoString, player.NameOfSuperStar(), player.NameOfSuperStar(), cardsToDiscardCount);
            
        if (selectedCard != -1)
        {
            CardController discardCardController = player.GetSpecificCardFromHand(selectedCard);
            Player playerHowDiscardCard = (player == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
            
            gameStructureInfo.CardMovement.TransferChoosinCardFromHandToRingSide(playerHowDiscardCard, discardCardController);
        }
    }

    protected void AddingCardFromRingSideToHand(GameStructureInfo gameStructureInfo, PlayerController player)
    {
        List<string> ringSideAsString = player.StringCardsRingSide();
        int selectedCard =_view.AskPlayerToSelectCardsToPutInHisHand(Name, 1, ringSideAsString);
        
        CardController addedCardController = player.GetSpecificCardFromRingSide(selectedCard);
        Player playerHowDiscardCard = (player == gameStructureInfo.ControllerCurrentPlayer)? gameStructureInfo.GetCurrentPlayer(): gameStructureInfo.GetOpponentPlayer();
        gameStructureInfo.CardMovement.TransferChoosinCardFromRingSideToHand(playerHowDiscardCard, addedCardController);
    }
}