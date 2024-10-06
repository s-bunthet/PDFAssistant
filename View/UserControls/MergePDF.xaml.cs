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
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PDFAssistant.View.UserControls
{

    /// <summary>
    /// Interaction logic for MergePDF.xaml
    /// </summary>
    public partial class MergePDF : UserControl
    {
        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
        private static Guid DownloadsFolderGuid = new Guid("374DE290-123F-4565-9164-39C4925E467B"); // Guid for Download folder 

        // properties
        private List<String> pdfsToMerge = new List<string>();

        private String outputFilePath = "";


        // methods
        public MergePDF()
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

        private void pdfs_listBox_Drop(object sender, DragEventArgs e)
        {
            String[] pdfsPaths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string pdfPath in pdfsPaths)
            {
                string pdfName = System.IO.Path.GetFileName(pdfPath);
                if (!pdfName.EndsWith(".pdf"))
                {
                    MessageBox.Show("\"" + pdfName + "\"" + " is not a pdf!", "Error");
                }
                else
                {
                    if (pdfs_listBox.Items.Contains(pdfName))
                    {
                        MessageBox.Show("The file " + pdfName + " already exist! ", "Error");
                    }
                    else
                    {
                        pdfs_listBox.Items.Add(pdfName);
                        this.pdfsToMerge.Add(pdfPath);

                        if (pdfs_listBox.Items.Count >= 2)
                        {
                            btn_merge.Visibility = Visibility.Visible;
                        }
                    }
                }

            }
        }

        private void btn_merge_Click(object sender, RoutedEventArgs e)
        {
            // check if the output path is valid
            this.outputFilePath = output_TextBox.Text;
            if (!outputFilePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid filename. Please enter a .pdf file path.", "Error");
            }


            // Create a Document instance
            Document document = new Document();

            try
            {
                // Create a PdfCopy instance and bind it to the output file
                using (FileStream stream = new FileStream(this.outputFilePath, FileMode.Create))
                {
                    PdfCopy pdfCopy = new PdfCopy(document, stream);
                    document.Open();

                    // Loop through the input PDF files and add their pages to the output
                    foreach (string pdfFile in this.pdfsToMerge)
                    {
                        PdfReader reader = new PdfReader(pdfFile);
                        int totalPages = reader.NumberOfPages;

                        for (int page = 1; page <= totalPages; page++)
                        {
                            // Add each page of the current PDF to the output
                            PdfImportedPage importedPage = pdfCopy.GetImportedPage(reader, page);
                            pdfCopy.AddPage(importedPage);
                        }

                        reader.Close();
                    }

                    document.Close();
                }

                // Clear the pdfs in the list
                this.pdfsToMerge.Clear();
                pdfs_listBox.Items.Clear();

            }
            catch (Exception ex)
            {
                // show the error message 
                MessageBox.Show("\"Error merging PDFs: \" + ex.Message", "Error");
                document.Close();
            }

            // Show the success message
            MessageBox.Show("\"Error merging PDFs: \" + ex.Message", "Success");

        }


        // when we press delete key on any pdf, we remove that file from the list
        private void pdfs_listBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Delete)
            {
                this.pdfsToMerge.RemoveAt(pdfs_listBox.SelectedIndex);
                pdfs_listBox.Items.RemoveAt(pdfs_listBox.SelectedIndex);
            }
        }
    }
}
