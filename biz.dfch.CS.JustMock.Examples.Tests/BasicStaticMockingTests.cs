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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    [TestClass]
    public class BasicStaticMockingTests
    {

        /**
         * For more details consult the Wiki
         * https://github.com/dfensgmbh/biz.dfch.CS.JustMock.Examples/wiki/BasicStaticMockingTests#mocking-datetimeoffsetutcnow
         **/
        [TestMethod]
        public void MockedDateTimeOffsetReturnsExpectedDateTimeOffset()
        {
            // Arrange
            Mock.SetupStatic(typeof(DateTimeOffset), Behavior.CallOriginal, StaticConstructor.Mocked);
            Mock.Arrange(() => DateTimeOffset.UtcNow).Returns(new DateTimeOffset(2015, 1, 1, 3, 10, 7, new TimeSpan())).MustBeCalled();

            // Act & Assert
            Assert.AreEqual(new DateTimeOffset(2015, 1, 1, 3, 10, 7, new TimeSpan()), DateTimeOffset.UtcNow);

            // Assert Occurence
            Mock.Assert(() => DateTimeOffset.UtcNow);
        }
    }
}
