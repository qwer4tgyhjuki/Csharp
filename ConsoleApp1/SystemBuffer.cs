using System.Buffers;

class SystemBuffer
{
    static void Main()
    {
        using (var memoryPool = MemoryPool<byte>.Shared)
        {
            var bufferOwner = memoryPool.Rent(1024);

            try
            {
                var array = bufferOwner.Memory.ToArray();
                Console.WriteLine($"Length of the array: {array.Length}");
            }
            finally
            {
                bufferOwner.Dispose();
            }
        }
    }
}
