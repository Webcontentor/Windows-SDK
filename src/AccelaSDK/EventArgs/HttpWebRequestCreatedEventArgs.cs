﻿/** 
  * Copyright 2014 Accela, Inc. 
  * 
  * You are hereby granted a non-exclusive, worldwide, royalty-free license to 
  * use, copy, modify, and distribute this software in source code or binary 
  * form for use in connection with the web services and APIs provided by 
  * Accela. 
  * 
  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
  * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
  * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
  * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
  * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
  * DEALINGS IN THE SOFTWARE. 
  * 
  */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accela.WindowsStoreSDK
{   /// <summary>
    /// Model class to set/get userToken and  <see cref="HttpWebRequestWrapper"/>
    /// </summary>
    class HttpWebRequestCreatedEventArgs : EventArgs
    {
        private readonly object _userToken;
        private readonly HttpWebRequestWrapper _httpWebRequestWrapper;

        public HttpWebRequestCreatedEventArgs(object userToken, HttpWebRequestWrapper httpWebRequestWrapper)
        {
            _userToken = userToken;
            _httpWebRequestWrapper = httpWebRequestWrapper;
        }

        public HttpWebRequestWrapper HttpWebRequest
        {
            get { return _httpWebRequestWrapper; }
        }

        public object UserState
        {
            get { return _userToken; }
        }
    }
}
