using homework4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к бинарному файлу");
            string filePath = args[0];
            List<Student> students = ReadStudentsFromBinFile(filePath);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string outputDirectoryPath = Path.Combine(desktopPath, "Students");
            if (!Directory.Exists(outputDirectoryPath))
            {
                Directory.CreateDirectory(outputDirectoryPath);
            }
            foreach (var student in students)
            {
                string groupFilePath = Path.Combine(outputDirectoryPath, $"{student.Group}.txt");
                using (StreamWriter writer = new StreamWriter(groupFilePath, true))
                {
                    foreach (var s in students)
                    {
                        if (s.Group == student.Group)
                        {
                            writer.WriteLine($"{s.Name}, {s.DateOfBirth}, {s.AverageScore}");
                        }
                    }
                }
            }
            Console.WriteLine("Работа программы завершена, проверьте рабочий стол");
        }

        static List<Student> ReadStudentsFromBinFile(string filePath)
        {
            List<Student> students = new List<Student>();
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var student = new Student()
                    {
                        Name = reader.ReadString(),
                        Group = reader.ReadString(),
                        DateOfBirth = DateTime.FromBinary(reader.ReadInt64()),
                        AverageScore = reader.ReadDecimal()
                    };
                    students.Add(student);
                }
            }
            return students;
        }
    }
}