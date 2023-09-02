using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class ReflejarEffect:IPhotoEffect
{
    private readonly string _description = "Cambia la foto para reflejarla con respecto al eje y.";
    
    public string Description
    {
        get { return _description; }
    }

    public Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> reflejarImage = new Image<Rgb24>(width, height); 
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
                reflejarImage[width-x-1, y] = new Rgb24(rByte,gByte,bByte);
            }   
        }

        return reflejarImage;
    }
}