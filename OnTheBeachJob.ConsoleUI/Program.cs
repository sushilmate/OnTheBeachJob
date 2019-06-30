using Microsoft.Extensions.DependencyInjection;
using OnTheBeachJob.ConsoleUI.Validation;
using System;

namespace OnTheBeachJob.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // create service collection
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                // create service provider
                var serviceProvider = serviceCollection.BuildServiceProvider();

                // entry to run app
                var result = serviceProvider.GetService<OnTheBeachJobStarter>().Run(args);

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
