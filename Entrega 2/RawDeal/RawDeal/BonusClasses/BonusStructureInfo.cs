using RawDeal.CardClasses;
using RawDeal.PlayerClasses;

namespace RawDeal.GameClasses;

public class BonusStructureInfo
{   
    // Bonus que afectan a la proxima carta jugada
    public bool IsJockeyingForPositionBonusDamageActive = false;
    public bool IsJockeyingForPositionBonusFortitudeActive = false;
    public bool IsIrishWhipBonusActive = false;
    public bool ClotheslineBonusActive = false;
    public bool AtomicDropBonusActive = false;
    public bool SnapMareBonusActive = false;
    public bool GetCrowdSupportBonusActive = false;
    public bool OpenUpaCanOfWhoopAssBonusActive = false;
    public bool SmackdownHotelBonusActive = false;
    
    public int BonusDamage = 4;
    public int BonusFortitude = 8;
    public int turnsLeftForBonusCounter = 0;
    public PlayerController WhoActivateNextPlayedCardBonusEffect;
    
    // Bonus que afectan dentro del mismo turno
    public int IAmTheGameBonus = 0;
    public int HaymakerBonus = 0;
    public int SuperkickBonus = 0;
    public int MrSockoBonus = 1;
    public int PowerofDarknessDamageBonus = 0;
    public int PowerofDarknessFortitudBonus = 0;
    public int DontYouNeverEVERBonus = 0;
    public int UndertakerSitsUpDamageBonus = 0;
    public int UndertakerSitsUpFortitudBonus = 0;
    public int KanesReturnDamageBonus = 0;
    public int KanesReturnFortitudBonus = 0;
    
    // Bonus para reversar cartas
    public bool DiversionBonusActive = false;
    public bool StaggerBonusActive = false;
    public bool AyatollahBonusActive = false;
    
}