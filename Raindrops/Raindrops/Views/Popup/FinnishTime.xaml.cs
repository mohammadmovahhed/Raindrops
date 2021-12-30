using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinnishTime : PopupPage
    {
        public event EventHandler onRestart;
        public event EventHandler onQuit;
        public event EventHandler onMenu;
        public FinnishTime()
        {
            InitializeComponent();
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