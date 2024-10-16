using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PDFAssistant.View.UserControls
{
    /// <summary>
    /// Interaction logic for FileAndBase64.xaml
    /// </summary>
    public partial class FileAndBase64 : UserControl
    {
        public FileAndBase64()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "(*.*)|*.*";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                // Handle the file upload here
                string filePath = dlg.FileName;
                fileToConvert.Text = filePath;
                fileToConvert.IsReadOnly = true;

            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            // check if the input pdf exist
            if (!File.Exists(fileToConvert.Text))
            {
                MessageBox.Show("Input file not exist!", "Error");
                return;
            }
            byte[] fileBytes = File.ReadAllBytes(fileToConvert.Text); 
            string base64String = Convert.ToBase64String(fileBytes);

            // fill the text zone
            base64TextBox.Text=base64String;
            base64TextBox.IsReadOnly = false;
            base64TextBox.FontSize = 12;
            base64TextBox.HorizontalContentAlignment= HorizontalAlignment.Stretch;
            base64TextBox.VerticalContentAlignment= VerticalAlignment.Stretch;
            base64TextBox.TextWrapping = TextWrapping.Wrap;

            // visualize the copy button
            copyBtn.Visibility = Visibility.Visible;

        }

        private void copyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(base64TextBox.Text);

            copyBtn.Content = "COPIED";
            copyBtn.Foreground = new SolidColorBrush(Colors.Green);
            copyBtn.IsEnabled = false;  
        }
    }
}
