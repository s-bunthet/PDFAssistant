using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDFAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // properties
        private enum menu
        {
            WELCOME,
            MERGE_PDF, 
            EXTRACT_PAGES,
        };

        private menu lastActive = menu.WELCOME;


        // methods
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_merge_pdf_Click(object sender, RoutedEventArgs e)
        {
            user_control_merge_pdf.Visibility = Visibility.Visible;
            if (lastActive != menu.MERGE_PDF) { 
                collapse_menu_visibility(lastActive);
                lastActive = menu.MERGE_PDF;
            }
            
            
            //btn_merge_pdf.Background = new SolidColorBrush(Colors.Red);
            //btn_merge_pdf.IsEnabled = true;

        }

        private void btn_extract_pages_Click(object sender, RoutedEventArgs e)
        {
            user_control_extract_pages.Visibility = Visibility.Visible;
            if (lastActive != menu.EXTRACT_PAGES)
            {
                collapse_menu_visibility(lastActive);
                lastActive = menu.EXTRACT_PAGES;
            }
            
        }

        private void collapse_menu_visibility(menu last_active)
        {
            switch (last_active)
            {
                case menu.WELCOME:
                    user_control_welcome.Visibility = Visibility.Collapsed;
                    break;
                case menu.MERGE_PDF:
                    user_control_merge_pdf.Visibility=Visibility.Collapsed;
                    break;
                case menu.EXTRACT_PAGES:
                    user_control_extract_pages.Visibility=Visibility.Collapsed;  
                    break;
                default:
                    break;

            }
        }
    }
}
