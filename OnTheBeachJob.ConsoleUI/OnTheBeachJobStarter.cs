using OnTheBeachJob.ConsoleUI.Validation;
using System;

namespace OnTheBeachJob.ConsoleUI
{
    public class OnTheBeachJobStarter
    {
        public OnTheBeachJobStarter(IValidator validator)
        {
            this.Validator = validator;
        }

        internal IValidator Validator { get; }

        public string Run(string[] args)
        {
            var validationResult = Validator.IsEmpty(args);

            if (validationResult)
                return string.Empty;

            return "Non empty o/p";
        }
    }
}