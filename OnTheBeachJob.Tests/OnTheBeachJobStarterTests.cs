using OnTheBeachJob.ConsoleUI;
using OnTheBeachJob.ConsoleUI.ErrorMessages;
using OnTheBeachJob.ConsoleUI.Validation;
using Xunit;

namespace OnTheBeachJob.Tests
{
    public class OnTheBeachJobStarterTests
    {
        public OnTheBeachJobStarterTests()
        {
            OnTheBeachJob = new OnTheBeachJobStarter(new Validator());
        }

        internal OnTheBeachJobStarter OnTheBeachJob { get; }

        [Fact]
        public void TestEmptyInputItemReturnsEmptyOutput()
        {
            string result = OnTheBeachJob.Run(new string[] { "" });

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestNullInputReturnsEmptyOutput()
        {
            string result = OnTheBeachJob.Run(null);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestEmptyInputReturnsEmptyOutput()
        {
            string result = OnTheBeachJob.Run(new string[0]);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestIfInputStringsWithOneParenthesisValid()
        {
            string result = OnTheBeachJob.Run(new string[] { "a => " });

            //Assert
            Assert.True(result == "a");
        }

        [Fact]
        public void TestIfInputStringsWithTwoParenthesisValid()
        {
            string result = OnTheBeachJob.Run(new string[] { "a => b" });

            //Assert
            Assert.True(result == "ba");
        }


        [Fact]
        public void TestInputWithMultipleParenthesisValid()
        {
            string result = OnTheBeachJob.Run(new string[] { "a => ", "b => ", "c => " });

            //Assert
            Assert.True(result == "abc");
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid()
        {
            string result = OnTheBeachJob.Run(new string[] { "a => ", "b => c", "c => " });

            //Assert
            Assert.True(result == "acb");
        }

        [Fact]
        public void TestJobsWithSelfJoined()
        {
            //Act
            string result = OnTheBeachJob.Run(new string[] { "a => ", "b => c", "c => c" });

            //Assert
            Assert.True(result == ErrorMessage.SelfJoinedErrorMessage);
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid2()
        {
            string result = OnTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => " });

            //Assert
            Assert.True(result == "adfcbe");
        }

        [Fact]
        public void TestJobsWithDependantJobIsInValid()
        {
            string result = OnTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f" });

            //Assert
            Assert.True(result == ErrorMessage.InvalidInputPatternErrorMessage);
        }

        [Fact]
        public void TestJobsWithCircularReference()
        {
            //Act
            string result = OnTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => b" });

            //Assert
            Assert.True(result == ErrorMessage.CircularErrorMessage);
        }
    }
}