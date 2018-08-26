using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BasicConsoleII
{
    /// <summary>
    /// The program is designed for BasicConsoleII that runs tests for
    /// Arrays, Lists, and StringFormatting that requires user data
    /// entry, processing, and output to the console window
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main entry for the program.
        /// Runs tests for arraytest, listtest, and stringformatting methods
        /// </summary>
        /// <param name="args">None processed</param>
        static void Main(string[] args)
        {
            ArrayTest();
            Console.WriteLine();

            ListTest();
            Console.WriteLine();

            StringFormatting();
            Console.WriteLine();

            // Pause the program for user to observe Console
            Console.Write("Press <Enter> to quit...");
            Console.ReadLine();
        } // end of Main

        /// <summary>
        /// This method asks the user to enter 5 numbers separated by commas and parses
        /// the user input to numbers based on first splitting it by the comma character.
        /// Then the minimum, maximum, sum, and average for the array of numbers is 
        /// computed and printed to the console. The lab specifically required Parse and not
        /// TryParse so there is potential for unhandled exceptions based on bad user data
        /// entry input. 
        /// </summary>
        static void ArrayTest()
        {
            string input;                       // A string for user data entry input
            string[] inputs = new string[5];    // A string array of split strings of input separated by a comma
            double[] values = new double[5];    // A double array of parsed numbers entered by the user
            double min;                         // The minimum number in the array
            double max;                         // The maximum number in the array
            double sum;                         // The sum of numbers in the array 
            double average;                     // The average of numbers in the array

            Console.WriteLine("ArrayTest()");
            Console.Write("Please enter 5 numbers separated by commas: ");
            input = Console.ReadLine();
            inputs = input.Split(',');

            // Lab specifically required parse not TryParse
            // Potential for unhandled exceptions here
            for (int i = 0; i < inputs.Length; i++)
            {
                values[i] = double.Parse(inputs[i]);
            }

            Array.Sort(values);

            min = values.Min();
            max = values.Max();
            sum = values.Sum();
            average = values.Average();

            Console.Write("Values: ");

            for (int i = 0; i < values.Length; i++)
            {
                Console.Write(values[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Min: " + min);
            Console.WriteLine("Max: " + max);
            Console.WriteLine("Sum: " + sum);
            Console.WriteLine("Average: " + average);

        } // end of ArrayTest()

        /// <summary>
        /// This method has the user to enter a series of words together followed by the 'q'
        /// character to quit the data entry process. The list of words entered is stored in
        /// a list then joined together into one string and then output to the console.
        /// </summary>
        static void ListTest()
        {        
            List<string> words = new List<string>();    // List to hold the words entered 
            string word;                                // String to hold the user data input
            string join;                                // String to join the list of user entered words

            Console.WriteLine("ListTest()");
            Console.Write("Continue entering one word at a time at the prompt. ");
            Console.WriteLine("Enter 'q' to quit.");

            while (true)
            { 
                word = Console.ReadLine();

                if (word.ToUpper() == "Q")
                {
                    break;
                }

                words.Add(word);
            } // end of while

            join = string.Join(" ", words);

            Console.WriteLine(join);
        } // end of ListTest()

        /// <summary>
        /// This method asks for user input for a part name, number, price, service phone, and manufacture date.
        /// While the user enters the data it validates the user data entry and requires the user to enter good data.
        /// The phone number can be entered in a natural format while the program removes any non-digit characters
        /// for phone data entry. Finally the list of records is printed out in a formated string with a header and row
        /// per the assignment instructions. Per the lab requirements, 5 records is requires, and so the user must enter
        /// 5 sequence of records for their data entry. 
        /// </summary>
        private static void StringFormatting()
        {
            List<int> partNumbers = new List<int>();        // List of part numbers
            List<string> partNames = new List<string>();    // List of part names
            List<decimal> prices = new List<decimal>();     // List of prices
            List<long> servicePhones = new List<long>();    // List of service phones
            List<DateTime> mfgDates = new List<DateTime>(); // List of manufacture dates

            int partNumber;     // Holds parsed user entry of part number
            string partName;    // Holds parsed user entry of part name
            decimal price;      // Holds parsed user entry of part price
            long servicePhone;  // Holds user service phone
            DateTime mfgDate;   // Holds user entry of part manufacture date
            string header;      // Holds header of the string format entry
            string record;      // Holds formated string of record

            char[] mychars = { '(', ')', '-', '#', ' ' };   // characters that will be removed from phone number data entry
            string input, newinput;                         // string for user data inputs
            bool ok;                                        // boolean for data validation and parsing success

            Console.WriteLine("StringFormatting()");
            Console.WriteLine("Five Data Entry Records Required");

            for (int i = 1; i <= 2; i++)
            {
                Console.WriteLine("Data Entry Record # " + i);

                // Ask User for Part Number
                while (true)
                {
                    Console.Write("Please enter a part number: ");
                    input = Console.ReadLine();

                    ok = Int32.TryParse(input.Trim(), out partNumber);

                    if (ok && (input.Trim().Length <= 9) && (partNumber >= 0))
                    {
                        partNumbers.Add(partNumber);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number");
                    }
                } // end of while

                // Ask User for Part Name
                while (true)
                {
                    Console.Write("Please enter a part name: ");
                    input = Console.ReadLine();

                    if (!String.IsNullOrWhiteSpace(input) && (input.Trim().Length <=23))
                    {
                        partName = input.Trim();
                        partNames.Add(partName);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid part name");
                    }
                } // end of while


                // Ask User for Price
                while (true)
                {
                    Console.Write("Please enter the part price: ");
                    input = Console.ReadLine();

                    ok = Decimal.TryParse(input.Trim(), NumberStyles.Currency, CultureInfo.CurrentCulture, out price);

                    if (ok && (price <= 999999.99M) && (price >= 0)) // makes sure the price is reasonable
                    {
                        prices.Add(price);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid price");
                    }
                } // end of while

                // Ask User for Phone Number
                while (true)
                {
                    Console.Write("Please enter a phone number: ");
                    input = Console.ReadLine();

                    // Call a method

                    newinput = ValidatePhoneNumber(mychars, input);

                    ok = long.TryParse(newinput.Trim(), out servicePhone);

                    if (newinput.Length != 10)  // makes sure 10 digits were entered (after removing chars)
                    {
                        ok = false;
                    }

                    if (ok && (servicePhone >= 0))  // makes sure the phone is not all 0's
                    {
                        servicePhones.Add(servicePhone);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid phone number");
                    }
                } // end of while

                // Ask User for Manufacture Date
                while (true)
                {
                    Console.Write("Please enter a valid manufacture date: ");
                    input = Console.ReadLine();

                    ok = DateTime.TryParse(input.Trim(), out mfgDate);

                    if (ok)
                    {
                        mfgDates.Add(mfgDate);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid date");
                    }
                } // end of while
            } // end of for loop


            // output to screen
            header = string.Format("{0,-10} {1, -24}{2,11} {3,-13} {4,-11}", "Part #", "Part Name", "Price", "Phone", "MFG Date");
            
            Console.WriteLine(header);
            Console.WriteLine(new String('=',71));
            //Console.WriteLine("01234567890123456789012345678901234567890123456789012345678901234567890");

            for (int i = 0; i < partNumbers.Count; i++)
            {
                record = string.Format("{0,-10:00-0000000} {1, -24}{2,11:C} {3,-13:(###)###-####} {4,-11:yyyy-MM-dd}", partNumbers[i], partNames[i], prices[i], servicePhones[i], mfgDates[i]);
                Console.WriteLine(record);
            } // end of for loop
        } // end of StringFormatting()

        /// <summary>
        /// This method helps a user enter a phone number in a format such as
        /// (317) 777-7777 and remove select characters to return a string of
        /// purely just numbers so the telephone number can be stored as a 
        /// number
        /// </summary>
        /// <param name="items">char[] character array of characters to remove from the telephone number
        /// example: '('</param>
        /// <param name="input">the string to remove the select characters</param>
        /// <returns>a new string with the select characters removed</returns>
        public static string ValidatePhoneNumber(char[] items, string input)
        {
            string newInput = input;
            int indexFound;

            for (int i = 0; i < items.Length; i++) // loops through all the possible character possibilities
            {
                char item = items[i];

                do
                {
                    indexFound = newInput.IndexOf(item);

                    if (indexFound >=0)
                    {
                        newInput = newInput.Remove(indexFound, 1);
                    }

                } while (indexFound >=0); // there may be more characters of the same, so loop until not found -1 is returned
            } // end of for loop

            return newInput;
        } // end of ValidatePhoneNumber()

    }
}
