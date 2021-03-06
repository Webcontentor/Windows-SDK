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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;


namespace Accela.WindowsStoreSDK
{
    internal class AgencyNativeViewModel : BindableBase
    {

        private NativeUserInfo _userInfo;
        private PopControl _pc;
        private AgencyNativeControl _uc;
        private AccelaSDK _am;
        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
        }

        public NativeUserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; SetProperty(ref _userInfo, value); }
        }

        public AgencyNativeViewModel(AccelaSDK am)
        {
            if (am == null)
                throw new ArgumentNullException("am");
            this._am = am;
            this._loginCommand = new DelegateCommand(Login);
            this._pc = new PopControl();
            this._uc = new AgencyNativeControl();
            this._userInfo = new NativeUserInfo();

            _uc.DataContext = this;
            _pc.Content = _uc;
        }

        public void OpenDialog()
        {
            _pc.ShowPop();
        }

        private async void Login()
        {
            _uc.IsRunning = true;
            MessageDialog md = null;
            try
            {
#if NATIVE
                //await this._am.Authorize(_userInfo.UserName,
                //                         _userInfo.Password,
                //                         false,
                //                         _userInfo.Agency,
                //                         (AccelaEnvironment)Enum.Parse(typeof(AccelaEnvironment), _userInfo.Enviroment));
#endif
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    md = new MessageDialog(ex.InnerException.Message);
                else
                    md = new MessageDialog(ex.Message);
            }
            finally
            {
                _uc.IsRunning = false;
            }
            if (md != null)
                await md.ShowAsync();
            else
                _pc.HidePop();
        }

    }
}
