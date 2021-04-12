using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sorter
{
    public partial class FileSorter : Form
    {
        public FileSorter()
        {
            InitializeComponent();
        }

        private void FileSorter_DragDrop(object sender, DragEventArgs e)
        {
            //Read file
            string[] fileData = (string[])e.Data.GetData(DataFormats.FileDrop);

            //Get Filename
            foreach (string file in fileData)
            {
                if (file != null)
                {
                    string[] sortedFile = SortFile(file);

                    if (moveSortedFile(file, sortedFile[1])) listBox1.Items.Add(sortedFile[0] + " --> " + sortedFile[1] + "  ✓");
                    else listBox1.Items.Add(sortedFile[0] + " --> Documents\\ImportantFiles\\" + sortedFile[1] + "  X  (Error occured)");


                }
                else MessageBox.Show("Invalid file type (null)");
            }
        }

        private void FileSorter_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) e.Effect = DragDropEffects.All;
        }

        private string[] SortFile(string fileWithPath)
        {
            string[] folders = new string[4];

            //Folders paths
            folders[0] = "faktury";
            folders[1] = "pdfs";
            folders[2] = "txts";
            folders[3] = "images";
            string[] values = new string[2];
            Regex r;

            values[0] = Path.GetFileName(fileWithPath);

            //Match txt
            r = new Regex(@".*\.txt");
            if (r.IsMatch(values[0])) values[1] = folders[2];

            //Match pdf
            r = new Regex(@".*\.pdf");
            if (r.IsMatch(values[0])) values[1] = folders[1];

            //Match images
            r = new Regex(@".*\.(png|jpg|jpeg|bmp|tiff|raw)");
            if (r.IsMatch(values[0])) values[1] = folders[3];

            //Match faktura
            r = new Regex(@"faktura.*");
            if (r.IsMatch(values[0])) values[1] = folders[0];

            return values;
        }

        private bool moveSortedFile(string filePath, string destPath)
        {
            bool flag = true;

            try
            {
                //System.IO.Directory.CreateDirectory(@"C:\Users\Mikoai\Documents\ImportantFiles\" + destPath);
                //System.IO.File.Move(@filePath, @"C:\Users\Mikoai\Documents\ImportantFiles\" + destPath);
            }
            catch (System.IO.IOException e)
            {
                _ = e.Message;
                flag = false;
            }

            return flag;
        }

        private void FileSorter_DragOver(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, false) == true) e.Effect = DragDropEffects.All;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
