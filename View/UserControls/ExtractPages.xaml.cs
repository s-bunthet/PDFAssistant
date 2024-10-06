using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Xml.Linq;

namespace PDFAssistant.View.UserControls
{
    /// <summary>
    /// Interaction logic for ExtractPages.xaml
    /// </summary>
    public partial class ExtractPages : UserControl
    {
        private String pdfToExtract;

        public ExtractPages()
        {
            InitializeComponent();
        }

        private void btn_extract_Click(object sender, RoutedEventArgs e)
        {
            List<int> pages = new List<int> { Int32.Parse(pages_box.Text) };
            
            String outputPdfPath = "C:/Users/say_bunthet/Downloads/output/extract.pdf";
            try
            {
                
                using (PdfReader reader = new PdfReader(this.pdfToExtract))
                {
                    Document document = new Document();
                    PdfCopy pdfCopyProvider = new PdfCopy(document, new FileStream(outputPdfPath, FileMode.Create));
                    document.Open();

                    foreach (int pageNumber in pages)
                    {
                        PdfImportedPage importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    document.Close();
                }

                // Clear the pdfs in the list
                this.pdfToExtract = "";
                pages_box.Text = "";
                pdf_extract.Items.Clear();

            }
            catch (Exception ex)
            {
                // show the error message 
                Console.WriteLine("Error merging PDFs: " + ex.Message);
        
            }

        }

        private void pdf_extract_Drop(object sender, DragEventArgs e)
        {
            if (pdf_extract.Items.Count < 1)
            {
                String[] pdfsPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
                String pdfPath = pdfsPaths[0];
                string pdfName = System.IO.Path.GetFileName(pdfPath);
                if (!pdfName.EndsWith(".pdf"))
                {
                    MessageBox.Show("\"" + pdfName + "\"" + " is not a pdf!", "Erreur");
                }
                else
                {
                    pdf_extract.Items.Add(pdfName);
                    this.pdfToExtract = pdfPath;
                }
            }
            
        }
    }
}
