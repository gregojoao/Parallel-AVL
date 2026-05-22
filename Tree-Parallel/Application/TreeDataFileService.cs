using System;
using System.Collections.Generic;
using System.IO;

namespace Tree_Parallel.Application
{
    public class TreeDataFileService
    {
        public void CreateOrderedFile(string path, int count)
        {
            using StreamWriter writer = new StreamWriter(path);

            for (long value = 1; value <= count; value++)
            {
                writer.WriteLine(value.ToString());
            }
        }

        public void CreateRandomFile(string path, int count)
        {
            using StreamWriter writer = new StreamWriter(path);
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                long value = random.Next(0, 1000000);
                value *= random.Next(0, 150);

                writer.WriteLine(value.ToString());
            }
        }

        public IEnumerable<long> ReadValues(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Arquivo de dados da árvore não encontrado.", path);

            foreach (string line in File.ReadLines(path))
            {
                if (long.TryParse(line, out long value))
                    yield return value;
            }
        }
    }
}
