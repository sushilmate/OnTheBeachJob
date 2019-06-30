using System;

namespace OnTheBeachJob.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var onTheBeachJobStarter = new OnTheBeachJobStarter();
                var result = onTheBeachJobStarter.Run(args);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception caught in Main function Reason: {0}", ex.Message));
            }
        }
    }
}
