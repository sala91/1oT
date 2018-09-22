using System;

using _1oT.Models;
using _1oT.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace _1oT.Views
{
    public sealed partial class MasterDetailDetailControl : UserControl
    {
        public SimCard MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SimCard; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SimCard), typeof(MasterDetailDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public MasterDetailDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as MasterDetailDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }

        private void saveDataLimit_Click(object sender, RoutedEventArgs e)
        {
            var dataService = new DataService();
            var setDataLimit = dataService.SetDataLimit(MasterMenuItem.iccid, MasterMenuItem.data_limit);
        }

        private void deactivate_Click(object sender, RoutedEventArgs e)
        {

            var dataService = new DataService();
            var setDataLimit = dataService.DeActivateSimCard(MasterMenuItem.iccid);
        }

        private void activate_Click(object sender, RoutedEventArgs e)
        {

            var dataService = new DataService();
            var setDataLimit = dataService.ActivateSimCard(MasterMenuItem.iccid);
        }
    }
}
