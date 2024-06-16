using System;
using System.IO;

namespace homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Евгений\Desktop\HW1";

            if (Directory.Exists(path))
            {
                long start_size = GetSize(path);
                Console.WriteLine("Исходный размер папки " + start_size + " байт");
                DeleteDirsAndFiles(path);
                long current_size = GetSize(path);
                Console.WriteLine("Освобождено " + (start_size - current_size) + " байт");
                Console.WriteLine("Текущий размер папки " + current_size + " байт");


            }
            else
            {
                Console.WriteLine("Все папки и файлы, не изменявшиеся более 30 минут удалены");
            }

        }

        static void DeleteDirsAndFiles(string path)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {

                var time = Directory.GetLastWriteTime(dir);
                try
                {
                    if ((DateTime.Now - time) > TimeSpan.FromMinutes(30))
                    {
                        Directory.Delete(dir, true);
                    }
                    else
                    {
                        DeleteDirsAndFiles(dir);
                    }
                }
                catch (IOException exeption)
                {
                    Console.WriteLine($"Для папки {dir} ошибка {exeption.Message}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }


            }

            foreach (var file in Directory.GetFiles(path))
            {

                var time = Directory.GetLastWriteTime(file);
                try
                {
                    if ((DateTime.Now - time) > TimeSpan.FromMinutes(30))
                    {
                        File.Delete(file);
                    }
                }
                catch (IOException exeption)
                {
                    Console.WriteLine($"Для папки {file} ошибка {exeption.Message}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }


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