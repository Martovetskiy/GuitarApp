
using Plugin.SimpleAudioPlayer;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaintItBlackMusicPage : ContentPage
    {
        ISimpleAudioPlayer player;
        public int BtnPlayCheck = 2;
        IOrientationHandler orientationHandler = DependencyService.Get<IOrientationHandler>();
        public PaintItBlackMusicPage()
        {
            InitializeComponent();
            orientationHandler.ForceLandscape();
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load("PaintItBlack.mp3");
            HintRow.Opacity = 0;
            InitControls();
            SecondArrow.FadeTo(0.3,1);
            ThirdArrow.FadeTo(0.3, 1);
        }

        private double width = 0;
        private double height = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            orientationHandler.ForceLandscape();
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
            }


        }
        void InitControls()
        {
            
            sliderPosition.ValueChanged += SliderPostionValueChanged;
        }


        private void SliderPostionValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sliderPosition.Value != player.Duration)
                player.Seek(sliderPosition.Value);
        }


        int GG = 0;
        bool PauseFlag = true;
        private void BtnPlayClicked(object sender, EventArgs e)
        {
            if (PauseFlag)
            {
                btnPlay.Source = "playButton";
                player.Play();
                
                sliderPosition.Maximum = player.Duration;
                sliderPosition.IsEnabled = player.CanSeek;

                Device.StartTimer(TimeSpan.FromSeconds(0.1), UpdatePosition);
                Device.StartTimer(TimeSpan.FromSeconds(2.0), PaintItBlackMusicActive);
                PauseFlag = false;
            }
            
            else
            {
                GG +=1;
                btnPlay.Source = "pause";
                player.Pause();
                PauseFlag = true;
            }
            
        }
        

        
        bool PaintItBlackMusicActive()
        {   
            if (sliderPosition.Value > 11.10 && (sliderPosition.Value + 4.4 < sliderPosition.Maximum ) && (GG==0))
            {
                PaintItBlackMusic();
                return true;
            }
            else if ((sliderPosition.Value + 4.4 > sliderPosition.Maximum) || (GG != 0))
            {
                GG = 0;
                return false;
            }
            else
            { 
                return true;
            }
            
        }
        private async void PaintItBlackMusic()
        {
            
             await FirstArrow.FadeTo(0.3, 250);
             await SecondArrow.FadeTo(1, 250);
             await SecondArrow.FadeTo(0.3, 250);
             await ThirdArrow.FadeTo(1, 250);
             await ThirdArrow.FadeTo(0.3, 250);
             await FirstArrow.FadeTo(1, 250);
            
        }
        
        bool UpdatePosition()
        {

            sliderPosition.ValueChanged -= SliderPostionValueChanged;
            sliderPosition.Value = player.CurrentPosition;
            sliderPosition.ValueChanged += SliderPostionValueChanged;
            double Stri = sliderPosition.Value;
            AccordName.Text = Stri.ToString();
            return player.IsPlaying;
        }
        

        bool VolumeFlag = true;
        private void MusicMute_Clicked(object sender, EventArgs e)
        {
            if (VolumeFlag)
            {
                player.Volume = 0;
                VolumeFlag = false;
            }
            else
            {
                player.Volume = 1;
                VolumeFlag = true;
            }
        }
    }
}
