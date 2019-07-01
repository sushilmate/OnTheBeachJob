namespace OnTheBeachJob.ConsoleUI.Validation
{
    public interface IValidator
    {
        bool IsEmpty(string[] inputs);
        bool IsValidInputPattern(string[] inputs);
        bool AreInputJobsSelfJoined(string[] args);
        bool CheckCircularReference(string orderedJobs);
    }
}
