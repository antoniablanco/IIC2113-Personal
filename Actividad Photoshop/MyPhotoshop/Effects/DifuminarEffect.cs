using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class DifuminarEffect: AbstractEffect 
{
    public override string Description => "Cambia la foto para difuminarla.";
    
    public override List<byte> ValorActualizado(Image<Rgb24> originalImage, int x, int y)
    {
        int width = originalImage.Width;
        int height = originalImage.Height;
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
        
        return new List<byte>(){rByte,gByte,bByte};
    }
}