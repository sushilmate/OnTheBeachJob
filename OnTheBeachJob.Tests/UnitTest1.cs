using System;
using Xunit;
using OnTheBeachJob.ConsoleUI;

namespace OnTheBeachJob.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmptyInputReturnsEmptyOutput()
        {
            var onTheBeachJob = new OnTheBeachJobStarter();

            var result = onTheBeachJob.Run(new string[] { "" });

            //Assert
            Assert.True(result == string.Empty);
        }
    }
}
