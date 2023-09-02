using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;


public class OscurecerEffect:IPhotoEffect
{
    private readonly string _description = "Cambia la foto para tener un tono más oscuro.";
    
    public string Description
    {
        get { return _description; }
    }
    
    public Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> oscuraImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int r = originalImage[x, y].R;
                int g = originalImage[x, y].G;
                int b = originalImage[x, y].B;
                Byte newr = (Byte)(Math.Max(r-20, 0));
                Byte newg = (Byte)(Math.Max(g-20,0));
                Byte newb = (Byte)(Math.Max(b-20,0));
                oscuraImage[x, y] = new Rgb24(newr, newg,newb);
            }   
        }

        return oscuraImage;
    }
}