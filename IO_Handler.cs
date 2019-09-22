using System;
using System.IO;

namespace Cinemas
{
    class IO_Handler
    {
        public static string EnterString(string message = "")
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public static byte EnterByte(string message = "")
        {
            byte result;
            Console.Write(message);
            while (!byte.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Incorrect value! \nTry again in range of 1-255.\n\n");
                Console.Write(message);
            }
            return result;
        }
        public static void SaveToFile(string[] stringsToSave)
        {
            string filename = EnterString("Saving to file, please enter the filepath/filename: ");
            using (FileStream fs = new FileStream(@filename, FileMode.Create))
            using(StreamWriter sw = new StreamWriter(fs))
            {
                for (int i = 0; i < stringsToSave.Length; i++)
                {
                    sw.WriteLine(stringsToSave[i]);
                }
            }
        }
    }
}
    

