using System;
using System.IO;

namespace Homework1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Евгений\Desktop\HW1";

            if (Directory.Exists(path))
            {

                DeleteDirsAndFiles(path);


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
    }
}