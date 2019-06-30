using System;
using Xunit;
using OnTheBeachJob.ConsoleUI;
using OnTheBeachJob.ConsoleUI.Validation;

namespace OnTheBeachJob.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmptyInputItemReturnsEmptyOutput()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "" });

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestNullInputReturnsEmptyOutput()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(null);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestEmptyInputReturnsEmptyOutput()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[0]);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestIfInputStringsWithOneParenthesisValid()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "a => " });

            //Assert
            Assert.True(result != string.Empty);
        }

        [Fact]
        public void TestIfInputStringsWithTwoParenthesisValid()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "a => b" });

            //Assert
            Assert.True(result != string.Empty);
        }
    }
}
