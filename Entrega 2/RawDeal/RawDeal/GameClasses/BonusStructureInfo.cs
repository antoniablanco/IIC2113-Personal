using RawDeal.CardClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.GameClasses;

public class BonusStructureInfo
{
    public CardController BonusCardActivator;
    public bool SuperkickDobleActivada = false;
    
    public bool IsJockeyingForPositionBonusDamageActive = false;
    public bool IsJockeyingForPositionBonusFortitudActive = false;
    public bool IsIrishWhipBonusActive = false;
    public bool SuperkickBonusActive = false;
    public bool ClotheslineBonusActive = false;
    public bool AtomicDropBonusActive = false;
    public bool SnapMareBonusActive = false;
    
    public int IAmTheGameBonus = 0;
    public int HaymakerBonus = 0;
    
    public int BonusDamage = 4;
    public int BonusFortitude = 8;
    public int turnsLeftForBonusCounter = 0;
    public PlayerController WhoActivateNextPlayedCardBonusEffect;
}