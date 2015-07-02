using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    [TestClass]
    public class StaticMockingTests
    {
        [TestMethod]
        public void MockedConsoleWriteLineGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            var called = false;
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { called = true; }).MustBeCalled();
            Console.WriteLine("Test");
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void MockedConsoleWriteLineNotGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            var called = false;
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { called = true; }).MustBeCalled();
            Console.WriteLine("Something");
            Assert.IsFalse(called);
        }
    }
}
