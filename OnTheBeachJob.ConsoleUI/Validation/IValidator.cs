using System;
using System.Collections.Generic;
using System.Text;

namespace OnTheBeachJob.ConsoleUI.Validation
{
    public interface IValidator
    {
        bool IsEmpty(string[] inputs);
        bool IsValidInputPattern(string[] inputs);
    }
}
