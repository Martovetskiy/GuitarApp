namespace MusicPlayer.ViewModel
{
    using Plugin.BluetoothClassic.Abstractions;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class DigitViewModel : INotifyPropertyChanged
    {
        public enum Properties
        {
            Digit,
            Digit2,
            ConnectionState
        }

        public const byte DigitDefault = 0;
        private byte _digit = DigitDefault;
        private byte _digit2 = DigitDefault;
        private ConnectionState _connectionState;
        private Color _connectionStateBackgroundColor;

        public DigitViewModel()
        {
            SetRecived();
            UpdateConnectionStateBackgroundColor();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal bool Reciving { get; set; }

        public byte Digit
        {
            get
            {
                return _digit;
            }
            set
            {
                if (_digit != value)
                {
                    _digit = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Digit"));
                }
            }
        }
        public byte Digit2
        {
            get
            {
                return _digit2;
            }
            set
            {
                if (_digit2 != value)
                {
                    _digit2 = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Digit2"));
                }
            }
        }

        public ConnectionState ConnectionState
        {
            get 
            {
                return _connectionState;
            }
            set
            {
                if(_connectionState != value)
                {
                    _connectionState = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnectionState"));
                    UpdateConnectionStateBackgroundColor();
                }
            }
        }

        private void UpdateConnectionStateBackgroundColor()
        {
            switch(ConnectionState)
            {
                case ConnectionState.Created:
                case ConnectionState.Initializing:
                case ConnectionState.Connecting:
                    ConnectionStateBackgroundColor = Color.Orange;
                    break;
                case ConnectionState.Connected:
                    ConnectionStateBackgroundColor = Color.SeaGreen;
                    break;
                case ConnectionState.ErrorOccured:
                case ConnectionState.Reconnecting:
                    ConnectionStateBackgroundColor = Color.Red;
                    break;
                default:
                    ConnectionStateBackgroundColor = Color.Black;
                    break;
            }
        }

        public Color ConnectionStateBackgroundColor
        {
            get
            {
                return _connectionStateBackgroundColor;
            }
            set
            {
                if(_connectionStateBackgroundColor != value)
                {
                    _connectionStateBackgroundColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnectionStateBackgroundColor"));
                }
            }
        }

        internal void SetReciving()
        {
            Reciving = true;
        }

        internal void SetRecived()
        {
            Reciving = false;
        }
    }
}
