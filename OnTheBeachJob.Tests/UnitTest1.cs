using System;
using Xunit;
using OnTheBeachJob.ConsoleUI;
using OnTheBeachJob.ConsoleUI.Validation;

namespace OnTheBeachJob.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmptyInputReturnsEmptyOutput()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "" });

            //Assert
            Assert.True(result == string.Empty);
        }
    }
}
