using OnTheBeachJob.ConsoleUI.Validation;
using System;
using System.Collections.Generic;

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

            var list = new LinkedList<string>();
            LinkedListNode<string> listNode = new LinkedListNode<string>("");

            foreach (var item in args)
            {
                var jobs = ParseInputToJobs(item);
                // if have dependant jobs
                if (jobs.Length == 2)
                {
                    // Check if the list is empty
                    if (list.Count == 0)
                    {
                        listNode = list.AddFirst(jobs[1]);
                    }
                    else
                    {
                        listNode = list.AddAfter(listNode, jobs[1]);
                    }
                    list.AddAfter(listNode, jobs[0]);
                }
                else
                {
                    // still checking if the list is empty
                    if (list.Count == 0)
                    {
                        listNode = list.AddFirst(jobs[0]);
                    }
                    else
                    {
                        if (!list.Contains(jobs[0]))
                            listNode = list.AddAfter(listNode, jobs[0]);
                    }
                }

            }

            return SerilizeList(list);
        }

        private string SerilizeList(LinkedList<string> list)
        {
            var result = string.Empty;
            foreach (var item in list)
            {
                result += item;
            }
            return result;
        }

        /// <summary>
        /// Parsing input string into dependant jobs if any
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string[] ParseInputToJobs(string item)
        {
            // spiltting the string & removing empty entries
            return item.Split(" => ", StringSplitOptions.RemoveEmptyEntries);
        }
    }
}