using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class BlackAndWhiteEffect: AbstractEffect 
{
    public override string Description => "Cambia la foto a blanco y negro.";

    public override List<byte> ValorActualizado(Image<Rgb24> originalImage, int x, int y)
    {
        
        int r = originalImage[x, y].R;
        int g = originalImage[x, y].G;
        int b = originalImage[x, y].B;
        Byte averageColor = (Byte)((r+g+b) / 3);
        
        return new List<byte>(){averageColor,averageColor,averageColor};
    }
}