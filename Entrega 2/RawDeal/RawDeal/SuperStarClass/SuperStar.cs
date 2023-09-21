using System.Reflection;
using RawDealView.Formatters;
using RawDealView;

namespace RawDeal.SuperStarClases;

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

    public virtual void UsingElectiveSuperAbility(PlayerController currentPlayer, PlayerController opponentPlayer)
    {
        
    }
    
    public virtual void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(PlayerController currentPlayer, PlayerController opponentPlayer)
    {
        
    }

    protected void DiscardingCardsFromHandToRingSide(PlayerController player, int cardsToDiscardCount)
    {
        List<string> handFormatoString = player.StringCardsHand();
        int selectedCard =_view.AskPlayerToSelectACardToDiscard(handFormatoString, player.NameOfSuperStar(), player.NameOfSuperStar(), cardsToDiscardCount);
            
        if (selectedCard != -1)
        {
            Card discardCard = player.GetSpecificCardFromHand(selectedCard);
            player.TransferChoosinCardFromHandToRingSide(discardCard);
        }
    }

    protected void AddingCardFromRingSideToHand(PlayerController player)
    {
        List<string> ringSideAsString = player.StringCardsRingSide();
        int selectedCard =_view.AskPlayerToSelectCardsToPutInHisHand(Name, 1, ringSideAsString);
        
        Card addedCard = player.GetSpecificCardFromRingSide(selectedCard);
        player.TransferChoosinCardFromRingSideToHand(addedCard);
    
    }
}