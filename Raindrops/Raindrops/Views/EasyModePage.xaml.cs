using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lottie.Forms;
using Rg.Plugins.Popup.Services;
using Raindrops.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Raindrops.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EasyModePage : ContentPage
    {
        List<Grid> Anser = new List<Grid>();

        
        int seconds = 60;
        int scoreValue = 300;
        int scoreMin = 5;
        int TimeSetGrid = 3, ValueSetGrid = 5;
        int anserNum = 5;
        int trueAnser = 0;

        public EasyModePage()
        {
            InitializeComponent();
            Timer();
        }

        private void Timer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1),  () =>
            {
                seconds--;
                if (seconds == -1) 
                {
                    
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
                    return false;
                }

                return true;
            });
        }

        public Grid SetImageGrid()
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

            var LstOprator = new List<string> { "+", "-", "*", "/" };
            int index = rnd.Next(LstOprator.Count() - 4);

            if (index == 0)
            {
                int text1 = rnd.Next(0, 50);
                int text2 = rnd.Next(1, 50);
                int Multiply = text1 + text2;
                Fnumber.Text = text1.ToString();
                Snumber.Text = text2.ToString();
                imageGrid.AutomationId = Multiply.ToString();
            }
            else if (index == 1)
            {
                int text1 = rnd.Next(20, 120);
                int text2 = rnd.Next(0, 20);
                int Multiply = text1 - text2;
                Fnumber.Text = text1.ToString();
                Snumber.Text = text2.ToString();
                imageGrid.AutomationId = Multiply.ToString();
            }
            else if (index == 2)
            {
                int text1 = rnd.Next(0, 11);
                int text2 = rnd.Next(0, 11);
                int Multiply = text1 * text2;
                Fnumber.Text = text1.ToString();
                Snumber.Text = text2.ToString();
                imageGrid.AutomationId = Multiply.ToString();
            }
            else if (index == 3)
            {
                int text1 = rnd.Next(9, 99);
                int text2 = rnd.Next(1, 9);
                int Multiply = text1 / text2;
                Fnumber.Text = text1.ToString();
                Snumber.Text = text2.ToString();
                imageGrid.AutomationId = Multiply.ToString();
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
                ()=>
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

        private async void OnBtnEnter(object sender, EventArgs e)
        {
            var gridlst = Anser.ToList();
            foreach (var item in gridlst)
            {
                if (item.AutomationId == ResultLable.Text)
                {
                    scoreValue += 20;
                    Scorelbl.Text = scoreValue.ToString();
                    ResultLable.Text = string.Empty;
                    WinAnimation(item);
                    PlaySound.Play("1.wav");
                    await Task.Delay(1000);
                    Anser.Remove(item);
                    grid.Children.Remove(item);
                    trueAnser++;
                    if (trueAnser >= anserNum)
                    {
                        trueAnser -= 2;
                        anserNum++;
                        await Task.Delay(200);
                        CreateImageGrid();
                    }
                }
                //else
                //{
                //    scoreValue -= 30;
                //    Scorelbl.Text = scoreValue.ToString();
                //    ResultLable.Text = string.Empty;
                //}
                //break;
            }
            ResultLable.Text = string.Empty;
        }
       
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
            var img = SetImageGrid();
            Anser.Add(img);
            //javab.Add(img.AutomationId);
        }

        private void OnBtnClear(object sender, EventArgs e)
        {
            ResultLable.Text = string.Empty;
        }

        private void OnBtnClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            ResultLable.Text += button.Text;
        }

        private void OnImageButtonClicked(object sender, EventArgs e)
        {
            Task t = new Task(async () =>
            {
                var popup = new Popup.OnBackButtonPopup();
                //popup.onClose += RunReactionTime;
                //popup.onRestart += RunRestartGame;
                popup.onQuit += RunQuitGame;
                await PopupNavigation.Instance.PushAsync(popup);
            });
            t.RunSynchronously();
        }

        //private async void RunRestartGame(object s, EventArgs e)
        //{
            //await Navigation.PopAsync(false);
            //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            //await Navigation.PushAsync(new EasyModePage());
        //}

        private void RunQuitGame(object s, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}