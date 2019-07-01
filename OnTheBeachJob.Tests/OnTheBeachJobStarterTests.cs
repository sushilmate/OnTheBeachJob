using OnTheBeachJob.ConsoleUI;
using OnTheBeachJob.ConsoleUI.Validation;
using System;
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
            var result = OnTheBeachJob.Run(new string[] { "" });

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestNullInputReturnsEmptyOutput()
        {
            var result = OnTheBeachJob.Run(null);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestEmptyInputReturnsEmptyOutput()
        {
            var result = OnTheBeachJob.Run(new string[0]);

            //Assert
            Assert.True(result == string.Empty);
        }

        [Fact]
        public void TestIfInputStringsWithOneParenthesisValid()
        {
            var result = OnTheBeachJob.Run(new string[] { "a => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "a");
        }

        [Fact]
        public void TestIfInputStringsWithTwoParenthesisValid()
        {
            var result = OnTheBeachJob.Run(new string[] { "a => b" });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "ba");
        }


        [Fact]
        public void TestIfInputStringsWithMultipleParenthesisValid()
        {
            var result = OnTheBeachJob.Run(new string[] { "a => ", "b => ", "c => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "abc");
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid()
        {
            var result = OnTheBeachJob.Run(new string[] { "a => ", "b => c", "c => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "acb");
        }

        [Fact]
        public void TestJobsWithSelfJoined()
        {
            //Act
            void act() => OnTheBeachJob.Run(new string[] { "a => ", "b => c", "c => c" });

            //Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid2()
        {
            var result = OnTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "adfcbe");
        }

        [Fact]
        public void TestJobsWithCircularReference()
        {
            //Act
            void act() => OnTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => b" });

            //Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}