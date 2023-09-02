using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MyPhotoshop.Effects;

public class MostrarBordesEffect: AbstractEffect 
{
    public override string Description => "Cambia la foto para mostrar los bordes.";
    
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
                
        List<int> sumaTotal = new List<int> { 8 * originalImage[x, y].R, 8 * originalImage[x, y].G, 8 * originalImage[x, y].B };

        for (int i = 1; i < pixelesVecinos.Count; i++)
        {   
            List<int> element = pixelesVecinos[i];
            if (element[0] >= 0 && element[0] < width && element[1] >= 0 && element[1] < height)
            {
                int r = originalImage[element[0], element[1]].R;
                int g = originalImage[element[0], element[1]].G;
                int b = originalImage[element[0], element[1]].B;
                sumaTotal[0] -= r;
                sumaTotal[1] -= g;
                sumaTotal[2] -= b;
            }
        }
                
        byte rByte = (byte) Math.Max(0, Math.Min(255, sumaTotal[0])); 
        byte gByte = (byte) Math.Max(0, Math.Min(255, sumaTotal[1]));
        byte bByte = (byte) Math.Max(0, Math.Min(255, sumaTotal[2]));
        
        return new List<byte>(){rByte,gByte,bByte};
    }
}