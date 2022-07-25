﻿
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
    public partial class Lesson1 : ContentPage
    {
        ISimpleAudioPlayer player;
        public int butcheck = 0;

        public Lesson1()
        {
            InitializeComponent();
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load("AmSongi.mp3");
            HintRow.Opacity = 0;
            InitControls();
        }

        void InitControls()
        {


            btnPlay.Clicked += BtnPlayClicked;


            sliderPosition.ValueChanged += SliderPostionValueChanged;

        }


        private void SliderPostionValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sliderPosition.Value != player.Duration)
                player.Seek(sliderPosition.Value);
        }

        

        private async void BtnPlayClicked(object sender, EventArgs e)
        {
            btnPlay1.RelScaleTo(-0.1, 150);
            await btnPlay.RelScaleTo(-0.1, 150);
            btnPlay1.RelScaleTo(0.1, 150);
            await btnPlay.RelScaleTo(0.1, 150);
            if (butcheck == 0)
            {
                StateHintText();
                butcheck += 1;
            }
            
            player.Play();

            sliderPosition.Maximum = player.Duration;
            sliderPosition.IsEnabled = player.CanSeek;

            Device.StartTimer(TimeSpan.FromSeconds(0.1), UpdatePosition);
            Device.StartTimer(TimeSpan.FromSeconds(0.1), CheckStateHintRow);


        }

        bool UpdatePosition()
        {

            sliderPosition.ValueChanged -= SliderPostionValueChanged;
            sliderPosition.Value = player.CurrentPosition;
            sliderPosition.ValueChanged += SliderPostionValueChanged;
            
            return player.IsPlaying;
        }
        bool CheckStateHintRow()
        {
            if (sliderPosition.Value >= (sliderPosition.Maximum - 0.1))
            {
                StateHintRow();
                return false;
            }
            else
            {
                return true;
            }
        }
        private async void StateHintText()
        {
            await hintText.FadeTo(0, 500);
            hintText.Text = "Зажми подсвеченные лады на гитаре";
            await Task.Delay(200);
            hintText.FadeTo(1, 500);
        }
        private async void StateHintRow()
        {
            HintRow.FadeTo(1, 1000);
            await Task.Delay(10000);
            HintRow.FadeTo(0, 1000);
        }

    }
}