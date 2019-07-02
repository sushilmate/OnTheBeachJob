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
                ServiceCollection serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // create service provider
                ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

                // entry to run app
                string result = serviceProvider.GetService<OnTheBeachJobStarter>().Run(args);

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception caught in Main function Reason: {0}", ex.Message));
            }
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IValidator, Validator>();
        }
    }
}
