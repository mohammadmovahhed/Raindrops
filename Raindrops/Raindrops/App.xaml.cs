﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raindrops.Views;

namespace Raindrops
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new EasyModePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
