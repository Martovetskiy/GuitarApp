namespace MusicPlayer
{
    using MusicPlayer.ViewModel;
    using Plugin.BluetoothClassic.Abstractions;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using System.Collections.Generic;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransiverPage : ContentPage
    {
        public TransiverPage()
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
        }

        ~TransiverPage()
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
        public String c1 = "";
        public List<string> people = new List<string>();
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
                    c1 += value.ToString() + " ";
                    people.Add(value.ToString());
                    
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
        public string c2;
        private void GG_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 191;
        }

        private void GG1_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 192;


        }

        

        private void GG2_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 193;
        }

        private void GG3_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 194;
        }

        private void GG5_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 195;
        }

        private void GG6_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 196;
        }

        private void GG4_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 197;
        }

        private void GG7_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 198;
        }

        private void GG8_Clicked(object sender, EventArgs e)
        {
            DigitViewModel model = (DigitViewModel)BindingContext;
            model.Digit = 199;
        }
    }
}