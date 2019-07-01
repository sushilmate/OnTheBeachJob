using OnTheBeachJob.ConsoleUI;
using OnTheBeachJob.ConsoleUI.Validation;
using System;
using Xunit;

namespace OnTheBeachJob.Tests
{
    public class OnTheBeachJobStarterTests
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
            Assert.True(result == "a");
        }

        [Fact]
        public void TestIfInputStringsWithTwoParenthesisValid()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "a => b" });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "ba");
        }


        [Fact]
        public void TestIfInputStringsWithMultipleParenthesisValid()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "a => ", "b => ", "c => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "abc");
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] { "a => ", "b => c", "c => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "acb");
        }

        [Fact]
        public void TestJobsWithSelfJoined()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            //Act
            void act() => onTheBeachJob.Run(new string[] { "a => ", "b => c", "c => c" });

            //Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void TestJobsWithDependantJobIsValid2()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            var result = onTheBeachJob.Run(new string[] 
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => " });

            //Assert
            Assert.True(result != string.Empty);
            Assert.True(result == "adfcbe");
        }


        [Fact]
        public void TestJobsWithCircularReference()
        {
            var validator = new Validator();

            var onTheBeachJob = new OnTheBeachJobStarter(validator);

            //Act
            void act() => onTheBeachJob.Run(new string[]
            { "a => ", "b => c", "c => f", "d => a", "e => b", "f => b" });

            //Assert
            Assert.Throws<ArgumentException>(act);
        }
    }
}