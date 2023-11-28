using System.Text.Json;
using RawDeal.CardClasses.Action;
using RawDeal.CardClasses.Hibrid;
using RawDeal.CardClasses.Maneuver;
using RawDeal.CardClasses.Reversal;
using RawDeal.CardClasses.UnspecifiedType;
using RawDeal.Exceptions;
using RawDeal.GameClasses;

namespace RawDeal.CardClasses;

public class CardGenerator
{
    public List<CardJson> DeserializeJsonCards()
    {
        var myJson = File.ReadAllText(Path.Combine("data", "cards.json"));
        var cards = JsonSerializer.Deserialize<List<CardJson>>(myJson);
        return cards;
    }

    public List<CardController> CreateDifferentTypesOfCard(string playerString, List<CardJson> totalCards,
        GameStructureInfo gameStructureInfo)
    {
        var pathDeck = Path.Combine(playerString);
        var lines = File.ReadAllLines(pathDeck);

        var matchingCards =
            (from line in lines
                from card in totalCards
                where line.Trim() == card.Title
                select FindCard(card.Title, card)).ToList();
        var cardControllers = matchingCards.Select(card => new CardController(card, gameStructureInfo)).ToList();

        return cardControllers;
    }

    private Card? FindCard(string cardName, CardJson card)
    {
        switch (cardName)
        {
            case "Abdominal Stretch":
                return new AbdominalStretch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Ankle Lock":
                return new AnkleLock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Arm Bar":
                return new ArmBar(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Arm Bar Takedown":
                return new ArmBarTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Arm Drag":
                return new ArmDrag(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Atomic Drop":
                return new AtomicDrop(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Atomic Facebuster":
                return new AtomicFacebuster(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Austin Elbow Smash":
                return new AustinElbowSmash(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Ayatollah of Rock 'n' Roll-a":
                return new AyatollahOfRocknRoll(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Back Body Drop":
                return new BackBodyDrop(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Back Breaker":
                return new BackBreaker(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Bear Hug":
                return new BearHug(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Belly to Back Suplex":
                return new BellyToBackSuplex(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Belly to Belly Suplex":
                return new BellyToBellySuplex(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Big Boot":
                return new BigBoot(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Body Slam":
                return new BodySlam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Boston Crab":
                return new BostonCrab(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Bow & Arrow":
                return new BowArrow(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Break the Hold":
                return new BreakTheHold(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Bulldog":
                return new Bulldog(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Camel Clutch":
                return new CamelClutch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Chair Shot":
                return new ChairShot(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Chicken Wing":
                return new ChickenWing(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Chin Lock":
                return new ChinLock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Choke Hold":
                return new ChokeHold(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Chop":
                return new Chop(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Chyna Interferes":
                return new ChynaInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Clean Break":
                return new CleanBreak(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Clothesline":
                return new Clothesline(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Cobra Clutch":
                return new CobraClutch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Collar & Elbow Lockup":
                return new CollarElbowLockup(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Comeback!":
                return new Comeback(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Cross Body Block":
                return new CrossBodyBlock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "DDT":
                return new DDT(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Deluding Yourself":
                return new DeludingYourself(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Discus Punch":
                return new DiscusPunch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Disqualification!":
                return new Disqualification(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Distract the Ref":
                return new DistractTheRef(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Diversion":
                return new Diversion(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Don't Think Too Hard":
                return new DontThinkTooHard(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Don't You Never... EVER!":
                return new DontYouNeverEver(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Double Arm DDT":
                return new DoubleArmDDT(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Double Digits":
                return new DoubleDigits(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Double Leg Takedown":
                return new DoubleLegTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Drop Kick":
                return new DropKick(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Ego Boost":
                return new EgoBoost(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Elbow to the Face":
                return new ElbowToTheFace(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Ensugiri":
                return new Ensugiri(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Escape Move":
                return new EscapeMove(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Facebuster":
                return new Facebuster(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Figure Four Leg Lock":
                return new FigureFourLegLock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Fireman's Carry":
                return new FiremansCarry(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Fisherman's Suplex":
                return new FishermansSuplex(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Flash in the Pan":
                return new FlashInThePan(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Full Nelson":
                return new FullNelson(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Get Crowd Support":
                return new GetCrowdSupport(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Guillotine Stretch":
                return new GuillotineStretch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Gut Buster":
                return new GutBuster(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Have a Nice Day!":
                return new HaveANiceDay(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Haymaker":
                return new Haymaker(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Head Butt":
                return new HeadButt(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Headlock Takedown":
                return new HeadlockTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Hellfire & Brimstone":
                return new HellfireBrimstone(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Hip Toss":
                return new HipoToss(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Hmmm":
                return new Hmmm(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Hurricanrana":
                return new Hurricanrana(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "I Am the Game":
                return new IAmTheGame(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Inverse Atomic Drop":
                return new InverseAtomicDrop(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Irish Whip":
                return new IrishWhip(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Jockeying for Position":
                return new JockeyingForPosition(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Kane's Chokeslam":
                return new KanesChokeslam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Kane's Flying Clothesline":
                return new KanesFlyingClothesline(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Kane's Return!":
                return new KanesReturn(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Kane's Tombstone Piledriver":
                return new KanesTombstonePiledriver(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Kick":
                return new Kick(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Knee to the Gut":
                return new KneeToTheGut(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Leaping Knee to the Face":
                return new LeapingKneeToTheFace(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Lionsault":
                return new Lionsault(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Lou Thesz Press":
                return new LouTheszPress(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Maintain Hold":
                return new MaintainHold(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Manager Interferes":
                return new ManagerInterferes(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Mandible Claw":
                return new MandibleClaw(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Marking Out":
                return new MarkingOut(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Mr. Socko":
                return new MrSocko(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "No Chance in Hell":
                return new NoChanceInHell(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Not Yet":
                return new NotYet(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Offer Handshake":
                return new OfferHandshake(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Open Up a Can of Whoop-A%$":
                return new OpenUpACanOgWhoopA(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Pat & Gerry":
                return new PatGerry(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Pedigree":
                return new Pedigree(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Power Slam":
                return new PowerSlam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Power of Darkness":
                return new PowerOfDarkness(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Powerbomb":
                return new Powerbomb(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Press Slam":
                return new PressSlam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Pump Handle Slam":
                return new PumpHandleSlam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Punch":
                return new Punch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Puppies! Puppies!":
                return new PuppiesPuppies(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Recovery":
                return new Recovery(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Reverse DDT":
                return new ReverseDDT(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Rock Bottom":
                return new RockBottom(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Roll Out of the Ring":
                return new RollOutOfTheRing(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Rolling Takedown":
                return new RollingTakedown(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Roundhouse Punch":
                return new RoundhousePunch(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Running Elbow Smash":
                return new RunningElbowSmash(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Russian Leg Sweep":
                return new RussianLegSweep(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Samoan Drop":
                return new SamoanDrop(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Shake It Off":
                return new ShakeItOff(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Shane O'Mac":
                return new ShaneOMac(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Shoulder Block":
                return new ShoulderBlock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Sit Out Powerbomb":
                return new SitOutPowerbomb(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Sleeper":
                return new Sleeper(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Smackdown Hotel":
                return new SmackdownHotel(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Snap Mare":
                return new SnapMare(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Spear":
                return new Spear(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Spinning Heel Kick":
                return new SpinningHeelKick(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Spit At Opponent":
                return new SpitAtOpponent(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Stagger":
                return new Stagger(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Standing Side Headlock":
                return new StandingSideHeadlock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Step Aside":
                return new StepAside(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Step Over Toe Hold":
                return new StepOverToeHold(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Stone Cold Stunner":
                return new StoneColdStunner(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Superkick":
                return new Superkick(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "The People's Elbow":
                return new ThePeoplesElbow(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue, card.CardEffect);
            case "The People's Eyebrow":
                return new ThePeoplesEyebrow(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue, card.CardEffect);
            case "Torture Rack":
                return new TortureRack(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue, card.CardEffect);
            case
                "Take That Move, Shine It Up Real Nice, Turn That Sumb*tch Sideways, and Stick It Straight Up Your Roody Poo Candy A%$!"
                :
                return new TakeThatMoveShineItUpRealNice(card.Title,
                    card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue, card.CardEffect);
            case "Tree of Woe":
                return new TreeOfWoe(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Undertaker Sits Up!":
                return new UndertakerSitsUp(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Undertaker's Chokeslam":
                return new UndertakersChokeslam(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Undertaker's Flying Clothesline":
                return new UndertakersFlyingClothesline(card.Title, card.Types, card.Subtypes, card.Fortitude,
                    card.Damage, card.StunValue,
                    card.CardEffect);
            case "Undertaker's Tombstone Piledriver":
                return new UndertakersTombstonePiledriver(card.Title, card.Types, card.Subtypes, card.Fortitude,
                    card.Damage, card.StunValue,
                    card.CardEffect);
            case "Vertical Suplex":
                return new VerticalSuplex(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "View of Villainy":
                return new ViewOfVillainy(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Walls of Jericho":
                return new WallsOfJericho(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Whaddya Got?":
                return new WhaddyaGot(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage,
                    card.StunValue,
                    card.CardEffect);
            case "Wrist Lock":
                return new WristLock(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            case "Y2J":
                return new Y2J(card.Title, card.Types, card.Subtypes, card.Fortitude, card.Damage, card.StunValue,
                    card.CardEffect);
            default:
                throw new VariableIsNullException("Esta carta no ha sido encontrada");
        }
    }
}

