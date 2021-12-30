using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasyPage : ContentPage
    {

        //Grid Grd;
        //int Counter = 5;
        Frame Frm = new Frame { CornerRadius = 40, BackgroundColor = Color.DodgerBlue, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
        //Image[,] Img;
        Label LblTimer = new Label { TextColor = Color.White, FontFamily = "IranSensWebbold", FontSize = 20 };
        ViweModels.Base F = new ViweModels.Base();

        public EasyPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await F.RunIsBusyTaskAsync(async () => await Play());
            (sender as Button).IsVisible = false;
        }
        public async Task Play()
        {
            Frm.Content = LblTimer;
            Grid.SetRow(Frm, 1);
            GridContent.Children.Add(Frm);
            Animation ParentAnimation = new Animation();
            Animation ParentAnimation2 = new Animation();
            Animation animate = new Animation(v => Frm.Scale = v, 1.2, 0.5, Easing.SpringIn);
            Animation animate1 = new Animation(v => Frm.Scale = v, 0.5, 1.2, Easing.SpringOut);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text=3.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            LblTimer.Text = 2.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            LblTimer.Text = 1.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            Frm.IsVisible = false;
            
        }
    }
}