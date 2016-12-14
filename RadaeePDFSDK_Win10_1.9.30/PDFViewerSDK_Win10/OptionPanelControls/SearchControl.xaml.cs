using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace PDFViewerSDK_Win10.OptionPanelControls
{
    public delegate void OnButtonTappedHandler(int btnCode, string searchKey, bool matchCase, bool matchWholeWord);

    public sealed partial class SearchControl : UserControl
    {
        public event OnButtonTappedHandler OnButtonTapped;
        public SearchControl()
        {
            this.InitializeComponent();
        }

        private void BtnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (searchTextBox.Text.Length == 0)
            {
                searchCancelBtn.IsEnabled = false;
                OnButtonTapped(-1, searchTextBox.Text, match_case_check_box.IsChecked.Value, whole_world_check_box.IsChecked.Value);
                return;
            }
            Button button = sender as Button;
            switch (button.Name)
            {
                case "searchPrevBtn":
                    searchCancelBtn.IsEnabled = true;
                    match_case_check_box.IsEnabled = false;
                    whole_world_check_box.IsEnabled = false;
                    OnButtonTapped(0, searchTextBox.Text, match_case_check_box.IsChecked.Value, whole_world_check_box.IsChecked.Value);
                    break;
                case "searchNextBtn":
                    searchCancelBtn.IsEnabled = true;
                    match_case_check_box.IsEnabled = false;
                    whole_world_check_box.IsEnabled = false;
                    OnButtonTapped(1, searchTextBox.Text, match_case_check_box.IsChecked.Value, whole_world_check_box.IsChecked.Value);
                    break;
                case "searchCancelBtn":
                    searchCancelBtn.IsEnabled = false;
                    match_case_check_box.IsEnabled = true;
                    whole_world_check_box.IsEnabled = true;
                    OnButtonTapped(-1, searchTextBox.Text, match_case_check_box.IsChecked.Value, whole_world_check_box.IsChecked.Value);
                    break;
            }
        }
    }
}
