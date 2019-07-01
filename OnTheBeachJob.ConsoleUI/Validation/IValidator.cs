using OnTheBeachJob.ConsoleUI.Enums;

namespace OnTheBeachJob.ConsoleUI.Validation
{
    public interface IValidator
    {
        string ValidationError { get; }

        void AddInputRule(ValidationRuleType validationRule);
        void AddOutputRule(ValidationRuleType validationRule);
        bool ValidateInput(string[] input);
        bool ValidateOutput(string output);
    }
}
