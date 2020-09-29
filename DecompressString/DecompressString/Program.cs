using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DescompressString
{
    class Program
    {
        static void Main(string[] args)
        {
            //test 1
            string compressed = "3[ab]x5[qr]";
            string result = DecompressStringCompressed(compressed);
            Console.WriteLine();
            Console.WriteLine("Test 1");
            Console.WriteLine(string.Format("The string '{0}' decompressed is: {1}", compressed, result));

            //test 2
            compressed = "3[2[ab]Z]c";
            result = DecompressStringCompressed(compressed);
            Console.WriteLine();
            Console.WriteLine("Test 2");
            Console.WriteLine(string.Format("The string '{0}' decompressed is: {1}", compressed, result));

            //test 3
            compressed = "3[2[ab]Z5[R]]c";
            result = DecompressStringCompressed(compressed);
            Console.WriteLine();
            Console.WriteLine("Test 3");
            Console.WriteLine(string.Format("The string '{0}' decompressed is: {1}", compressed, result));
            Console.ReadKey();
        }
        public static string DecompressStringCompressed(string compressedString)
        {
            // I used Stack because Stack is LIFO, and get easier to work on this exercise
            Stack<string> stackDecompressingString = new Stack<string>();
            Stack<int> stackNumberOfRepetitions = new Stack<int>();
            string result = "";
            int repetition;

            for (int i = 0; i < compressedString.Length; i++)
            {
                repetition = 0;
                if (char.IsNumber(compressedString[i]))
                {
                    if (char.IsNumber(compressedString[i + 1]))
                    {
                        int.TryParse(compressedString[i].ToString() + compressedString[i + 1].ToString(), out repetition);
                    }
                    else
                    {
                        int.TryParse(compressedString[i].ToString(), out repetition);
                    }
                    stackNumberOfRepetitions.Push(repetition);
                }
                else if (compressedString[i] == ']')
                {
                    string tmpResult = "";
                    while (stackDecompressingString.Count > 0)
                    {
                        if (stackDecompressingString.Peek() != "[")
                        {
                            tmpResult = stackDecompressingString.Peek() + tmpResult;
                            stackDecompressingString.Pop();
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (stackNumberOfRepetitions.Count > 0)
                    {
                        repetition = stackNumberOfRepetitions.Peek();
                        stackNumberOfRepetitions.Pop();
                    }

                    if (stackDecompressingString.Count > 0 && stackDecompressingString.Peek() == "[")
                    {
                        stackDecompressingString.Pop();
                    }

                    for (int j = 0; j < repetition; j++)
                    {
                        stackDecompressingString.Push(result + tmpResult);
                    }
                    result = "";
                }
                else
                {
                    stackDecompressingString.Push(compressedString[i].ToString());
                }

                //Getting the result
                if (compressedString.Length == i + 1)
                {
                    while (stackDecompressingString.Count > 0)
                    {
                        result = stackDecompressingString.Peek() + result;
                        stackDecompressingString.Pop();
                    }
                }
            }

            return result;
        }
    }
}