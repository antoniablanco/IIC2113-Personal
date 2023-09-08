using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class ReflejarEffect: AbstractEffect
{
    public override string Description => "Cambia la foto para reflejarla con respecto al eje y.";

    public override Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> reflejarImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Rgb24 originalColor = originalImage[x, y];
                reflejarImage[width - x - 1, y] = originalColor;
            }  
        }
        return reflejarImage;
    }
}