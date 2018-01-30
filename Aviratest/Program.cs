using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aviratest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> German = new List<string>();
            List<string> English = new List<string>();
            List<int> Counter = new List<int>();
            if (args.Length > 0)
            {
                using (var CSVFile = File.Open(args[0], FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader str = new StreamReader(CSVFile))
                    {
                        string line = "";
                        int i = 0;
                        while ((line = str.ReadLine()) != null)
                        {
                            if (i != 0)
                            {
                                string[] Values = line.Split(';',',');
                                German.Add(Values[0]);
                                English.Add(Values[1]);
                                Counter.Add(Convert.ToInt32(Values[2]));
                            }
                            i++;
                        }
                    }
                }
            }

            Console.WriteLine("Vocabulary training started");
            Console.WriteLine("Please enter the German translations");
            
            if(IsCompeletedLesson(Counter))
            {
                Console.WriteLine("Congratulations. You successfully finished the lesson.");
            }
            else
            {
                var correct = 0;
                var wrong = 0;
                for (int i = 0; i < German.Count; i++)
                {
                    if (Counter[i] == 4)
                        continue;
                    Console.WriteLine("English:" + English[i]);
                    Console.Write("German:");
                    var GermanVal = Console.ReadLine();
                    if (GermanVal.Trim().ToLower() == German[i].Trim().ToLower())
                    {
                        Console.WriteLine("Correct");
                        Counter[i]++;
                        correct++;
                    }
                    else
                    {
                        Console.WriteLine("Wrong. Correct is: " + German[i]);
                        Counter[i]--;
                        wrong++;
                    }
                }
                Console.WriteLine("Total:" + Counter.Count + ", Correct:" + correct + ", Wrong: " + wrong);
            }
            

            using (var CSVFile = File.Open(args[0], FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter stw = new StreamWriter(CSVFile))
                {
                    stw.WriteLine("German;English;Count");
                    for (int i = 0; i < German.Count; i++)
                    {
                        stw.WriteLine(German[i] + ";" + English[i] + ";" + Counter[i]);
                    }
                }
            }

            Console.ReadKey();
        }

        public static bool IsCompeletedLesson(List<int>Count)
        {
            var C = Count.Where(a => a == 4).Count();
            return C == Count.Count ? true : false;
        }
    }
}
