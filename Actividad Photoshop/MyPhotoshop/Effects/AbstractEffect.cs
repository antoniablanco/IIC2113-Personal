using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public abstract class AbstractEffect: IPhotoEffect
{
    public abstract string Description { get; }
    
    public virtual Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> newImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                List<byte>averageColor = ValorActualizado(originalImage, x, y);
                newImage[x, y] = new Rgb24(averageColor[0],averageColor[1],averageColor[2]);
            }   
        }

        return newImage;
    }

    public virtual List<byte> ValorActualizado(Image<Rgb24> originalImage, int x, int y)
    {
        
        int r = originalImage[x, y].R;
        int g = originalImage[x, y].G;
        int b = originalImage[x, y].B;
        
        return new List<byte>(){(byte) r, (byte) g,(byte) b};
    }
}