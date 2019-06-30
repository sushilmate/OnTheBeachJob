using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OnTheBeachJob.ConsoleUI.Validation
{
    public class Validator : IValidator
    {
        /// <summary>
        /// This will check if the collection is null or zero element or elements with empty string
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public bool IsEmpty(string[] inputs)
        {
            if (inputs == null || inputs.Length == 0 || inputs.All(x => string.IsNullOrWhiteSpace(x)))
            {
                return true;
            }
            return false;
        }
    }
}