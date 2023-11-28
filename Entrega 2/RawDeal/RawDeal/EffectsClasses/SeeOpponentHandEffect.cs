using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class SeeOpponentHandEffect: EffectsUtils
{
    private PlayerController currentPlayerController;
    private PlayerController opponentPlayerController;
    
    
    public SeeOpponentHandEffect(PlayerController currentPlayerController,  PlayerController opponentPlayerController,  
        GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.currentPlayerController = currentPlayerController;
        this.opponentPlayerController = opponentPlayerController;
        
        Apply();
    }
    
    private void Apply()
    {
        gameStructureInfo.View.SayThatPlayerLooksAtHisOpponentsHand(currentPlayerController.GetNameOfSuperStar(),
            opponentPlayerController.GetNameOfSuperStar());
        List<string> stringCardSet = opponentPlayerController.GetStringCardsFrom("Hand");
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
}