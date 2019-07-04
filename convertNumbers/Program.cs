using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace convertNumbers
{
    static class Roman
    {
        static string[] roman1 = { "MMM", "MM", "M" };
        static string[] roman2 = { "CM", "DCCC", "DCC", "DC", "D", "CD", "CCC", "CC", "C" };
        static string[] roman3 = { "XC", "LXXX", "LXX", "LX", "L", "XL", "XXX", "XX", "X" };
        static string[] roman4 = { "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };

        public static bool TryParse(string text, out int value)
        {
            value = 0;
            if (String.IsNullOrEmpty(text)) return false;
            text = text.ToUpper();
            int len = 0;

            for (int i = 0; i < 3; i++)
            {
                if (text.StartsWith(roman1[i]))
                {
                    value += 1000 * (3 - i);
                    len = roman1[i].Length;
                    break;
                }
            }

            if (len > 0)
            {
                text = text.Substring(len);
                len = 0;
            }

            for (int i = 0; i < 9; i++)
            {
                if (text.StartsWith(roman2[i]))
                {
                    value += 100 * (9 - i);
                    len = roman2[i].Length;
                    break;
                }
            }

            if (len > 0)
            {
                text = text.Substring(len);
                len = 0;
            }

            for (int i = 0; i < 9; i++)
            {
                if (text.StartsWith(roman3[i]))
                {
                    value += 10 * (9 - i);
                    len = roman3[i].Length;
                    break;
                }
            }

            if (len > 0)
            {
                text = text.Substring(len);
                len = 0;
            }

            for (int i = 0; i < 9; i++)
            {
                if (text.StartsWith(roman4[i]))
                {
                    value += 9 - i;
                    len = roman4[i].Length;
                    break;
                }
            }

            if (text.Length > len)
            {
                value = 0;
                return false;
            }

            return true;
        }

        public static string ToRoman(int num)
        {
            if (num > 3999) throw new ArgumentException("Too big - can't exceed 3999");
            if (num < 1) throw new ArgumentException("Too small - can't be less than 1");
            int thousands, hundreds, tens, units;
            thousands = num / 1000;
            num %= 1000;
            hundreds = num / 100;
            num %= 100;
            tens = num / 10;
            units = num % 10;
            var sb = new StringBuilder();
            if (thousands > 0) sb.Append(roman1[3 - thousands]);
            if (hundreds > 0) sb.Append(roman2[9 - hundreds]);
            if (tens > 0) sb.Append(roman3[9 - tens]);
            if (units > 0) sb.Append(roman4[9 - units]);
            return sb.ToString();
        }
    }
    // Macintosh HD⁩/Users/cabicash/Hernandez/convert/Numbers.txt
    //Users/cabicash/Hernandez/⁨convertNumbers⁩/numbers.txt

    class Program
    {
        static void Main()
        {

            Console.WriteLine("Ingrese el documento Txt:");
            var direction = Console.ReadLine().Trim();
            var newDirection = direction.Substring(0, direction.Length - 5);

            // C:\Users\ivanh\convertApp\numbers.txt
            var document = System.IO.File.ReadAllLines(@direction);
            List<string> Numbers_Convert = new List<string>();

            string val;
            string typeNumber;
            string convertTo;
            string result = null;

            foreach (string rom in document)
            {
                var space = rom.Split('|');
                string numSelect = space[0];
                val = space[0];
                typeNumber = space[1];
                convertTo = space[2];
                Boolean isRoman = typeNumber == "roman" ? true : false;

                if (isRoman == true)
                {

                    string[] romans = { val };
                    int value = 0;

                    foreach (string roman in romans)
                    {
                        if (Roman.TryParse(roman, out value))
                            result = value.ToString();
                    }
                }
                else
                {

                    string intString = val;
                    int i = 0;
                    if (!Int32.TryParse(intString, out i))
                    {
                        i = -1;
                    }

                    if (i < 1)
                    {
                        result = "result";
                    }
                    else
                    {
                        int[] numbers = { i };

                        foreach (int number in numbers)
                        {
                            result = Roman.ToRoman(number);
                            // Console.WriteLine("{0} = {1}", number, Roman.ToRoman(number));  
                        }
                    }

                }

                Numbers_Convert.Add($"{val}|{typeNumber}|{convertTo}|{result}");

            }


            System.IO.File.WriteAllLines(newDirection + "_result_new.txt", Numbers_Convert);

            Console.WriteLine("Ready");

            // string[] romans = { "XX" };  
            // int value = 0;  

            // foreach (string roman in romans)  
            // {  
            //     if (Roman.TryParse(roman, out value)) Console.WriteLine("{0} = {1}", roman, value);  
            // }  


            // int[] numbers = { 10 };  

            // foreach (int number in numbers)  
            // {  
            //     Console.WriteLine("{0} = {1}", number, Roman.ToRoman(number));  
            // }  

            //Console.ReadKey();  
        }

    }
}
