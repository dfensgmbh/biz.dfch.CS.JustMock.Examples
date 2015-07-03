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
    public class MustBeCalledWithMatchingTest
    {
        private Boolean _called;

        [TestInitialize]
        public void TestInitialize()
        {
            _called = false;
        }

         /**
         * If passing a specific parameter like "Test" in mock arrangment the mocked method will only be called
         * if the parameter in the call (Act) matches the parameter specified in arrangement.
         **/

        [TestMethod]
        public void MockedConsoleWriteLineGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { _called = true; }).MustBeCalled();

            Console.WriteLine("Test");

            Assert.IsTrue(_called);
        }

        [TestMethod]
        public void MockedConsoleWriteLineNotGettingCalledWithSpecificArgument()
        {
            Mock.SetupStatic(typeof(Console), StaticConstructor.Mocked);
            Mock.Arrange(() => Console.WriteLine("Test")).DoInstead(() => { _called = true; }).MustBeCalled();

            Console.WriteLine("Something");

            Assert.IsFalse(_called);
        }
    }
}
