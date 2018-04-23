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

        // Method is Bound to "ReceivedInfo" Event of receiver. Every time a udp package is received, the UI will be updated
        private async void OnReceive(object sender, ReceiveEventArgs e)
        {
            Visibility = true;
            string info = e.Info.ToString();
            InfoText = info;
            await Task.Delay(3000);
            this.Visibility = false;
        }
    }
}
