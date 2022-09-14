
using Plugin.SimpleAudioPlayer;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MusicPlayer.ViewModel;
using Plugin.BluetoothClassic.Abstractions;
namespace MusicPlayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lesson1 : ContentPage
    {
        ISimpleAudioPlayer player;
        public int butcheck = 0;
        IOrientationHandler orientationHandler = DependencyService.Get<IOrientationHandler>();
        public Lesson1()
        {
            InitializeComponent();

            DigitViewModel model = (DigitViewModel)BindingContext;
            model.PropertyChanged += Model_PropertyChanged;

            if (App.CurrentBluetoothConnection != null)
            {
                App.CurrentBluetoothConnection.OnStateChanged += CurrentBluetoothConnection_OnStateChanged;
                App.CurrentBluetoothConnection.OnRecived += CurrentBluetoothConnection_OnRecived;
                App.CurrentBluetoothConnection.OnError += CurrentBluetoothConnection_OnError;
            }

            orientationHandler.ForceLandscape();
            player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load("AmSongi.mp3");
            HintRow.Opacity = 0;
            InitControls();
        }

        ~Lesson1()
        {
            if (App.CurrentBluetoothConnection != null)
            {
                App.CurrentBluetoothConnection.OnStateChanged -= CurrentBluetoothConnection_OnStateChanged;
                App.CurrentBluetoothConnection.OnRecived -= CurrentBluetoothConnection_OnRecived;
                App.CurrentBluetoothConnection.OnError -= CurrentBluetoothConnection_OnError;
            }
        }

        private void CurrentBluetoothConnection_OnStateChanged(object sender, StateChangedEventArgs stateChangedEventArgs)
        {
            var model = (DigitViewModel)BindingContext;
            if (model != null)
            {
                model.ConnectionState = stateChangedEventArgs.ConnectionState;
            }
        }

        private void CurrentBluetoothConnection_OnRecived(object sender, Plugin.BluetoothClassic.Abstractions.RecivedEventArgs recivedEventArgs)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;

            if (model != null)
            {
                model.SetReciving();

                for (int index = 0; index < recivedEventArgs.Buffer.Length; index++)
                {
                    byte value = recivedEventArgs.Buffer.ToArray()[index];
                    model.Digit2 = value;
                }

                model.SetRecived();
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DigitViewModel.Properties.Digit.ToString())
            {
                TransmitCurrentDigit();
            }
        }

        private void CurrentBluetoothConnection_OnError(object sender, System.Threading.ThreadExceptionEventArgs errorEventArgs)
        {
            if (errorEventArgs.Exception is BluetoothDataTransferUnitException)
            {
                TransmitCurrentDigit();
            }
        }

        private void TransmitCurrentDigit()
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            if (model != null && !model.Reciving)
            {
                App.CurrentBluetoothConnection.Transmit(new Memory<byte>(new byte[] { model.Digit }));
            }
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


            btnPlay.Clicked += BtnPlayClicked;


            sliderPosition.ValueChanged += SliderPostionValueChanged;

        }

        void ActivateExp()
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 1;
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
            ActivateExp();
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
        private void NextLesson()
        {
            btnPlay.Source = "dmpink.png";
            TaskText.Text = "Молодец, а теперь аккорд ";
            AccordName.Text = "Dm";
        }

        bool correctnessGame()
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            int Flag1 = model.Digit2;
            AccordName.Text = Flag1.ToString();
            if (model.Digit2 == 49)
            {
                return true;
            }
            else
            {
                return false;
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

            await HintRow.FadeTo(1, 1000);
            if (correctnessGame())
            {
                await HintRow.FadeTo(0, 1000);
                NextLesson();
            }
            else
            {
                await DisplayAlert("Немного не так", "Попробуй сыграть еще раз, просто так же нажми на аккорд", "Хорошо, спасибо");
            }
            
        }

    }
}