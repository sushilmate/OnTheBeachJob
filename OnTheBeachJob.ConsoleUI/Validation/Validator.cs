﻿using System.Linq;
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
                result = Regex.IsMatch(item, "[a-zA-Z]+ => [a-zA-Z]*");
                //Break the loop, found invalid input
                if (!result)
                    return result;
            }
            return result;
        }

        public bool AreInputJobsSelfJoined(string[] inputs)
        {
            foreach (var item in inputs)
            {
                var jobs = Regex.Split(item, "=>", RegexOptions.IgnorePatternWhitespace);
                //Break the loop, found self joined input
                if (jobs.Length == 2 && jobs[0].Trim() == jobs[1].Trim())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the string has circular reference
        /// </summary>
        /// <param name="orderedJobs"></param>
        /// <returns></returns>
        public bool CheckCircularReference(string orderedJobs)
        {
            // if the character repeats in the list that means we got the ciruclar reference.
            var repeatedChars = orderedJobs.ToCharArray().GroupBy(x => x).Where(y => y.Count() > 1).Select(z => z.Key).ToList();
            return repeatedChars.Count > 0;
        }
    }
}