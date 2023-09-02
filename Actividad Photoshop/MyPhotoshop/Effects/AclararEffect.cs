using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;


public class AclararEffect:IPhotoEffect
{
    private readonly string _description = "Cambia la foto para tener un tono más claro";
    
    public string Description
    {
        get { return _description; }
    }
    
    public Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> aclararImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int r = originalImage[x, y].R;
                int g = originalImage[x, y].G;
                int b = originalImage[x, y].B;
                Byte newr = (Byte)(Math.Min(r+20, 255));
                Byte newg = (Byte)(Math.Min(g+20,255));
                Byte newb = (Byte)(Math.Min(b+20,255));
                aclararImage[x, y] = new Rgb24(newr, newg,newb);
            }   
        }

        return aclararImage;
    }
}