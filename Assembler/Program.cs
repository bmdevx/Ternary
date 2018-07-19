using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ternary;

namespace Assembler
{
    class Program
    {
        //.tasm - Ternary Assembly Language
        //.traw - Raw Ternary File [(#sym) +0- || (#num) numbers]

        //.btrn - binary storage of ternary asemmbly, sequence of Int16 that represent a Tryte


        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string file = args[0];
                if (File.Exists(file))
                {
                    string outFile = $"{(Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file)))}.btrn";

                    if (File.Exists(outFile))
                    {
                        Console.WriteLine($"File '{outFile}' already exists.");
                    }
                    else
                    {
                        switch (Path.GetExtension(file).ToLower())
                        {
                            case ".traw": AssembleRaw(file, outFile); break;
                            case ".tasm": AssembleTasm(file, outFile); break;
                            default: Console.WriteLine("Invalid File Type"); break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"File '{file}' does not exist.");
                }
            }
            else
            {
                Console.WriteLine("No input file");
            }
        }


        static void AssembleTasm(string inputFile, string outputFile)
        {
            Console.WriteLine("TASM not yet supported.");
        }

        static void AssembleRaw(string inputFile, string outputFile)
        {
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line = reader.ReadLine();
                bool sym = true;

                if (line.StartsWith("#num"))
                {
                    sym = false;
                }
                else if (!line.StartsWith("#sym"))
                {
                    Console.WriteLine("Invalid file format.");
                    return;
                }
                
                using (BinaryWriter writer = new BinaryWriter(File.Create(outputFile)))
                {
                    int lineNum = 1;
                    if (sym)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (Tryte.TryParse(line, out Tryte tryte))
                            {
                                writer.Write((Int16)tryte.ToInt());
                            }
                            else
                            {
                                Console.WriteLine($"Format error on line {lineNum}.");
                                break;
                            }

                            lineNum++;
                        }
                    }
                    else
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (Int16.TryParse(line, out Int16 val))
                            {
                                if (val < Tryte.MIN_INT_VALUE || val > Tryte.MAX_INT_VALUE)
                                {
                                    Console.WriteLine($"Value out of range on line {lineNum}.");
                                    break;
                                }

                                writer.Write(val);
                            }
                            else
                            {
                                Console.WriteLine($"Format error on line {lineNum}.");
                                break;
                            }

                            lineNum++;
                        }
                    }
                }
            }
        }
    }
}
