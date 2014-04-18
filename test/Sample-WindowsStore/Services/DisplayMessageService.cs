﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Accela.WindowsStore.Sample.Services
{
    public class DisplayMessageService : IDisplayMessageService
    {
        public async void Show(string title, string message)
        {
            MessageDialog md = new MessageDialog(message, title);
            await md.ShowAsync();
        }

        public async void Show(string message)
        {
            MessageDialog md = new MessageDialog(message);
            await md.ShowAsync();
        }
    }
}
