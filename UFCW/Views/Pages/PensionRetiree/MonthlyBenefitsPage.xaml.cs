﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UFCW.Views.Pages.Pension
{
    public partial class MonthlyBenefitsPage : ContentPage
    {
        public MonthlyBenefitsPage()
        {
            InitializeComponent();
            BindingContext = App.retiree.Monthly_Benefits;
        }
    }
}
