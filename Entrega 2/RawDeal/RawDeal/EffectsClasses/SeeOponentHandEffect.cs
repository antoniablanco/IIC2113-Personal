using RawDeal.GameClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.EffectsClasses;

public class SeeOponentHandEffect: EffectsUtils
{
    private PlayerController currentPlayerController;
    private PlayerController opponentPlayerController;
    
    
    public SeeOponentHandEffect(PlayerController currentPlayerController,  PlayerController opponentPlayerController,  
        GameStructureInfo gameStructureInfo)
        : base(gameStructureInfo)
    {
        this.currentPlayerController = currentPlayerController;
        this.opponentPlayerController = opponentPlayerController;
        
        Apply();
    }
    
    private void Apply()
    {
        gameStructureInfo.View.SayThatPlayerLooksAtHisOpponentsHand(currentPlayerController.NameOfSuperStar(),
            opponentPlayerController.NameOfSuperStar());
        List<string> stringCardSet = opponentPlayerController.StringCardsFrom("Hand");
        gameStructureInfo.View.ShowCards(stringCardSet);
    }
}