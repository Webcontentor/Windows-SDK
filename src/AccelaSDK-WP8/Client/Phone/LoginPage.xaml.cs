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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

/// <summary>
/// Contains the LoginPage class which displays the Accela authentication page for the Windows Phone.
/// </summary>
namespace Accela.WindowsStoreSDK.Client
{
    /// <summary>
    /// A login page for users to do Authentification operation
    /// </summary>
    public partial class LoginPage : PhoneApplicationPage
    {
        private string responseData = "";
        private uint responseErrorDetail = 0;
        private WebAuthenticationStatus responseStatus = WebAuthenticationStatus.UserCancel;

        // We need to keep this state to make sure we do the right thing even during
        // normal phone navigation actions (such as going to start screen and back).
        private bool authenticationStarted = false;
        private bool authenticationFinished = false;

        /// <summary>
        /// Initiatlizes the page by hooking up some event handlers.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

            BackKeyPress += LoginPage_BackKeyPress;
            browserControl.Navigating += BrowserControl_Navigating;
            browserControl.NavigationFailed += BrowserControl_NavigationFailed;
        }

        /// <summary>
        /// Initiates the authentication operation by pointing the browser control
        /// to the PhoneWebAuthenticationBroker.StartUri.  If the PhoneWebAuthenticationBroker
        /// isn't currently in the middle of an authentication operation, then we immediately
        /// navigate back.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Make sure that there is an authentication operation in progress.
            // If not, we'll navigate back to the previous page.
            if (!WebAuthenticationBroker.AuthenticationInProgress)
            {
                this.NavigationService.GoBack();
            }

            if (!authenticationStarted)
            {
                authenticationStarted = true;
                authenticationFinished = false;

                // Point the browser control to the authentication start page.
                browserControl.Source = WebAuthenticationBroker.StartUri;
            }
        }

        /// <summary>
        /// Updates the PhoneWebAuthenticationBroker on the state of the authentication
        /// operation.  If we navigated back by pressing the back key, then the operation
        /// will be canceled.  If the browser control successfully completed the operation,
        /// signaled by its navigating to the PhoneWebAuthenticationBroker.EndUri, then we
        /// pass the results on.
        /// </summary>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // If there is an active authentication operation in progress and we have
            // finished, then we need to inform the authentication broker of the results.
            // We don't want to stop the operation prematurely, such as when navigating to
            // the start screen.
            if (WebAuthenticationBroker.AuthenticationInProgress && authenticationFinished)
            {
                authenticationStarted = false;
                authenticationFinished = false;

                WebAuthenticationBroker.OnAuthenticationFinished(responseData, responseStatus, responseErrorDetail);
            }
        }

        /// <summary>
        /// Handler for the page's back key events.  We use this to determine whether navigations
        /// away from this page are benign (such as going to the start screen) or actually meant
        /// to cancel the operation.
        /// </summary>
        void LoginPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            responseData = "";
            responseStatus = WebAuthenticationStatus.UserCancel;

            authenticationFinished = true;
        }

        /// <summary>
        /// Handler for the browser control's navigating event.  We use this to detect when login
        /// has completed.
        /// </summary>
        private void BrowserControl_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.Host == WebAuthenticationBroker.EndUri.Host)
            {
                responseData = e.Uri.ToString();
                responseStatus = WebAuthenticationStatus.Success;

                authenticationFinished = true;

                // Navigate back now.
                browserControl.Source = new Uri("about:blank");
                NavigationService.GoBack();
            }
        }

        /// <summary>
        /// Handler for the browser control's navigation failed event.  We use this to detect errors
        /// </summary>
        private void BrowserControl_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            WebBrowserNavigationException navEx = e.Exception as WebBrowserNavigationException;

            if (navEx != null)
            {
                // Pass along the provided error information.
                responseErrorDetail = (uint)navEx.StatusCode;
            }
            else
            {
                // No error information available.
                responseErrorDetail = 0;
            }
            responseStatus = WebAuthenticationStatus.ErrorHttp;

            authenticationFinished = true;
            e.Handled = true;

            // Navigate back now.
            browserControl.Source = new Uri("about:blank");
            NavigationService.GoBack();
        }
    }
}