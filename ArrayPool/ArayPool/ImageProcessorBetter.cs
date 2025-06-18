using System;
using System.Diagnostics;
using System.Buffers;

public class ImageProcessorBetter
{
    private const int IMAGE_WIDTH = 800;
    private const int IMAGE_HEIGHT = 600;
    private const int TOTAL_IMAGES = 500;
    
    public static void ProcessImages()
    {
        Console.WriteLine("Iniciando processamento de imagens (versão trivial)...");
        
        var stopwatch = Stopwatch.StartNew();
        int processedCount = 0;
        
        for (int imageIndex = 0; imageIndex < TOTAL_IMAGES; imageIndex++)
        {
            // Gera uma imagem sintética
            PixelRGB[] originalImage = GenerateSyntheticImage(imageIndex);
            
            // Aplica filtro blur (cria novo array a cada operação)
            PixelRGB[] blurredImage = ApplyBlurFilter(originalImage);
            
            // Simula salvamento
            SaveImage(blurredImage, $"processed_{imageIndex}.jpg");
            processedCount++;
            
            if (imageIndex % 50 == 0)
            {
                Console.WriteLine($"Processadas {imageIndex} imagens...");
            }
        }
        
        stopwatch.Stop();
        
        Console.WriteLine($"Processamento concluído!");
        Console.WriteLine($"Imagens processadas: {processedCount}");
        Console.WriteLine($"Tempo total: {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Tempo médio por imagem: {stopwatch.ElapsedMilliseconds / (double)processedCount:F2} ms");
    }
    
    private static PixelRGB[] GenerateSyntheticImage(int seed)
    {
        var image = ArrayPool<PixelRGB>.Shared.Rent(IMAGE_HEIGHT * IMAGE_WIDTH);
        var random = new Random(seed);

        try
        {

            for (int y = 0; y < IMAGE_HEIGHT; y++)
            {
                for (int x = 0; x < IMAGE_WIDTH; x++)
                {
                    image[y * IMAGE_WIDTH + x] = new PixelRGB(
                        (byte)random.Next(256),
                        (byte)random.Next(256),
                        (byte)random.Next(256)
                    );
                }
            }
        }
        finally
        {
            ArrayPool<PixelRGB>.Shared.Return(image);
        }
        
        return image;
    }
    
    private static PixelRGB[] ApplyBlurFilter(PixelRGB[] original)
    {   
        var blurred = ArrayPool<PixelRGB>.Shared.Rent(IMAGE_HEIGHT * IMAGE_WIDTH);

        try
        {

            for (int y = 0; y < IMAGE_HEIGHT - 1; y++)
            {
                for (int x = 0; x < IMAGE_WIDTH - 1; x++)
                {
                    blurred[y * IMAGE_WIDTH + x] = PixelRGB.Average(
                        original[y * IMAGE_WIDTH + x],
                        original[y * IMAGE_WIDTH + (x + 1)],
                        original[(y + 1) * IMAGE_WIDTH + x],
                        original[(y + 1) * IMAGE_WIDTH + (x + 1)]
                    );
                }
            }
        }
        finally
        {
            ArrayPool<PixelRGB>.Shared.Return(blurred);
        }
        
        return blurred;
    }
    
    private static void SaveImage(PixelRGB[] image, string filename)
    {
        // Simula salvamento - na prática salvaria em disco
        // Para o exercício, apenas dar print
    }
}