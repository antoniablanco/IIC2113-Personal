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
    protected VisualisarCartas visualisarCartas = new VisualisarCartas();

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

    public virtual bool CanUseSuperAbility(Player currentPlayer)
    {
        return false;
    }

    public virtual void UsingElectiveSuperAbility(Player currentPlayer, Player opponentPlayer)
    {
        
    }
    
    public virtual void UsingAutomaticSuperAbilityAtTheStartOfTheTurn(Player currentPlayer, Player opponentPlayer)
    {
        
    }

    protected void DiscardingCardsFromHandToRingSide(Player player, int cardsToDiscardCoun)
    {
        List<string> handFormatoString = visualisarCartas.CreateStringCardList(player.cardsHand);
        int selectedCard =_view.AskPlayerToSelectACardToDiscard(handFormatoString, player.NameOfSuperStar(), player.NameOfSuperStar(), cardsToDiscardCoun);
            
        if (selectedCard != -1)
        {
            Card discardCard = player.cardsHand[selectedCard];
            player.CardTransferChoosingWhichOneToChange(discardCard, player.cardsHand, player.cardsRingSide);
        }
    }

    protected void AddingCardFromRingSideToHand(Player player)
    {
        List<string> ringSideAsString = visualisarCartas.CreateStringCardList(player.cardsRingSide);
        int selectedCard =_view.AskPlayerToSelectCardsToPutInHisHand(Name, 1, ringSideAsString);
        
        Card addedCard = player.cardsRingSide[selectedCard];
        player.CardTransferChoosingWhichOneToChange(addedCard, player.cardsRingSide, player.cardsHand);
    
    }
}