using System.Linq;
using System.Text.RegularExpressions;

namespace OnTheBeachJob.ConsoleUI.Validation
{
    public class Validator : IValidator
    {
        /// <summary>
        /// This will check if the collection is null or zero element or elements with empty string
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>True if valid input/s, false if not</returns>
        public bool IsEmpty(string[] inputs)
        {
            if (inputs == null || inputs.Length == 0 || inputs.All(x => string.IsNullOrWhiteSpace(x)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check whether input string follows given pattern.
        /// e.g. 1) a => 
        /// 2) a => b
        /// 3) a => c
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>True if valid input/s, false if not</returns>
        public bool IsValidInputPattern(string[] inputs)
        {
            var result = false;

            foreach (var item in inputs)
            {
                result = Regex.IsMatch(inputs[0], "[a-zA-Z]+ => [a-zA-Z]*");
                //Break the loop, found invalid input
                if (!result)
                    return result;
            }
            return result;
        }
    }
}