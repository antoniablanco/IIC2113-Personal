using System.Text.Json;
using RawDealView;

namespace RawDeal.SuperStarClass;

public class CreateSuperStart
{
    public View view;
    
    public List<SuperStarJSON> DeserializeJsonSuperStar()
    {
        string myJson = File.ReadAllText (Path.Combine("data","superstar.json")) ;
        var superstars = JsonSerializer.Deserialize<List<SuperStarJSON>>(myJson) ;
        return superstars;
    }
    
    public SuperStar? CreateSuperStar(string deck, List<SuperStarJSON> totalSuperStars) 
    {
        string firstLineDeck = GetSuperStarName(deck);
        Dictionary<SuperStarJSON, Type> superStarTypes = GetSuperStarTypesDictionary(totalSuperStars);
        
        foreach (var superstar in from super in superStarTypes where firstLineDeck.Contains(super.Key.Name) 
                 select (SuperStar)Activator.CreateInstance(super.Value,super.Key.Name, super.Key.Logo, super.Key.HandSize, super.Key.SuperstarValue, super.Key.SuperstarAbility, view))
        {
            return superstar;
        }

        return null;
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
            if (super.Name == "STONE COLD STEVE AUSTIN")
                superStarTypes.Add(super, typeof(StoneCold));
            else if (super.Name == "THE UNDERTAKER")
                superStarTypes.Add(super, typeof(Undertaker));
            else if (super.Name == "MANKIND")
                superStarTypes.Add(super, typeof(Mankind));
            else if (super.Name == "KANE")
                superStarTypes.Add(super, typeof(Kane));
            else if (super.Name == "HHH")
                superStarTypes.Add(super, typeof(HHH));
            else if (super.Name == "THE ROCK")
                superStarTypes.Add(super, typeof(TheRock));
            else if (super.Name == "CHRIS JERICHO")
                superStarTypes.Add(super, typeof(Jericho));
        }

        return superStarTypes;
    }

}