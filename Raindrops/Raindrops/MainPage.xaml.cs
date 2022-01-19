using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raindrops.Views;
using Xamarin.Forms;

namespace Raindrops
{
    public partial class MainPage : ContentPage
    {
        //for start animation
        Frame Frm = new Frame { CornerRadius = 40, BackgroundColor = Color.DodgerBlue, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center, Margin = 20 };
        Label LblTimer = new Label { TextColor = Color.White, FontFamily = "IranSensWebbold", FontSize = 20 };
        ViweModels.Base F = new ViweModels.Base();
        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartButton(object sender, EventArgs e)
        {
            await F.RunIsBusyTaskAsync(() => Play());
            (sender as Button).IsVisible = false;

            await Navigation.PushAsync(new EasyModePage());
        }

        public async Task Play()
        {
            Frm.Content = LblTimer;
            mainStack.Children.Add(Frm);
            Animation ParentAnimation = new Animation();
            Animation ParentAnimation2 = new Animation();
            Animation animate = new Animation(v => Frm.Scale = v, 1.2, 0.5, Easing.SpringIn);
            Animation animate1 = new Animation(v => Frm.Scale = v, 0.5, 1.2, Easing.SpringOut);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text = 3.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text = 2.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text = 1.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            Frm.IsVisible = false;
            mainStack.Children.Remove(Frm);
        }
    }
}
