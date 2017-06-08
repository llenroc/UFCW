﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using UFCW.Services.Models.User;
using UFCW.ViewModels;
using Xamarin.Forms;

namespace UFCW.Views.Login
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginVm;
        public LoginPage()
        {
            InitializeComponent();
            loginVm = new LoginViewModel();
            BindingContext = loginVm;
		}

        async Task LoginClicked()
		{
			if(!String.IsNullOrEmpty(loginVm.Email) && !String.IsNullOrEmpty(loginVm.Password))
            {
                //Indicator.Start();
				loginVm.ShowError = false;
                loginVm.IsBusy = true;
				//WaitingIndicator.Start();
				LoginResponse response = await loginVm.LogiUser(loginVm.Email, loginVm.Password);
               // WaitingIndicator.Stop();
				loginVm.IsBusy = false;
				if (String.IsNullOrEmpty(response.ErrorText) && String.IsNullOrEmpty(response.ErrorDetails))
				{
					loginVm.user = response.Profile;
					Debug.WriteLine("Login Success" + loginVm.user.FIRST_NAME);
				}
				else
				{
					await Navigation.PushModalAsync(new RootPage());
					//await this.DisplayAlert("Login Failed!", "\n" + response.ErrorDetails, "Try Again!");
				}
            }
            else
            {
				loginVm.ShowError = true;
            }
        }

		//public static bool IsPageInNavigationStack<TPage>(INavigation navigation) where TPage : Page
		//{
		//	if (navigation.NavigationStack.Count > 1)
		//	{
		//		var last = navigation.NavigationStack[navigation.NavigationStack.Count - 2];

		//		if (last is TPage)
		//		{
		//			return true;
		//		}
		//	}
		//	return false;
		//}
    }
}