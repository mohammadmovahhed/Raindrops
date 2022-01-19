using Raindrops.ViweModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinnishResult : PopupPage
    {
        PlayerViewModel viewModel = new PlayerViewModel();
        public FinnishResult()
        {
            InitializeComponent();
            //BindingContext = viewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lst.ItemsSource = await viewModel.PlayerVm.GetAllInformationAsync();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}