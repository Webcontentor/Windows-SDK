﻿#if WINDOWS_PHONE
using Accela.WindowsPhone8.Sample.Services;
#else
using Accela.WindowsStore.Sample.Services;
#endif
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE
namespace Accela.WindowsPhone8.Sample.ViewModels
#else
namespace Accela.WindowsStore.Sample.ViewModels
#endif
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private RelayCommand _agencyAppCommand;
        public RelayCommand AgencyAppCommand
        {
            get
            {
                return _agencyAppCommand ??
                    (_agencyAppCommand = new RelayCommand(ExcuteAgencyAppCommand));
            }
        }

        private void ExcuteAgencyAppCommand()
        {
            _navigationService.Navigate<AgencyViewModel>();
        }


        private RelayCommand _citizenAppCommand;
        public RelayCommand CitizenAppCommand
        {
            get
            {
                return _citizenAppCommand ??
                    (_citizenAppCommand = new RelayCommand(ExcuteCitizenAppCommand));
            }
        }

        private void ExcuteCitizenAppCommand()
        {
            _navigationService.Navigate<CitizenViewModel>();
        }

    }
}
