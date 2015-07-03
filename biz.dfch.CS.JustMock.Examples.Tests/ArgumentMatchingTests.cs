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
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { _called = true; });

            // Act
            Console.WriteLine("Test");

            // Assert
            Assert.IsTrue(_called);
        }

        [TestMethod]
        public void MockedConsoleWriteLineNotGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { _called = true; });

            Console.WriteLine("Something");

            Assert.IsFalse(_called);
        }

        /**
         * For more details consult the Wiki
         * https://github.com/dfensgmbh/biz.dfch.CS.JustMock.Examples/wiki/ArgumentMatchingTests#matching-any-argument-of-type
         **/
        [TestMethod]
        public void MockedCDoNothingGettingCalledWithArgumentOfSpecificType()
        {
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.DoNothing(Arg.AnyString)).MustBeCalled();

            mock.DoNothing("Something");

            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDoNothingNotGettingCalledWithNullAsArgument()
        {
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.DoNothing(Arg.AnyString)).MustBeCalled();

            mock.DoNothing(null);

            Mock.Assert(mock);
        }

        [TestMethod]
        public void MockedDoNothingNotGettingCalledWithNullAsArgument2()
        {
            var mock = Mock.Create<MockSamplesHelper>();
            Mock.Arrange(() => mock.DoNothing(Arg.IsAny<String>())).MustBeCalled();

            mock.DoNothing(null);

            Mock.Assert(mock);
        }
    }
}
