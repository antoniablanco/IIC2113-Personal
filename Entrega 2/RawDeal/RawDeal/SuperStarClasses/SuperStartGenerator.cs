using System.Text.Json;
using RawDealView;

namespace RawDeal.SuperStarClasses;

public class SuperStartGenerator
{
    public List<SuperStarJSON> DeserializeJsonSuperStar()
    {
        string myJson = File.ReadAllText (Path.Combine("data","superstar.json")) ;
        var superstars = JsonSerializer.Deserialize<List<SuperStarJSON>>(myJson) ;
        return superstars;
    }
    
    public SuperStar? CreateSuperStar(string deck, List<SuperStarJSON> totalSuperStars, View view) 
    {
        string firstLineDeck = GetSuperStarName(deck);
        Dictionary<SuperStarJSON, Type> superStarTypes = GetSuperStarTypesDictionary(totalSuperStars);
        
        foreach (var superstar in from super in superStarTypes where firstLineDeck.Contains(super.Key.Name) 
                 select (SuperStar)Activator.CreateInstance(super.Value,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility, view))
            return superstar;

        throw new InvalidOperationException("SuperStar especifico no encontrado.");

    }

    private string GetSuperStarName(string deck)
    {
        string pathDeck = Path.Combine($"{deck}");
        string[] lines = File.ReadAllLines(pathDeck);
        return lines[0];
    }

    private Dictionary<SuperStarJSON, Type> GetSuperStarTypesDictionary(List<SuperStarJSON> totalSuperStars)
    {
        Dictionary<SuperStarJSON, Type> superStarTypes = new Dictionary<SuperStarJSON, Type>();
        foreach (var super in totalSuperStars)
        {
            switch (super.Name)
            {
                case "STONE COLD STEVE AUSTIN":
                    superStarTypes.Add(super, typeof(StoneCold));
                    break;
                case "THE UNDERTAKER":
                    superStarTypes.Add(super, typeof(Undertaker));
                    break;
                case "MANKIND":
                    superStarTypes.Add(super, typeof(Mankind));
                    break;
                case "KANE":
                    superStarTypes.Add(super, typeof(Kane));
                    break;
                case "HHH":
                    superStarTypes.Add(super, typeof(HHH));
                    break;
                case "THE ROCK":
                    superStarTypes.Add(super, typeof(TheRock));
                    break;
                case "CHRIS JERICHO":
                    superStarTypes.Add(super, typeof(Jericho));
                    break;
            }
        }

        return superStarTypes;
    }

}