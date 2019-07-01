using OnTheBeachJob.ConsoleUI.Validation;
using System;
using System.Collections.Generic;
using OnTheBeachJob.ConsoleUI.Extensions;

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

            validationResult = Validator.IsValidInputPattern(args);

            if (!validationResult)
                return string.Empty;

            validationResult = Validator.AreInputJobsSelfJoined(args);

            if (validationResult)
                return "Error Self Joined Present in the input";

            var sequencedJobs = new LinkedList<string>();
            //LinkedListNode<string> currentNode = new LinkedListNode<string>("");
            
            foreach (var item in args)
            {
                var jobs = Parse(item);
                if(sequencedJobs.Count == 0)
                {
                    sequencedJobs.AppendRange(jobs);
                    continue; // found first node for the list move to next.
                }
                if(jobs.Count == 1)
                {
                    if (!sequencedJobs.Contains(jobs[0]))
                        sequencedJobs.AddLast(jobs[0]);
                    continue;
                }
                if (sequencedJobs.Contains(jobs[0]))
                {
                    var currentNode = sequencedJobs.Find(jobs[0]);
                    sequencedJobs.AddAfter(currentNode, jobs[1]);
                    continue;
                }
                if (sequencedJobs.Contains(jobs[1]))
                {
                    var currentNode = sequencedJobs.Find(jobs[1]);
                    sequencedJobs.AddBefore(currentNode, jobs[0]);
                    continue;
                }
                sequencedJobs.AppendRange(jobs);
            }

            return OrderByAsc(sequencedJobs);
        }

        /// <summary>
        /// Parsing input string into dependant jobs if any
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private List<string> Parse(string item)
        {
            var jobList = new List<string>();
            // spiltting the string & removing empty entries
            var jobs = item.Split(" => ", StringSplitOptions.RemoveEmptyEntries);
            // Adding dependent job first then next job
            for (int i = jobs.Length -1; i >= 0; i--)
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
        private string OrderByAsc(LinkedList<string> list)
        {
            var result = string.Empty;
            foreach (var item in list)
            {
                result += item;
            }
            return result;
        }
    }
}