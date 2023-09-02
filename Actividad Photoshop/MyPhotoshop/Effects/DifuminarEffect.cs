using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class DifuminarEffect:IPhotoEffect
{
    private readonly string _description = "Cambia la foto para difuminarla.";
    
    public string Description
    {
        get { return _description; }
    }

    public Image<Rgb24> Apply(Image<Rgb24> originalImage)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
        Image<Rgb24> difuminarImage = new Image<Rgb24>(width, height); 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                List<List<int>> pixelesVecinos = new List<List<int>>();
                pixelesVecinos.Add(new List<int> { x, y });
                pixelesVecinos.Add(new List<int>{x-1,y-1});
                pixelesVecinos.Add(new List<int>{x,y-1});
                pixelesVecinos.Add(new List<int>{x+1,y-1});
                pixelesVecinos.Add(new List<int>{x+1,y});
                pixelesVecinos.Add(new List<int>{x+1,y+1});
                pixelesVecinos.Add(new List<int>{x,y+1});
                pixelesVecinos.Add(new List<int>{x-1,y+1});
                pixelesVecinos.Add(new List<int>{x-1,y});
                
                List<int> sumaTotal = new List<int> { 0, 0, 0 };
                
                foreach (List<int> element in pixelesVecinos)
                {   
                    if (element[0] <= -1 || element[1] <= -1 || element[0] >= width || element[1] >= height)
                    {   
                        sumaTotal[0] += 0;
                        sumaTotal[1] += 0;
                        sumaTotal[2] += 0;
                    }
                    else
                    {
                        int r = originalImage[element[0], element[1]].R;
                        int g = originalImage[element[0], element[1]].G;
                        int b = originalImage[element[0], element[1]].B;
                        sumaTotal[0] += r;
                        sumaTotal[1] += g;
                        sumaTotal[2] += b;
                    }
                }
                
                byte rByte = (byte)(sumaTotal[0] / 9);
                byte gByte = (byte)(sumaTotal[1] / 9);
                byte bByte = (byte)(sumaTotal[2] / 9);
                difuminarImage[x, y] = new Rgb24(rByte,gByte,bByte);
            }   
        }

        return difuminarImage;
    }
}