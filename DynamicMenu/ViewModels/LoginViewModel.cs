using System.Collections.ObjectModel;
using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DynamicMenu.EventArg;
using DynamicMenu.Models;
using DynamicMenu.Services;

namespace DynamicMenu.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public static event EventHandler<LoginEventArgs> SuccessfulLogin;
        private readonly UserService service;
        public LoginViewModel(UserService service)
        {
            this.service = service;
        }

        //default value can be set directly to observable property field here and it will work correctly

        //not used actually atm
        [ObservableProperty]
        private bool reloadingUsers;

        [ObservableProperty]
        private ObservableCollection<User> availableUsers;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool unknownUsernameTextVisible;

        //warning: setting IsRefreshing property (bound to RefreshView) to true
        //will enable the loading animation
        //but also will execute the command set in RefreshView (ReloadUsers method in this case)
        //https://github.com/dotnet/maui/issues/6456#issuecomment-1500983408
        [ObservableProperty]
        private bool isRefreshing;

        private void ResetForm()
        {
            AvailableUsers = new ObservableCollection<User>(new List<User>());
            Name = string.Empty;
            UnknownUsernameTextVisible = false;
        }

        [RelayCommand]
        async void OnAppearing()
        {
            ResetForm();
            await ReloadUsers();
        }

        [RelayCommand]
        void OnNameChanged()
        {
            UnknownUsernameTextVisible = false;
        }

        [RelayCommand]
        async Task Login()
        {
            var inputText = Name;
            var user = await service.GetUserByName(inputText);
            if (user is null)
            {
                UnknownUsernameTextVisible = true;
                return;
            }

            SuccessfulLogin?.Invoke(this, new LoginEventArgs(user));
        }

        //prevent duplicate calls when IsRefreshing is set to true to be able to show loading animation
        //https://github.com/dotnet/maui/issues/6456#issuecomment-1500983408
        private bool alreadyReloadingUsers;

        [RelayCommand]
        async Task ReloadUsers()
        {
            if (alreadyReloadingUsers)
            {
                return;
            }
            alreadyReloadingUsers = true;

            //RefreshView loading animation
            if (IsRefreshing != true)
            {
                //enables loading animation on RefreshView
                //but also triggers this function by coincidence via registered Command on RefreshView
                IsRefreshing = true; 
            }

            ReloadingUsers = true;
            var items = await service.GetAll();
            AvailableUsers = new ObservableCollection<User>(items);
            ReloadingUsers = false;

            IsRefreshing = false; //RefreshView loading animation

            alreadyReloadingUsers = false;
        }
    }
}
