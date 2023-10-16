using RawDeal.CardClass;
using RawDealView.Formatters;

namespace RawDeal.PlayerClasses;

public class PlayInfoImplementation : IViewablePlayInfo
{
    public IViewableCardInfo CardInfo { get; set; }

    public string PlayedAs { get; set; }
    
    public PlayInfoImplementation(string title, string fortitude, string damage, string stunValue, List<string> types, List<string> subtypes, string cardEffect, string playedAs)
    {
        CardInfo = new CardInfoImplementation(title, fortitude, damage, stunValue, types, subtypes, cardEffect); 
        PlayedAs = playedAs;
    }
}