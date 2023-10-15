using RawDeal.CardClass;
using RawDeal.DecksBehavior;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.SuperStarClasses;

namespace RawDeal.PlayerClasses;

public class PlayersGenerator
{
    private GameStructureInfo gameStructureInfo;
    private string _deckFolder;
    private CardGenerator _cardGenerator = new CardGenerator();
    private CreateSuperStart _createSuperStart = new CreateSuperStart();
    
    public PlayersGenerator(GameStructureInfo gameStructureInfo, string deckFolder)
    {
        this.gameStructureInfo = gameStructureInfo;
        _deckFolder = deckFolder;
        _createSuperStart.view = gameStructureInfo.view;
        _cardGenerator.gameStructureInfo = gameStructureInfo;
        Create();
    }

    private void Create()
    {
        PlayerController playerUno = CreateOnePlayer();
        PlayerController playerDos = CreateOnePlayer();
        InitializeGameVariables(playerUno, playerDos);
        InitializePlayerHands();
    }
    
    private PlayerController CreateOnePlayer()
    {
        var (totalCards, totalSuperStars) = GetTotalCardsAndSuperStars();
        Player player = InitializePlayer(totalCards, totalSuperStars);
        DeckValidator deckValidator = new DeckValidator(player);
        
        if (!deckValidator.IsValidDeck())
        {
            throw new InvalidDeckException("The Deck Is Not Valid");
        }

        PlayerController playerController = InitializePlayerController(player);
        return playerController;
    }
    
    private (List<CardJson>, List<SuperStarJSON>) GetTotalCardsAndSuperStars() 
    {
        List<CardJson> totalCards = _cardGenerator.DeserializeJsonCards();
        List<SuperStarJSON> totalSuperStars = _createSuperStart.DeserializeJsonSuperStar();
    
        return (totalCards, totalSuperStars);
    }
    
    private Player InitializePlayer(List<CardJson> totalCards, List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = gameStructureInfo.view.AskUserToSelectDeck(_deckFolder);
        List<CardController> playerCardList = _cardGenerator.CreateDiferentTypesOfCard(stringPlayer, totalCards, gameStructureInfo.view);
        SuperStar? superStarPlayer = _createSuperStart.CreateSuperStar(stringPlayer, totalSuperStars);
        
        Player playerReturn = new Player(playerCardList, superStarPlayer);

        SavePlayerInGameStructureInfo(playerReturn);
        
        return playerReturn;
    }

    private void SavePlayerInGameStructureInfo(Player player)
    {
        if (gameStructureInfo.playerOne == null)
            gameStructureInfo.playerOne = player;
        else
            gameStructureInfo.playerTwo = player;
    }

    private PlayerController InitializePlayerController(Player player)
    {
        PlayerController playerController = new PlayerController(player, gameStructureInfo);
        return playerController;
    }
    
    private void InitializeGameVariables(PlayerController playerOne, PlayerController playerTwo) 
    {
        gameStructureInfo.ControllerPlayerOne = playerOne;
        gameStructureInfo.ControllerPlayerTwo = playerTwo;
        
        gameStructureInfo.GetSetGameVariables.CreatePlayerInitialOrder();
    }

    private void InitializePlayerHands()
    {
        gameStructureInfo.GameLogic.ThePlayerDrawTheirInitialsHands();
    }
}