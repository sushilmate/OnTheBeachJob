using OnTheBeachJob.ConsoleUI.Enums;
using OnTheBeachJob.ConsoleUI.ErrorMessages;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnTheBeachJob.ConsoleUI.Validation
{
    public class Validator : IValidator
    {
        private List<ValidationRuleType> InputRules { get; }
        private List<ValidationRuleType> OutputRules { get; }
        public string ValidationError { get; private set; }

        public Validator()
        {
            this.InputRules = new List<ValidationRuleType>();
            this.OutputRules = new List<ValidationRuleType>();
            this.ValidationError = string.Empty;
        }
        /// <summary>
        /// This will check if the collection is null or zero element or elements with empty string
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>True if valid input/s, false if not</returns>
        public bool IsEmpty(string[] inputs)
        {
            if (inputs == null || inputs.Length == 0 || inputs.All(x => string.IsNullOrWhiteSpace(x)))
            {
                ValidationError = string.Empty;
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
        public bool IsInvalidInputPattern(string[] inputs)
        {
            foreach (var item in inputs)
            {
                var result = Regex.IsMatch(item, "[a-zA-Z]+ => [a-zA-Z]*");
                //Break the loop, found invalid input
                if (!result)
                {
                    ValidationError = ErrorMessage.InvalidInputPatternErrorMessage;
                    return true;
                }
            }
            return false;
        }

        public bool AreInputJobsSelfJoined(string[] inputs)
        {
            foreach (var item in inputs)
            {
                var jobs = Regex.Split(item, "=>", RegexOptions.IgnorePatternWhitespace);
                //Break the loop, found self joined input
                if (jobs.Length == 2 && jobs[0].Trim() == jobs[1].Trim())
                {
                    ValidationError = ErrorMessage.SelfJoinedErrorMessage;
                    return true;
                }
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
            if (repeatedChars.Count > 0)
            {
                ValidationError = ErrorMessage.CircularErrorMessage;
                return true;
            }
            return false;
        }

        public void AddInputRule(ValidationRuleType validationRule)
        {
            InputRules.Add(validationRule);
        }

        public void AddOutputRule(ValidationRuleType validationRule)
        {
            OutputRules.Add(validationRule);
        }

        public bool ValidateInput(string[] input)
        {
            // Clear out the errors before processing it.
            ValidationError = string.Empty;
            var isInvalid = false;

            foreach (var item in InputRules)
            {
                switch (item)
                {
                    case ValidationRuleType.IsEmpty:
                        isInvalid = IsEmpty(input);
                        break;
                    case ValidationRuleType.IsValidInputPattern:
                        isInvalid = IsInvalidInputPattern(input);
                        break;
                    case ValidationRuleType.AreInputJobsSelfJoined:
                        isInvalid = AreInputJobsSelfJoined(input);
                        break;
                    default:
                        break;
                }
                if (isInvalid)
                    break; // breaking the loop as we got invalid input, no need to iterate further
            }
            return isInvalid;
        }

        public bool ValidateOutput(string output)
        {
            // Clear out the errors before processing it.
            ValidationError = string.Empty;
            var isInvalid = false;

            foreach (var item in OutputRules)
            {
                switch (item)
                {
                    case ValidationRuleType.CheckCircularReference:
                        isInvalid = CheckCircularReference(output);
                        break;
                    default:
                        break;
                }
                if (isInvalid)
                    break; // breaking the loop as we got invalid input, no need to iterate further
            }
            return isInvalid;
        }
    }
}