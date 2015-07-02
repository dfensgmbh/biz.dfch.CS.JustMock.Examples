using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    [TestClass]
    public class StaticMockingTests
    {
        private Boolean called;

        [TestInitialize]
        public void TestInitialize()
        {
            called = false;
        }

        /**
         * When mocking DateTimeOffset it's important to create the mock with the 
         * Behavior.CallOriginal behaviour, if DateTimeOffset is used in arrangement.
         * 
         * Otherwise a default value will be returned because by default the Behaviour is set to RecursiveLooser as described here:
         * http://www.telerik.com/help/justmock/basic-usage-mock-behaviors.html
         **/
        [TestMethod]
        public void MockedDateTimeOffsetReturnsExpectedDateTimeOffset()
        {
            Mock.SetupStatic(typeof(DateTimeOffset), Behavior.CallOriginal, StaticConstructor.Mocked);
            Mock.Arrange(() => DateTimeOffset.UtcNow).Returns(new DateTimeOffset(2015, 1, 1, 3, 10, 7, new TimeSpan()));

            Assert.AreEqual(new DateTimeOffset(2015, 1, 1, 3, 10, 7, new TimeSpan()), DateTimeOffset.UtcNow);
        }


        /**
         * If passing a specific parameter like "Test" in mock arrangment the mocked method will only be called
         * if the parameter in the call (Act) matches the parameter specified in arrangement.
         **/

        [TestMethod]
        public void MockedConsoleWriteLineGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { called = true; }).MustBeCalled();
            
            Console.WriteLine("Test");
            
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void MockedConsoleWriteLineNotGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { called = true; }).MustBeCalled();

            Console.WriteLine("Something");

            Assert.IsFalse(called);
        }
    }
}
