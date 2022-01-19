using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottie.Forms;
using Rg.Plugins.Popup.Services;
using Raindrops.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raindrops.Views.Popup;
using Raindrops.ViweModels;

namespace Raindrops.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasyModePage : ContentPage
    {
        //for start animation
        Frame Frm = new Frame { CornerRadius = 40, BackgroundColor = Color.DodgerBlue, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center, Margin = 40 };
        Label LblTimer = new Label { TextColor = Color.White, FontFamily = "IranSensWebbold", FontSize = 20 };
        Base F = new Base();

        private List<Grid> Anser = new List<Grid>();

        private int seconds = 60; //timer
        private int scoreValue = 300; //Player initial score
        private int scoreMin = 5; //Every second, the score decreases by 5 points
        private int TimeSetGrid = 3, ValueSetGrid = 5; //Grid means rain-drop

        public EasyModePage()
        {
            InitializeComponent();
        }

        //The user has 60 seconds and during this time he encounters several problems
        private void Timer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
           {
               seconds--;
               if (seconds == -1)
               {
                   Task.Run(async () =>
                   {
                       Insert();
                       await Task.Delay(100);
                       await PopupNavigation.Instance.PushAsync(new FinnishResult());
                   });
                   return false;
               }
               TimeSetGrid++;
               scoreValue -= scoreMin;
               if (TimeSetGrid >= ValueSetGrid)
               {
                   CreateImageGrid();
                   TimeSetGrid = 0;
               }
               if (seconds > -1 && scoreValue > -1)
               {
                   Timerlbl.Text = seconds.ToString();
                   Scorelbl.Text = scoreValue.ToString();
               }
               else
               {
                   Task.Run(async () =>
                   {
                       Insert();
                       await Task.Delay(100);
                       await PopupNavigation.Instance.PushAsync(new FinnishResult());
                   });
                   return false;
               }

               return true;
           });
        }
        private async void Insert()
        {
            PlayerViewModel pvm=new PlayerViewModel();
            Player p = new Player();
            p.Score = scoreValue;
            await pvm.PlayerVm.InsertScore(p);
        }

        //In this function, raindrops are made
        private Grid SetImageGrid()
        {
            Grid imageGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };
            Random rnd = new Random();
            int i = rnd.Next(0, 5);
            Grid.SetColumn(imageGrid, i);

            Label Fnumber = new Label
            {
                TextColor = Color.FromHex("#f3ffbd"),
                FontFamily = "IranSensWebbold",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            Label Snumber = new Label
            {
                TextColor = Color.FromHex("#f3ffbd"),
                FontFamily = "IranSensWebbold",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };

            int text1, text2, Multiply;
            var LstOprator = new List<string> { "+", "-", "*", "/" };
            int index = rnd.Next(LstOprator.Count() - 1);
            switch (index)
            {
                case 0:
                    text1 = rnd.Next(0, 100);
                    text2 = rnd.Next(1, 50);
                    Multiply = text1 + text2;
                    Fnumber.Text = text1.ToString();
                    Snumber.Text = text2.ToString();
                    imageGrid.AutomationId = Multiply.ToString();
                    break;
                case 1:
                    text1 = rnd.Next(50, 120);
                    text2 = rnd.Next(0, 49);
                    Multiply = text1 - text2;
                    Fnumber.Text = text1.ToString();
                    Snumber.Text = text2.ToString();
                    imageGrid.AutomationId = Multiply.ToString();
                    break;
                case 2:
                    text1 = rnd.Next(1, 11);
                    text2 = rnd.Next(0, 11);
                    Multiply = text1 * text2;
                    Fnumber.Text = text1.ToString();
                    Snumber.Text = text2.ToString();
                    imageGrid.AutomationId = Multiply.ToString();
                    break;
                case 3:
                    text1 = rnd.Next(9, 99);
                    text2 = rnd.Next(1, 9);
                    Multiply = text1 / text2;
                    Fnumber.Text = text1.ToString();
                    Snumber.Text = text2.ToString();
                    imageGrid.AutomationId = Multiply.ToString();
                    break;
            }

            Label Oprator = new Label
            {
                Text = LstOprator[index],
                FontFamily = "IranSensWebbold",
                TextColor = Color.FromHex("#d00000"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            imageGrid.Children.Add(new Image
            {
                Source = "raindrop.webp",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill,
                TranslationY = -2
            });
            imageGrid.Children.Add(Oprator);
            imageGrid.Children.Add(Fnumber);
            imageGrid.Children.Add(Snumber);

            int r = 10;
            Animation anime = new Animation(v => imageGrid.TranslationY = v, 0, grid.Height - (imageGrid.Height + 80), Easing.Linear,
                () =>
                {
                    List<Grid> an = Anser.Where(x => x.AutomationId == imageGrid.AutomationId).ToList();
                    if (an.Count > 0)
                    {
                        grid.Children.Remove(imageGrid);
                        Anser.Remove(imageGrid);
                        PlaySound.Play("2.wav");
                    }
                });
            Animation anime1 = new Animation
            {
                {0, 1, anime }
            };

            r += 10;
            anime1.Commit(imageGrid, "Linear", 16, 10000);

            grid.Children.Add(imageGrid);
            return imageGrid;
        }

        //This function checks whether the answer is correct or not
        private async void OnBtnEnter(object sender, EventArgs e)
        {
            List<Grid> gridlst = Anser.ToList();
            foreach (Grid item in gridlst)
            {
                if (item.AutomationId == ResultLable.Text)
                {
                    scoreValue += 20;
                    Scorelbl.Text = scoreValue.ToString();
                    ResultLable.Text = string.Empty;
                    WinAnimation(item);
                    PlaySound.Play("1.wav");
                    await Task.Delay(500);
                    Anser.Remove(item);
                    grid.Children.Remove(item);
                    TimeSetGrid++;
                    if (TimeSetGrid >= ValueSetGrid)
                    {
                        TimeSetGrid -= 3;
                        ValueSetGrid++;
                    }
                }
            }
            ResultLable.Text = string.Empty;
        }

        //Show animation at correct answer time
        private void WinAnimation(Grid grid)
        {
            grid.CancelAnimations();
            grid.Children.Clear();
            AnimationView atest = new AnimationView
            {
                Animation = "likelove.json",
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            grid.Children.Add(atest);
        }

        private void CreateImageGrid()
        {
            Grid img = SetImageGrid();
            Anser.Add(img);
        }

        //Clear input (Entry.text)
        private void OnBtnClear(object sender, EventArgs e)
        {
            ResultLable.Text = string.Empty;
        }
        //Display the numbers entered in the input
        private void OnBtnClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ResultLable.Text += button.Text;
        }

        //when push this button = game start
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await F.RunIsBusyTaskAsync(() => Play());

            startGame.IsVisible = startGame.IsEnabled = false;
            grid.IsEnabled = grid.IsVisible = keypadGrid.IsEnabled = keypadGrid.IsVisible = true;
            Timer();
        }
        //animation start game (number)
        public async Task Play()
        {
            Frm.Content = LblTimer;
            mainGrid.Children.Add(Frm);
            Animation ParentAnimation = new Animation();
            Animation animate = new Animation(v => Frm.Scale = v, 1.2, 0.5, Easing.SpringIn);
            Animation animate1 = new Animation(v => Frm.Scale = v, 0.5, 1.2, Easing.SpringOut);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text = 3.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            ParentAnimation.Add(0, 0.7, animate);
            ParentAnimation.Add(0.7, 1, animate1);
            LblTimer.Text = 2.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            ParentAnimation.Add(0, 0.5, animate);
            ParentAnimation.Add(0.5, 1, animate1);
            LblTimer.Text = 1.ToString();
            ParentAnimation.Commit(Frm, "counterAnim", 16, 1000);
            await Task.Delay(1000);
            Frm.IsVisible = false;
            mainGrid.Children.Remove(Frm);
        }

        //show popup quit, resume, menu
        //popUp Menu functions
        private void OnImageButtonClicked(object sender, EventArgs e)
        {
            Task t = new Task(async () =>
            {
                Popup.OnBackButtonPopup popup = new Popup.OnBackButtonPopup();
                popup.OnQuit += RunQuitGame;
                await PopupNavigation.Instance.PushAsync(popup);
            });
            t.RunSynchronously();
        }
        private void RunQuitGame(object s, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}