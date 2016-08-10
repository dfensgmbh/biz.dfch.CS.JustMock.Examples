/**
 *
 *
 * Copyright 2015 Ronald Rink, d-fens GmbH
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
 *
 */
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using Telerik.JustMock;

namespace biz.dfch.CS.JustMock.Examples.Tests
{
    //see http://www.telerik.com/help/justmock/advanced-usage-future-mocking.html
    [TestClass]
    public class FutureMockingTests
    {
        [TestMethod]
        public void MockInnerHttpClientWithStatusCodeThrowsUnauthorizedAccessException()
        {
            // in this test we mock _all_ objects of a specific type 
            // (instead of mocking a concrete instance)
            
            // Arrange
            var httpClient = Mock.Create<HttpClient>();
            var task = Mock.Create<System.Threading.Tasks.Task<HttpResponseMessage>>();

            Mock.Arrange(() => httpClient.GetAsync(Arg.AnyString))
                .IgnoreInstance()
                .Returns(task)
                .OccursOnce();
            Mock.Arrange(() => task.Result.StatusCode)
                .IgnoreInstance()
                .Returns(HttpStatusCode.Unauthorized)
                .OccursOnce();

            // Act
            var classUsingHttpClient = new ClassUsingHttpClient();
            ThrowsAssert.Throws<UnauthorizedAccessException>(() => classUsingHttpClient.Invoke("any-uri"));

            Mock.Assert(httpClient);
            Mock.Assert(task);
        }

        [TestMethod]
        public void MockInnerHttpClientWithStatusCodeReturnsString()
        {
            // in this test we mock _all_ objects of a specific type 
            // (instead of mocking a concrete instance)

            // Arrange
            var httpClient = Mock.Create<HttpClient>();
            var task = Mock.Create<System.Threading.Tasks.Task<HttpResponseMessage>>();
            var content = "some-arbitrary-text";

            Mock.Arrange(() => httpClient.GetAsync(Arg.AnyString))
                .IgnoreInstance()
                .Returns(task)
                .OccursOnce();
            Mock.Arrange(() => task.Result.StatusCode)
                .IgnoreInstance()
                .Returns(HttpStatusCode.OK)
                .OccursOnce();
            Mock.Arrange(() => task.Result.Content.ReadAsStringAsync().Result)
                .IgnoreInstance()
                .Returns(content)
                .OccursOnce();

            // Act
            var classUsingHttpClient = new ClassUsingHttpClient();
            var result = classUsingHttpClient.Invoke("any-uri");

            // Assert
            Assert.AreEqual(content, result);

            Mock.Assert(httpClient);
            Mock.Assert(task);
        }
    }
}
