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
            FILE_AND_BASE64,
            COMPRESS_PDF,
            FILE_TO_PDF
        };

        private Color _btnBorderColor = Colors.BlueViolet;
        private Color _btnForgroundColor = Colors.White;
        private Color _btnBackgroundColor = Color.FromRgb(50, 47, 91);
        private Color _btnForgroundMouseEnter = Colors.BlueViolet;

        private menu lastActive = menu.WELCOME;


        // methods
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_merge_pdf_Click(object sender, RoutedEventArgs e)
        {
            user_control_merge_pdf.Visibility = Visibility.Visible;
            btn_merge_pdf.BorderBrush = new SolidColorBrush(_btnBorderColor);
            if (lastActive != menu.MERGE_PDF) { 
                collapse_menu_visibility(lastActive);
                lastActive = menu.MERGE_PDF;
            }

            

        }

        private void btn_extract_pages_Click(object sender, RoutedEventArgs e)
        {
            user_control_extract_pages.Visibility = Visibility.Visible;
            btn_extract_pages.BorderBrush = new SolidColorBrush(_btnBorderColor);
            if (lastActive != menu.EXTRACT_PAGES)
            {
                collapse_menu_visibility(lastActive);
                lastActive = menu.EXTRACT_PAGES;
            }
            
        }

        private void BtnFileAndBase64_Click(object sender, RoutedEventArgs e)
        {
            user_control_file_and_base64.Visibility = Visibility.Visible;
            BtnFileAndBase64.BorderBrush = new SolidColorBrush(_btnBorderColor);
            if (lastActive != menu.FILE_AND_BASE64)
            {
                collapse_menu_visibility(lastActive);
                lastActive = menu.FILE_AND_BASE64;
            }
        }

        private void BtnCompressPDF_Click(object sender, RoutedEventArgs e)
        {
            user_control_compress_pdf.Visibility = Visibility.Visible;
            BtnCompressPDF.BorderBrush = new SolidColorBrush(_btnBorderColor);
            if (lastActive != menu.COMPRESS_PDF)
            {
                collapse_menu_visibility(lastActive);
                lastActive = menu.COMPRESS_PDF;
            }
        }

        private void BtnFileToPdf_Click(object sender, RoutedEventArgs e)
        {
            user_control_file_to_pdf.Visibility = Visibility.Visible;
            BtnFileToPdf.BorderBrush = new SolidColorBrush(_btnBorderColor);
            if (lastActive != menu.FILE_TO_PDF)
            {
                collapse_menu_visibility(lastActive);
                lastActive = menu.FILE_TO_PDF;
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
                    user_control_merge_pdf.Visibility = Visibility.Collapsed;
                    btn_merge_pdf.BorderBrush = null;
                    break;
                case menu.EXTRACT_PAGES:
                    user_control_extract_pages.Visibility = Visibility.Collapsed;
                    btn_extract_pages.BorderBrush = null;
                    break;
                case menu.FILE_AND_BASE64:
                    user_control_file_and_base64.Visibility = Visibility.Collapsed;
                    BtnFileAndBase64.BorderBrush= null;
                    break;
                case menu.COMPRESS_PDF:
                    user_control_compress_pdf.Visibility = Visibility.Collapsed;
                    BtnCompressPDF.BorderBrush= null;
                    break;
                case menu.FILE_TO_PDF:
                    BtnFileToPdf.BorderBrush=null;
                    user_control_file_to_pdf.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;

            }
        }

        private void btn_merge_pdf_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_merge_pdf.Foreground = new SolidColorBrush(_btnForgroundMouseEnter);
        }

        private void btn_merge_pdf_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_merge_pdf.Background = new SolidColorBrush(_btnBackgroundColor);
            btn_merge_pdf.Foreground = new SolidColorBrush(_btnForgroundColor);

        }

        private void btn_extract_pages_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_extract_pages.Foreground = new SolidColorBrush(_btnForgroundMouseEnter);
        }


        private void btn_extract_pages_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_extract_pages.Background = new SolidColorBrush(_btnBackgroundColor);
            btn_extract_pages.Foreground = new SolidColorBrush(_btnForgroundColor);
        }

        private void BtnFileAndBase64_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnFileAndBase64.Foreground = new SolidColorBrush(_btnForgroundMouseEnter);
        }

        private void BtnFileAndBase64_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnFileAndBase64.Background = new SolidColorBrush(_btnBackgroundColor);
            BtnFileAndBase64.Foreground = new SolidColorBrush(_btnForgroundColor);
        }

        private void BtnCompressPDF_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnCompressPDF.Foreground = new SolidColorBrush(_btnForgroundMouseEnter);
        }

        private void BtnCompressPDF_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnCompressPDF.Background = new SolidColorBrush(_btnBackgroundColor);
            BtnCompressPDF.Foreground = new SolidColorBrush(_btnForgroundColor);
        }

        private void BtnFileToPdf_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnFileToPdf.Foreground = new SolidColorBrush(_btnForgroundMouseEnter);
        }

        private void BtnFileToPdf_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnFileToPdf.Background = new SolidColorBrush(_btnBackgroundColor);
            BtnFileToPdf.Foreground = new SolidColorBrush(_btnForgroundColor);
        }
    }
}
