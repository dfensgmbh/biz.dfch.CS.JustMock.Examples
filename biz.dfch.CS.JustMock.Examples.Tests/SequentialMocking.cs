using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    [TestClass]
    public class SequentialMocking
    {
        [TestMethod]
        public void MockedAddReturnsOneThenTwoThenThree()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();

            Mock.Arrange(() => mock.Add(Arg.AnyInt, Arg.AnyInt)).Returns(1).InSequence();
            Mock.Arrange(() => mock.Add(Arg.AnyInt, Arg.AnyInt)).Returns(2).InSequence();
            Mock.Arrange(() => mock.Add(Arg.AnyInt, Arg.AnyInt)).Returns(3).InSequence();
            
            //Act
            int result1 = mock.Add(3, 56);
            int result2 = mock.Add(234, 234);
            int result3 = mock.Add(254, 987);
            
            //Assert
            Assert.AreEqual(1,result1);
            Assert.AreEqual(2,result2);
            Assert.AreEqual(3,result3);
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedReturnStringReturnsJohnThenJohnnyThenJohnson()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();

            Mock.Arrange(() => mock.ReturnString(Arg.AnyString)).Returns("John").InSequence();
            Mock.Arrange(() => mock.ReturnString(Arg.AnyString)).Returns("Johnny").InSequence();
            Mock.Arrange(() => mock.ReturnString(Arg.AnyString)).Returns("Johnson").InSequence();
            
            //Act
            string result1 = mock.ReturnString("");
            string result2 = mock.ReturnString("");
            string result3 = mock.ReturnString("");
            
            //Assert
            Assert.AreEqual("John",result1);
            Assert.AreEqual("Johnny",result2);
            Assert.AreEqual("Johnson", result3);
        }

        [TestMethod]
        public void MockedAddReturnsOnlyMockedResultsWhenTheFirstNumberIsBiggerThanTenAndOriginalMustBeCalled()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Add(Arg.Matches<int>(x => x > 10), Arg.AnyInt)).Returns(1).InSequence();
            Mock.Arrange(() => mock.Add(Arg.Matches<int>(x => x > 10), Arg.AnyInt)).Returns(2).InSequence();
            
            //Test fails if mock.Add(Arg.AnyInt,Arg.AnyInt) is mocked with CallOriginal()
            Mock.Arrange(() => mock.Add(Arg.Matches<int>(x => x < 10), Arg.AnyInt)).CallOriginal().MustBeCalled(); 
            
            //Act
            int firstMockedResult = mock.Add(234, 54);
            int notMockedResult = mock.Add(3, 54);
            int secondMockedResult = mock.Add(45, 23);
            
            //Assert
            Assert.AreEqual(1, firstMockedResult);
            Assert.AreNotEqual(1, notMockedResult);
            Assert.AreNotEqual(2, notMockedResult);
            Assert.AreEqual(2,secondMockedResult);
        }
    }
}
