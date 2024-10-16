using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Xml.Linq;

namespace PDFAssistant.View.UserControls
{
    /// <summary>
    /// Interaction logic for ExtractPages.xaml
    /// </summary>
    public partial class ExtractPages : UserControl
    {
        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
        private static Guid DownloadsFolderGuid = new Guid("374DE290-123F-4565-9164-39C4925E467B"); // Guid for Download folder 

        private String outputPath = "";

        private string extractPagesMessage = "Pages to extract (ex: 2 or 5-23 or 2,4-8,11-last or last)";

        public ExtractPages()
        {
            InitializeComponent();

            // initialize by default the output => Download/output.pdf
            IntPtr pathPtr;
            SHGetKnownFolderPath(DownloadsFolderGuid, 0, IntPtr.Zero, out pathPtr);
            string downloadsPath = Marshal.PtrToStringUni(pathPtr);
            Marshal.FreeCoTaskMem(pathPtr);
            String outputFilePath = downloadsPath + System.IO.Path.DirectorySeparatorChar + "output.pdf";
            output_TextBox.Text = outputFilePath;
        }

        private void btn_extract_Click(object sender, RoutedEventArgs e)
        {
            List<int> pages = givePagesToExtract(pages_box.Text);

            // check if the output path is valid
            this.outputPath = output_TextBox.Text;
            if (!this.outputPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid output filename. Please enter a .pdf file path.", "Error");
                return;
            }

            // check if the input pdf exist
            if (!File.Exists(this.pdf_extract.Text))
            {
                MessageBox.Show("Input file not exist!", "Error");
                return;
            }

            try
            {

                using (PdfReader reader = new PdfReader(this.pdf_extract.Text))
                {
                    Document document = new Document();
                    PdfCopy pdfCopyProvider = new PdfCopy(document, new FileStream(this.outputPath, FileMode.Create));
                    document.Open();

                    foreach (int pageNumber in pages)
                    {
                        PdfImportedPage importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    document.Close();

                    // Clear the pdfs in the list
                    pages_box.Text = "";
                    pdf_extract.Text = "";

                    // Show success message 
                    MessageBox.Show("Extracting pages success!", "Success");
                }

            }
            catch (Exception ex)
            {
                // show the error message 
                Console.WriteLine("Error merging PDFs: " + ex.Message);

                // Clear the pdfs in the list
                pages_box.Text = "";
                pdf_extract.Text = "";

            }

            

        }

        private void pdf_extract_Drop(object sender, DragEventArgs e)
        {
            
                String[] pdfsPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                String pdfPath = pdfsPaths[0];
                string pdfName = System.IO.Path.GetFileName(pdfPath);
                if (!pdfName.EndsWith(".pdf"))
                {
                    MessageBox.Show("\"" + pdfName + "\"" + " is not a pdf!", "Error");
                }
                else
                {
                    pdf_extract.Text = pdfName;
                }
            
            
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "PDF Files (*.pdf)|*.pdf";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                // Handle the file upload here
                string filePath = dlg.FileName;
                pdf_extract.Text = filePath;
               
            }
        }

        private List<int> givePagesToExtract(String filter)
        {
            List<int> res = new List<int>();

            try
            {
                String[] filterCommaSplit = filter.Split(',');
                foreach (string fcs in filterCommaSplit)
                {
                    string[] filterDashSplit = fcs.Split('-');
                    if (filterDashSplit.Length == 1)
                    {
                        int p = int.Parse(filterDashSplit[0]);
                        if (!res.Contains(p)) { res.Add(p); }
                    }
                    else if (filterDashSplit.Length == 2)
                    {
                        int n1 = int.Parse(filterDashSplit[0]);
                        int n2 = int.Parse(filterDashSplit[1]);
                        if (n2 < n1)
                        {
                            throw new Exception("Problem with interval value of the pages!");
                        }
                        else
                        {
                            for (int p = n1; p <= n2; p++)
                            {
                                if (!res.Contains(p)) { res.Add(p); }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Problem with interval value of the pages!");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Calculating the pages to extract: " + ex.Message, "Error");
            }



            return res;
        }

        private void pages_box_GotFocus(object sender, RoutedEventArgs e)
        {
            if (pages_box.Text == extractPagesMessage)
            {
                pages_box.Text = "";
                pages_box.Foreground = Brushes.White;
            }
        }

        private void pages_box_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pages_box.Text))
            {
                pages_box.Text = extractPagesMessage;
                pages_box.Foreground = Brushes.Gray;
            }
        }

    }
}
