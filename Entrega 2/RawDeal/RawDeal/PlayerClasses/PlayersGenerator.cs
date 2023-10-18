using RawDeal.CardClasses;
using RawDeal.DecksBehavior;
using RawDeal.Exceptions;
using RawDeal.GameClasses;
using RawDeal.SuperStarClasses;

namespace RawDeal.PlayerClasses;

public class PlayersGenerator
{
    private CardGenerator _cardGenerator = new CardGenerator();
    private string _deckFolder;
    private SuperStartGenerator _superStartGenerator = new SuperStartGenerator();
    private GameStructureInfo gameStructureInfo;

    public PlayersGenerator(GameStructureInfo gameStructureInfo, string deckFolder)
    {
        this.gameStructureInfo = gameStructureInfo;
        _deckFolder = deckFolder;
        CreatePlayers();
    }

    private void CreatePlayers()
    {   
        var (totalCards, totalSuperStars) = GetTotalCardsAndSuperStars();
        PlayerController playerUno = CreateOnePlayer(totalCards, totalSuperStars);
        PlayerController playerDos = CreateOnePlayer(totalCards, totalSuperStars);
        InitializeGameVariables(playerUno, playerDos);
        InitializePlayerHands();
    }

    private (List<CardJson>, List<SuperStarJSON>) GetTotalCardsAndSuperStars() 
    {
        List<CardJson> totalCards = _cardGenerator.DeserializeJsonCards();
        List<SuperStarJSON> totalSuperStars = _superStartGenerator.DeserializeJsonSuperStar();
    
        return (totalCards, totalSuperStars);
    }

    private PlayerController CreateOnePlayer(List<CardJson> totalCards, List<SuperStarJSON> totalSuperStars)
    {
        Player player = InitializePlayer(totalCards, totalSuperStars);
        DeckValidator deckValidator = new DeckValidator(player);
        
        if (!deckValidator.IsValidDeck())
            throw new InvalidDeckException("The Deck Is Not Valid");

        return InitializePlayerController(player);
    }

    private Player InitializePlayer(List<CardJson> totalCards, List<SuperStarJSON> totalSuperStars) 
    {
        string stringPlayer = gameStructureInfo.View.AskUserToSelectDeck(_deckFolder);
        List<CardController> playerCardList = _cardGenerator.CreateDiferentTypesOfCard(stringPlayer, totalCards, gameStructureInfo);
        SuperStar? superStarPlayer = _superStartGenerator.CreateSuperStar(stringPlayer, totalSuperStars, gameStructureInfo.View);
        
        Player playerReturn = new Player(playerCardList, superStarPlayer);
        SavePlayerInGameStructureInfo(playerReturn);
        
        return playerReturn;
    }

    private void SavePlayerInGameStructureInfo(Player player)
    {   
        if (gameStructureInfo.PlayerOne is null)
            gameStructureInfo.PlayerOne = player;
        else
            gameStructureInfo.PlayerTwo = player;
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
        gameStructureInfo.ControllerPlayerOne.DrawInitialHandCards();
        gameStructureInfo.ControllerPlayerTwo.DrawInitialHandCards();
    }
}