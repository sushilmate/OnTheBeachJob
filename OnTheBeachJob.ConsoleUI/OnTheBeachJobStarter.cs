using OnTheBeachJob.ConsoleUI.Enums;
using OnTheBeachJob.ConsoleUI.Extensions;
using OnTheBeachJob.ConsoleUI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnTheBeachJob.ConsoleUI
{
    public class OnTheBeachJobStarter
    {
        public OnTheBeachJobStarter()
        {
        }

        public OnTheBeachJobStarter(IValidator validator)
        {
            Validator = validator;

            // Rule to Validate Input
            Validator.AddInputRule(ValidationRuleType.IsEmpty);
            Validator.AddInputRule(ValidationRuleType.IsValidInputPattern);
            Validator.AddInputRule(ValidationRuleType.AreInputJobsSelfJoined);

            // Rule to Validate Output
            Validator.AddOutputRule(ValidationRuleType.CheckCircularReference);
        }

        internal IValidator Validator { get; }

        public string Run(string[] args)
        {
            bool isInValid = Validator.ValidateInput(args);

            if (isInValid)
            {
                return Validator.ValidationError;
            }

            LinkedList<string> sequencedJobs = new LinkedList<string>();

            foreach (string item in args)
            {
                List<string> jobs = Parse(item);
                if (jobs.Count == 1) // Non dependent job add if not added before
                {
                    if (!sequencedJobs.Contains(jobs[0]))
                    {
                        sequencedJobs.AddLast(jobs[0]);
                    }

                    continue;
                }
                if (sequencedJobs.Contains(jobs[0])) // Added job found the dependency in iteration.
                {
                    LinkedListNode<string> currentNode = sequencedJobs.Find(jobs[0]);
                    sequencedJobs.AddAfter(currentNode, jobs[1]);
                    continue;
                }
                if (sequencedJobs.Contains(jobs[1]))  // New Dependent jobs found with already added item.
                {
                    LinkedListNode<string> currentNode = sequencedJobs.Find(jobs[1]);
                    sequencedJobs.AddBefore(currentNode, jobs[0]);
                    continue;
                }
                sequencedJobs.AppendRange(jobs); // New Job with dependent Job. 
            }

            string orderedJobs = OrderByAsc(sequencedJobs);

            isInValid = Validator.ValidateOutput(new string[] { orderedJobs });

            return isInValid ? Validator.ValidationError : orderedJobs;
        }

        /// <summary>
        /// Parsing input string into dependent jobs if any
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static List<string> Parse(string item)
        {
            List<string> jobList = new List<string>();
            // splitting the string & removing empty entries
            string[] jobs = item.Split(" => ", StringSplitOptions.RemoveEmptyEntries);
            // Adding dependent job first then next job
            for (int i = jobs.Length - 1; i >= 0; i--)
            {
                jobList.Add(jobs[i]);
            }
            return jobList;
        }

        /// <summary>
        /// This will order the list from first to last element and returns the elements in a string format
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static string OrderByAsc(IEnumerable<string> list)
        {
            return list.Aggregate(string.Empty, (current, item) => current + item);
        }
    }
}