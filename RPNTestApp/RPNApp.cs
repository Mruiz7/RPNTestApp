using System;
using System.Collections.Generic;
using System.Linq;

namespace RPNTest
{
    class RPNApp
    {

        #region [ -- Properties & Attributes   -- ]     

        private static int valueLimit;

        public static List<int> rpnFinalList = new List<int>();

        #endregion

        #region [ -- Constructors -- ]

        static void Main(string[] args)
        {
            ConsoleLogic();
        }

        #endregion

        #region [ -- Private Methods -- ] 

        #region [ -- Logic Methods -- ] 

        private static List<int> GeneratePrimes(int n)
        {
            rpnFinalList.Add(2);
            rpnFinalList.Add(3);
            rpnFinalList.Add(5);
            rpnFinalList.Add(7);

            var listPrimes = (from i in Enumerable.Range(2, n - 1).AsParallel()
                              where Enumerable.Range(2, (int)Math.Sqrt(i)).All(j => i % j != 0 && IsPrimeValidateNumber(i))
                              orderby i
                              select i).ToList();

            listPrimes.ForEach(x =>
            {
                ValidateListRPN(x, listPrimes);
            });

            return listPrimes;
        }

        private static string GetLength(int valueRPN, int lengthRPN)
        {
            return valueRPN.ToString().Substring(valueRPN.ToString().Length - lengthRPN);
        }

        private static void ValidateListRPN(int valueRPN, List<int> listValidate)
        {
            var lengthRON = valueRPN.ToString().Length;
            ValidatedLengthAndAddList(valueRPN, lengthRON, listValidate);
        }

        private static bool IsPrimeValidateNumber(int valueRPN)
        {
            return (valueRPN.ToString().Contains(0.ToString()) ? false : true);
        }

        private static void ValidatedLengthAndAddList(int valueRPN, int lengthRPN, List<int> listValidate)
        {
            for (int i = 1; i < lengthRPN; i++)
            {
                var lengthFinalRPN = GetLength(valueRPN, i);
                var IsValid = listValidate.Contains(Int32.Parse(lengthFinalRPN));
                if (!IsValid)
                {
                    return;
                }
            }

            var lengthReducedRPN = (lengthRPN >= 2 ? lengthRPN - 1 : lengthRPN);
            var lengthReducedFinalRPN = GetLength(valueRPN, lengthRPN);
            if (!rpnFinalList.Contains(valueRPN))
            {
                rpnFinalList.Add(valueRPN);
            }
        }

        #endregion

        #region [ -- Console Methods -- ] 

        private static void ConsoleLogic()
        {
            bool validList = false;
            bool validCheck = false;

            int inputMenu = 0;
            do
            {
                inputMenu = DisplayMenu();

                switch (inputMenu)
                {
                    case 1:
                        valueLimit = 0;

                        while (valueLimit == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Enter the upper limit number you want");
                            Console.WriteLine();

                            string inputLimit = Console.ReadLine();
                            ValidateNumber(inputLimit, out valueLimit);
                            var timeWatch = System.Diagnostics.Stopwatch.StartNew();
                            rpnFinalList.Clear();
                            GeneratePrimes(valueLimit);
                            timeWatch.Stop();
                            Console.WriteLine("Total execution time: " + timeWatch.ElapsedMilliseconds + " ms");
                            TotalCount();
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        ValidatedLimit();
                        validList = false;
                        while (rpnFinalList.Count > 0 && !validList)
                        {
                            rpnFinalList.ForEach(item =>
                            {
                                Console.WriteLine(item);
                            });
                            validList = true;
                            TotalCount();
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        ValidatedLimit();
                        while (rpnFinalList.Count > 0 && !validCheck)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select number between 1 and " + rpnFinalList.Count);
                            Console.WriteLine();

                            string numberCheck = Console.ReadLine();
                            int numberCheckRPN;
                            ValidateNumber(numberCheck, out numberCheckRPN);
                            numberCheckRPN--;
                            if (numberCheckRPN >= rpnFinalList.Count || numberCheckRPN == -1)
                            {
                                validCheck = false;
                                Console.WriteLine("Number out of range");
                            }
                            else
                            {
                                validCheck = true;
                                Console.WriteLine(rpnFinalList[numberCheckRPN]);
                                TotalCount();
                            }
                        }
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Select a valid option");
                        Console.WriteLine();
                        break;
                }


            } while (inputMenu != 4);
        }

        private static void ValidatedLimit()
        {
            if (rpnFinalList.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("It is necessary to enter a limit");
                Console.WriteLine();
            }
        }

        private static void ValidateNumber(string result, out int resultInt)
        {
            if (!int.TryParse(result, out resultInt))
            {
                Console.WriteLine();
                Console.WriteLine("The input " + result + " is not a VALID number");
                Console.WriteLine("Please enter a VALID number");
                Console.WriteLine();
                var resultRecursive = Console.ReadLine();
                ValidateNumber(resultRecursive, out resultInt);
            }
        }

        private static int DisplayMenu()
        {
            Console.WriteLine(" --- Generator Robustly Prime Number (RPN) ---");
            Console.WriteLine();
            Console.WriteLine("1. Enter the upper limit you want");
            Console.WriteLine("2. List Robustly Prime Number (RPN)");
            Console.WriteLine("3. Check Robustly Prime Number (RPN)");
            Console.WriteLine("4. Exit");
            Console.WriteLine();
            Console.WriteLine("Select an option");
            Console.WriteLine();

            var result = Console.ReadLine();
            int resultInt;

            ValidateNumber(result, out resultInt);

            return resultInt;
        }

        private static void TotalCount()
        {
            Console.WriteLine();
            Console.WriteLine("Total RPNs generated RPNs :" + rpnFinalList.Count());
            Console.WriteLine();
        }
      
        #endregion

        #endregion
    }
}
