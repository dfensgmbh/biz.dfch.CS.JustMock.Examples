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
﻿using System;

namespace biz.dfch.CS.JustMock.Examples
{
    public class MockSamplesHelper
    {
        private bool _someProperty;

        public bool SomeProperty
        {
            get { return _someProperty; }
            set { _someProperty = value; }
        }
        
        public void Dummy(String s)
        {
           // Intentionally do nothing
        }

        public int Add(int firstNumber, int secondNumber)
        {
            if (firstNumber > secondNumber)
            {
                Dummy("");
            }
            return firstNumber + secondNumber;
        }

        public string ReturnString(string s)
        {
            return "called";
        }
    }
}
