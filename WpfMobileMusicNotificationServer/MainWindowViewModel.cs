using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace WpfMobileMusicNotificationServer
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Receiver _receiver;

        private bool _visibility;
        private string _infoText;

        public string InfoText
        {
            get { return _infoText; }
            set { SetProperty(ref _infoText, value, () => InfoText); }
        }
        public bool Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value, () => Visibility); }
        }

        public MainWindowViewModel()
        {
            Visibility = false;
            _receiver = new Receiver();
            _receiver.ReceiveInfoAsync();
            _receiver.ReceivedInfo += OnReceive;
        }

        private async void OnReceive(object sender, ReceiveEventArgs e)
        {
            Visibility = true;
            string info = e.Info.ToString();
            InfoText = info;
            await Task.Delay(1500);
            this.Visibility = false;
        }
    }
}
