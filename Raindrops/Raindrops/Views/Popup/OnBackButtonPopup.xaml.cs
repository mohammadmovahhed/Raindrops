using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBackButtonPopup : PopupPage
    {
        public event EventHandler OnClose;
        public event EventHandler OnQuit;
        public OnBackButtonPopup()
        {
            InitializeComponent();
        }

        private async void ResumeButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            OnClose?.Invoke(this, e);
        }

        private async void QuitButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            OnQuit?.Invoke(this, e);
        }
    }
}