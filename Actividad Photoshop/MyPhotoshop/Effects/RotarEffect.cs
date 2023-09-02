using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class RotarEffect:IPhotoEffect
{
    private readonly string _description = "Cambia la foto para rotarla en 90°.";
    
    public string Description
    {
        get { return _description; }
    }

    public Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> rotarImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int r = originalImage[x, y].R;
                int g = originalImage[x, y].G;
                int b = originalImage[x, y].B;
                byte rByte = (byte)r;
                byte gByte = (byte)g;
                byte bByte = (byte)b;
                rotarImage[width-y-1,x] = new Rgb24(rByte,gByte,bByte);
            }   
        }

        return rotarImage;
    }
}