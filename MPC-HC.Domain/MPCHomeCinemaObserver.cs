using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPC_HC.Domain
{
    public class MPCHomeCinemaObserver
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event ExceptionEventHandler Error;

        public TimeSpan UpdateFrequency { get; set; }

        private Info _oldInfo;
        private Info _newInfo;
        private readonly IMPCHomeCinema _mpcHomeCinema;
        
        private Timer _observationTimer;


        public MPCHomeCinemaObserver(IMPCHomeCinema client)
        {
            _mpcHomeCinema = client;
            UpdateFrequency = TimeSpan.FromMilliseconds(500);
        }

        public async Task Start()
        {
            _oldInfo = await _mpcHomeCinema.GetInfo();
            // Do not start a new Timer if one is already running
            if (_observationTimer != null)
            {
                return;
            }

            _observationTimer = new Timer(Callback, null, new TimeSpan(0), UpdateFrequency);
        }

        private async void Callback(object state)
        {
            try
            {
                _newInfo = await _mpcHomeCinema.GetInfo();

                if (_newInfo.State != _oldInfo.State)
                {
                    OnPropertyChanged(Property.State);
                }

                if (_newInfo.Position != _oldInfo.Position)
                {
                    OnPropertyChanged(Property.Possition);
                }

                if (_newInfo.FileName != _oldInfo.FileName)
                {
                    OnPropertyChanged(Property.File);
                }

                _oldInfo = _newInfo;
            }
            catch (Exception e)
            {
                Error?.Invoke(this, new ExceptionEventArgs(e));
            }
        }

        public void Stop()
        {
            _observationTimer.Dispose();
            _observationTimer = null;
        }
        
        private void OnPropertyChanged(Property propertyType)
        {
            var args = new PropertyChangedEventArgs(_oldInfo, _newInfo, propertyType);
            PropertyChanged?.Invoke(this, args);
        }
    }
}