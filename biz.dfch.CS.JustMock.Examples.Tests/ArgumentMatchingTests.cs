/**
 * Copyright 2015 Marc Rufer, d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Microsoft.VisualStudio.TestTools.UnitTesting;
﻿using System;
using Telerik.JustMock;
using Telerik.JustMock.Core;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    [TestClass]
    public class ArgumentMatchingTests
    {
        private Boolean _called;

        [TestInitialize]
        public void TestInitialize()
        {
            _called = false;
        }

        /**
         * For more details consult the Wiki
         * https://github.com/dfensgmbh/biz.dfch.CS.JustMock.Examples/wiki/ArgumentMatchingTests#matching-specific-argument
         **/
        [TestMethod]
        public void MockedConsoleWriteLineGettingCalledWithMatchingArgument()
        {
            // Arrange
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test"))
                .DoInstead(() => { _called = true; })
                .OccursOnce();

            // Act
            Console.WriteLine("Test");

            // Assert
            Assert.IsTrue(_called);

            Mock.Assert(() => Console.WriteLine("Test"));
        }

        [TestMethod]
        public void MockedConsoleWriteLineNotGettingCalledWithSpecificArgument()
        {
            // Arrange
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test"))
                .DoInstead(() => { _called = true; })
                .OccursNever();

            // Act
            Console.WriteLine("Something");

            // Assert
            Assert.IsFalse(_called);

            Mock.Assert(() => Console.WriteLine("Test"));
        }

        /**
         * For more details consult the Wiki
         * https://github.com/dfensgmbh/biz.dfch.CS.JustMock.Examples/wiki/ArgumentMatchingTests#matching-any-argument-of-type
         **/
        [TestMethod]
        public void MockedCDummyGettingCalledWithArgumentOfSpecificType()
        {
            // Arrange
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.Dummy(Arg.AnyString))
                .OccursOnce();

            // Act
            mock.Dummy("Something");

            // Assert
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDummyGettingCalledWithNullAsArgument()
        {
            // Arrange
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.Dummy(Arg.AnyString))
                .OccursOnce();

            // Act
            mock.Dummy(null);

            // Assert
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDummyGettingCalledWithNullAsArgument2()
        {
            // Arrange
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.Dummy(Arg.IsAny<String>()))
                .OccursOnce();

            // Act
            mock.Dummy(null);

            // Assert
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDummySetsCalled()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Dummy(""))
                .DoInstead(() => _called = true)
                .OccursOnce();
            
            //Act
            mock.Dummy("");
           
            //Assert
            Assert.IsTrue(_called);

            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedAddReturns4()
        {
            //Arrange
            int result = 0;
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Add(Arg.IsAny<int>(), Arg.IsAny<int>()))
                .DoInstead(() => result = 4)
                .OccursOnce();
            
            //Act
            mock.Add(3, 3);
            
            //Assert
            Assert.AreEqual(result, 4);

            Mock.Assert(mock);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void MockedAddThrowsArgumentException()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Add(Arg.IsAny<int>(), Arg.IsAny<int>()))
                .Throws(new ArgumentException())
                .OccursOnce();
            
            //Act
            mock.Add(2, 2);

            // Assert
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedAddArgumentsAreInRangeFrom20To30()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(
                () => mock.Add(Arg.IsInRange(20, 30, RangeKind.Inclusive), Arg.IsInRange(20, 30, RangeKind.Inclusive)))
                .Returns(2)
                .OccursOnce();
           
            //Act
            var result = mock.Add(29, 27);
            
            //Assert
            Assert.AreEqual(2,result);
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDummyGetsCalledFromAdd()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Dummy(Arg.AnyString))
                .OccursOnce();
            
            //Act
            mock.Add(4, 3);
            
            //Assert
            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedAddReturns7IfCalledWithArguments1And1()
        {
            //Arrange
            MockSamplesHelper mock = new MockSamplesHelper();
            Mock.Arrange(() => mock.Add(1, 1))
                .Returns(7)
                .OccursOnce();
            
            //Act
            var result = mock.Add(1, 1);
            
            //Assert
            Assert.AreEqual(7,result);

            Mock.Assert(mock);
        }

        [TestMethod]
        [ExpectedException(typeof (StrictMockException))]
        public void MockedSomePropertyThrowsWhenSetToSomewhatNotEqualTheMock()
        {
            //Arrange
            var mock = Mock.Create<MockSamplesHelper>(Behavior.Strict);
            Mock.ArrangeSet(() => mock.SomeProperty = false);
            
            //Act
            mock.SomeProperty = true;
            
            //Assert
            Mock.Assert(mock);
        }
    }
}
