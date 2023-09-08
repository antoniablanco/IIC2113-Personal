using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;


public class AclararEffect: AbstractEffect
{
    public override string Description => "Cambia la foto para tener un tono más claro.";
    
    public override List<byte> ValorActualizado(Image<Rgb24> originalImage, int x, int y)
    {
        int r = originalImage[x, y].R;
        int g = originalImage[x, y].G;
        int b = originalImage[x, y].B;
        Byte newr = (Byte)(Math.Min(r+20, 255));
        Byte newg = (Byte)(Math.Min(g+20,255));
        Byte newb = (Byte)(Math.Min(b+20,255));
        
        return new List<byte>(){newr, newg,newb};
    }
}