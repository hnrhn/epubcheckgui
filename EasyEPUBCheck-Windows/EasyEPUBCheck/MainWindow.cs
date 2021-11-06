using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EasyEPUBCheck
{
    public partial class MainWindow : Form
    {
        private string _chosenEpubPath;
        private string _output;
        
        public MainWindow()
        {
            InitializeComponent();

            FormClosing += MainWindow_ClosingHandler;
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void chooseEpubButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "EPUB Files (*.epub)|*.epub",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            
            _chosenEpubPath = openFileDialog.FileName;
            chosenFileDisplay.Text = openFileDialog.FileName;
            validateButton.Enabled = true;
        }
        private void validateButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists("w3c/epubcheck.exe"))
            {
                outputTextBox.Text = $"ERROR: File {Environment.CurrentDirectory}\\w3c\\epubcheck.exe not found";
                return;
            }
            
            if (!File.Exists("w3c/epubcheck.jar"))
            {
                outputTextBox.Text = $"ERROR: File {Environment.CurrentDirectory}\\w3c\\epubcheck.jar not found";
                return;
            }
            
            outputTextBox.Text = "Validating; please wait...";
            var epubcheckProcessInfo = new ProcessStartInfo
            {
                FileName = "w3c/epubcheck.exe",
                Arguments = _chosenEpubPath,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var epubcheckProcess = Process.Start(epubcheckProcessInfo);
            if (epubcheckProcess == null)
            {
                outputTextBox.Text = "Failed to start the epubcheck service";
                return;
            }
            
            epubcheckProcess.WaitForExit();

            var errors = epubcheckProcess.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(errors))
            {
                _output = errors;
                outputTextBox.Text = errors;
                return;
            }
            
            var nonErrors = epubcheckProcess.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(nonErrors))
            {
                _output = nonErrors;
                outputTextBox.Text = nonErrors;
                return;
            }
            
            outputTextBox.Text = "No output from epubCheck";
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".txt",
                Filter = "Text Files (*.txt)|*.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, _output);   
            }
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainWindow_ClosingHandler(object sender, FormClosingEventArgs e)
        {
            var confirmationBox = MessageBox.Show("Are you sure you want to close?", "Confirm Exit", MessageBoxButtons.OKCancel);
            if (confirmationBox != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}