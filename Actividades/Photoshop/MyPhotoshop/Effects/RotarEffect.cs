using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class RotarEffect: AbstractEffect
{
    public override string Description => "Cambia la foto para rotarla en 90°.";
    
    public override Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> rotarImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Rgb24 originalColor = originalImage[x, y];
                rotarImage[width-y-1,x] = originalColor;
            }   
        }

        return rotarImage;
    }
}