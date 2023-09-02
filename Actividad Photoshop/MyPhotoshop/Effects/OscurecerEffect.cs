using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;


public class OscurecerEffect: AbstractEffect 
{
    public override string Description => "Cambia la foto para tener un tono más oscuro.";
    
    public override List<byte> ValorActualizado(Image<Rgb24> originalImage, int x, int y)
    {
        
        int r = originalImage[x, y].R;
        int g = originalImage[x, y].G;
        int b = originalImage[x, y].B;
        Byte newr = (Byte)(Math.Max(r-20, 0));
        Byte newg = (Byte)(Math.Max(g-20,0));
        Byte newb = (Byte)(Math.Max(b-20,0));
        
        return new List<byte>(){newr, newg, newb};
    }
}