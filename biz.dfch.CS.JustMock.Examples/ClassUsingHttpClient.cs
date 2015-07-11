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

namespace biz.dfch.CS.JustMock.Examples
{
    public class ClassUsingHttpClient
    {
        public String Invoke(String uri)
        {
            // this instance of the HttpClient class will be mocked
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri).Result;
                if(response.StatusCode.Equals(HttpStatusCode.Unauthorized))
                {
                    throw new UnauthorizedAccessException("HttpStatusCode.Unauthorized");
                }
                response.EnsureSuccessStatusCode();

                var content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
