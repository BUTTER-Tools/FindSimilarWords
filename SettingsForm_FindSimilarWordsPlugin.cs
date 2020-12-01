using System.IO;
using System.Text;
using System.Windows.Forms;
using System;

namespace FindSimilarWordsPlugin
{
    internal partial class SettingsForm_FindSimilarWordsPlugin : Form
    {


        #region Get and Set Options

        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }
        public string SelectedEncoding { get; set; }
        public int VectorSize { get; set; }
        public int VocabSize { get; set; }
        public string[] WordList { get; set; }
        public double CosineCutoff { get; set; }
        public bool modelFileHeader { get; set; }

       #endregion



        public SettingsForm_FindSimilarWordsPlugin(string InputFile, string OutputFile, string SelectedEncodingIncoming, int VecSize, int VocSize, string[] WordList, double CosineThreshold)
        {
            InitializeComponent();

            foreach (var encoding in Encoding.GetEncodings())
            {
                EncodingDropdown.Items.Add(encoding.Name);
            }

            try
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(SelectedEncodingIncoming);
            }
            catch
            {
                EncodingDropdown.SelectedIndex = EncodingDropdown.FindStringExact(Encoding.Default.BodyName);
            }

            CosineThresholdBox.Value = (decimal)CosineThreshold;
            OutputFileTextbox.Text = OutputFile;
            VocabSize = VocSize;
            VectorSize = VecSize;
            SelectedFileTextbox.Text = InputFile;

            if (VocabSize == -1) ModelDetailsTextbox.Text = "Vocab size: unknown; Vector Size: " + VectorSize.ToString();
            else ModelDetailsTextbox.Text = "Vocab size: " + VocabSize.ToString() + "; Vector Size: " + VectorSize.ToString();
            WordListTextbox.Lines = WordList;


        }






        private void SetFolderButton_Click(object sender, System.EventArgs e)
        {

            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.ValidateNames = true;
                dialog.Title = "Please choose the model file that you would like to read";
                dialog.FileName = "Model.txt";
                dialog.Filter = "Word Embedding Model (.txt,.vec)|*.txt;*.vec";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    


                    try
                    {
                        using (var stream = File.OpenRead(dialog.FileName))
                        using (var reader = new StreamReader(stream, encoding: Encoding.GetEncoding(EncodingDropdown.SelectedItem.ToString())))
                        {

                            string[] firstLine = reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            if(firstLine.Length == 2)
                            {
                                VocabSize = int.Parse(firstLine[0]);
                                VectorSize = int.Parse(firstLine[1]);
                                ModelDetailsTextbox.Text = "Vocab size: " + firstLine[0] + "; Vector Size: " + firstLine[1];
                            }
                            else
                            {
                                VectorSize = firstLine.Length - 1;
                                VocabSize = -1;
                                ModelDetailsTextbox.Text = "Vocab size: unknown; Vector Size: " + VectorSize.ToString();
                            }

                            

                            
                            SelectedFileTextbox.Text = dialog.FileName;

                        }

                    }
                    catch
                    {
                        MessageBox.Show("There was an error while trying to read your word embedding model. It is possible that your file is not correctly formatted, or that your model file is open in another program.", "Error reading model", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }
            }


        }


        private void OKButton_Click(object sender, System.EventArgs e)
        {
            CosineCutoff = (double)CosineThresholdBox.Value;
            this.SelectedEncoding = EncodingDropdown.SelectedItem.ToString();
            this.InputFileName = SelectedFileTextbox.Text;
            WordList = WordListTextbox.Lines;
            this.DialogResult = DialogResult.OK;
            this.OutputFileName = OutputFileTextbox.Text;
        }


        private void ChooseOutputFileButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Please choose the output location for your CSV file";
                dialog.FileName = "BUTTER-CosineSimilarity.csv";
                dialog.Filter = "Comma-Separated Values (CSV) File (*.csv)|*.csv";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (File.Exists(dialog.FileName.ToString()))
                        {
                            if (DialogResult.Yes == MessageBox.Show("BUTTER is about to overwrite your selected file. Are you ABSOLUTELY sure that you want to do this? All data currently contained in the selected file will immediately be deleted if you select \"Yes\".", "Overwrite File?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            {
                                using (var myFile = File.Create(dialog.FileName.ToString())) { }
                                OutputFileTextbox.Text = dialog.FileName.ToString();
                            }
                            else
                            {
                                OutputFileTextbox.Text = "";
                            }
                        }
                        else
                        {
                            using (var myFile = File.Create(dialog.FileName.ToString())) { }
                            OutputFileTextbox.Text = dialog.FileName.ToString();
                        }



                    }
                    catch
                    {
                        MessageBox.Show("BUTTER does not appear to be able to create this output file. Do you have write permissions for this file? Is the file already open in another program?", "Cannot Create File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        OutputFileTextbox.Text = "";
                    }
                }
            }
        }



    }
}
