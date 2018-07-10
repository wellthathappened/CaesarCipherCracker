// Name: Ian Lewis
// Date: 11/18/2017
// Course: COP 4020
// C# Caesar Cipher

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher
{
    static class StringExtensions
    {
        static double[] table = new double[] { 8.2, 1.5, 2.8, 4.3, 12.7, 2.2, 2.0, 6.1, 7.0, 0.2, 0.8, 4.0, 2.4, 6.7, 7.5, 1.9, 0.1, 6.0, 6.3, 9.1, 2.8, 1.0, 2.4, 0.2, 2.0, 0.1 };

        static void Main(string[] args)
        {
            string cracked = StringExtensions.Cracked("myxqbkdevkdsyxc yx mywzvodsxq dro ohkw!");

            Console.WriteLine(cracked);
            Console.ReadLine();
			
			string command = null;
			
			while(command != "exit")
			{
				string message = null;
				
				Console.WriteLine("Enter a commmand ('decode' to crack a code | 'encode' to encode a message | 'exit' to exit the program):");
                command = Console.ReadLine();
				
				if((command == "decode") || (command == "encode"))
				{
					if(command == "decode")
					{
						Console.WriteLine("Enter the message you'd like to decode:");
						message = Console.ReadLine();
						Console.WriteLine(StringExtensions.Cracked(message);
					}
					
					else if(command == "encode")
					{
						Console.WriteLine("Enter the message you'd like to encode:");
						message = Console.ReadLine();
						Console.WriteLine(StringExtensions.encode(message));
					}
				}
				
				else if(command != exit)
					Console.WriteLine("Invalid command.\n");
			}
        }

        public static bool nat2letCheck(char let)
        {
            int num = let;

            if ((num > 96) && (num < 123))
                return true;

            else return false;
        }

        public static int let2nat(char let)
        {
            return (int)(let - 97);
        }

        public static char nat2let(int nat)
        {
            return (char)(nat + 97);
        }

        public static char shift(int factor, char let)
        {
            if ((Char.IsUpper(let) == false) && (nat2letCheck(let) == true))
                return nat2let((((let2nat(let) + factor) % 26) + 26) % 26);

            else
                return let;
        }

        public static string encode(int factor, string str)
        {
            string encodedString = null;

            for(int i = 0; i < str.Length ; i++)
                encodedString += shift(factor, str[i]);

            return encodedString;
        }

        public static string decode(int factor, string str)
        {
            string decodedString = null;

            for(int i = 0; i < str.Length; i++)
                decodedString += shift(-factor, str[i]);

            return decodedString;
        }

        public static int lowers(string str)
        {
            int counter = 0;

            for(int i = 0; i < str.Length; i++)
                if (char.IsLower(str[i]))
                    counter++;

            return counter;
        }

        public static int count(char let, string str)
        {
            int counter = 0;

            for(int i = 0; i < str.Length; i++)
                if (let == str[i])
                    counter++;

            return counter;
        }

        public static double percent(int a, int b)
        {
            return ((double)a / (double)b) * 100;
        }

        public static double[] freqs(string str)
        {
            double[] frequency = new double[26];

            for (int i = 0; i < 26; i++)
                frequency[i] = percent(count((char)(i + 97), str), lowers(str));

            return frequency;
        }

        public static double[] rotate(int placeNumber, double[] list)
        {
            double[] newList = new double[26];

            for (int i = 0; i < 26 ; i++)
                newList[i] = list[(i + placeNumber) % 26];

            return newList;
        }

        public static double chisqr(double[] o, double[] e)
        {
            double sum = 0;

            for (int i = 0; i < 26; i++)
                sum += (Math.Pow((o[i] - e[i]), 2) / e[i]);

            return sum;
        }

        public static string Cracked(string str)
        {
            double[] frequencyList = freqs(str);
            double[] chiSqList = new double[26];
            double minVal = 10000000;
            int factor = 0;

            for (int i = 0; i < 26; i++)
            {
                chiSqList[i] = chisqr(rotate(i, frequencyList), table);

                if (chiSqList[i] < minVal)
                {
                    minVal = chiSqList[i];
                    factor = i;
                }
            }

            return decode(factor, str);
        }
    }
}