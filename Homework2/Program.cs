using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Program Files";

            if (Directory.Exists(path))
            {
                Console.WriteLine(GetSize(path).ToString("N0") + " байт");
            }
        }

        static long GetSize(string path)
        {
            long size = 0;
            try
            {

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    size += file.Length;
                }

                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    size += GetSize(dir);
                }
            }
            catch (Exception expection)
            {
                Console.WriteLine($"Файлы не учтены: {expection.Message}");
            }
            return size;

        }
    }
}
