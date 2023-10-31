using RawDeal.PlayerClasses;

namespace RawDeal.GameClasses;

public class BonusStructureInfo
{
    public bool IsJockeyingForPositionBonusDamageActive = false;
    public bool IsJockeyingForPositionBonusFortitudActive = false;
    public bool IsIrishWhipBonusActive = false;
    
    public int BonusDamage = 4;
    public int BonusFortitude = 8;
    public int turnsLeftForBonusCounter = 0;
    public PlayerController WhoActivateNextPlayedCardBonusEffect;
    
}