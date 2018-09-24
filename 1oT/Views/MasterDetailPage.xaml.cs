using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using _1oT.Models;
using _1oT.Services;
using MetroLog;
using Microsoft.Toolkit.Uwp.UI.Controls;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace _1oT.Views
{
    public sealed partial class MasterDetailPage : Page, INotifyPropertyChanged
    {
        private SimCard _selected;
        private ILogger logger = LogManagerFactory.DefaultLogManager.GetLogger<DataService>();

        public SimCard Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<SimCard> SimCards { get; private set; } = new ObservableCollection<SimCard>();

        public MasterDetailPage()
        {
            InitializeComponent();
            Loaded += MasterDetailPage_Loaded;
        }

        private void MasterDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            SimCards.Clear();
            var dataService = new DataService();
            var getSims = dataService.GetSimCards();
            try
            {
                foreach (var item in getSims.sims)
                {
                    SimCards.Add(item);
                }

                if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
                {
                    Selected = SimCards.First();
                }
            }
            catch (Exception)
            {
                logger.Error("No sims to present... Maybe api keys missing/expired or not set up yet");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
