using System;
using System.IO;
using SharpFNT;

namespace fonverter
{
    static class Program
    {
        public static int Main(string[] args)
        {
            static int ShowUsage()
            {
                Console.Error.WriteLine("Usage: fonverter a.ext1 b.ext2");
                Console.Error.WriteLine("File extensions specify formats to convert from and to.");
                Console.Error.WriteLine("Supported formats: 'bin' (binary), 'fnt' (text) and 'xml' (XML).");
                return 1;
            }

            static FormatHint GetFormat(string filePath) => Path.GetExtension(filePath).ToLower() switch
            {
                ".fnt" => FormatHint.Text,
                ".bin" => FormatHint.Binary,
                ".xml" => FormatHint.XML,
                _ => throw new NotSupportedException()
            };

            if (args.Length != 2)
                return ShowUsage();

            try
            {
                BitmapFont.FromFile(args[0], GetFormat(args[0])).Save(args[1], GetFormat(args[1]));
            }
            catch (NotSupportedException)
            {
                return ShowUsage();
            }
            return 0;
        }
    }
}
