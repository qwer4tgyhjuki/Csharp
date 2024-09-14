
using System.IO;
using System.IO.Compression;

class Compress
{
    static void Main()
    {
        string sourceFilePath = "text.txt";
        string compressedFilePath = "text.gz";


        CompressFile(sourceFilePath, compressedFilePath);
    }
    static void CompressFile(string sourceFilePath, string compressedFilePath)
    {
        using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open))
        {
            using (FileStream compressedFileStream = File.Create(compressedFilePath))
            {
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    sourceFileStream.CopyTo(compressionStream);
                }
            }
        }
        Console.WriteLine("File compressed successfully.");
    }
}