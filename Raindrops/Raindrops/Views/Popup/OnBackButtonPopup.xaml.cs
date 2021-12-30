using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBackButtonPopup : PopupPage
    {
        public event EventHandler onClose;
        public event EventHandler onRestart;
        public event EventHandler onQuit;
        public event EventHandler onMenu;
        public OnBackButtonPopup()
        {
            InitializeComponent();
        }

        private async void ResumeButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            onClose?.Invoke(this, e);
        }

        private async void RestartButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            onRestart?.Invoke(this, e);
        }

        private async void MenuButton_Clicke(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            await Navigation.PopToRootAsync();
            await Navigation.PushAsync(new MainPage());
        }

        private async void QuitButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync(true);
            onQuit?.Invoke(this, e);
        }
    }
}