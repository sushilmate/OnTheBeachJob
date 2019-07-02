using Microsoft.Extensions.DependencyInjection;
using OnTheBeachJob.ConsoleUI.Validation;
using System;

namespace OnTheBeachJob.ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // create service collection
                IServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // create service provider
                ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                // Add custom input if empty for demo purpose.
                if (args.Length == 0)
                {
                    args = new string[] { "a => ", "b => c", "c => f", "d => a", "e => b", "f => " };
                }

                // entry to run app
                string result = serviceProvider.GetService<OnTheBeachJobStarter>().Run(args);

                Console.WriteLine(result);

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in Main function Reason: {ex.Message}");
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IValidator, Validator>();
            // add app
            serviceCollection.AddTransient<OnTheBeachJobStarter>();
        }
    }
}