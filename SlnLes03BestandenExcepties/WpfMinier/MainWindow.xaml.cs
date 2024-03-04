using Microsoft.Win32;
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


namespace WpfMinier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string selectedFolderPath;
        private List<string> selectedFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            SelecteerBtn.Click += SelecteerBtn_Click;
            ModifyBtn.Click += ModifyBtn_Click;
            ModifyAlsBtn.Click += ModifyAlsBtn_Click;
        }

        private void SelecteerBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ValidateNames = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!string.IsNullOrEmpty(selectedFolderPath))
            {
                dialog.InitialDirectory = selectedFolderPath;
            }

            if (dialog.ShowDialog() == true)
            {
                // Geselecteerde map instellen en bestanden weergeven
                selectedFolderPath = Path.GetDirectoryName(dialog.FileName);
                FileTbx.Text = selectedFolderPath;
                PopulateFileList(selectedFolderPath);
            }

        }

        private void PopulateFileList(string folderPath)
        {
            FileLbx.Items.Clear();
            try
            {
                var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                     .Where(s => s.EndsWith(".css") || s.EndsWith(".html") || s.EndsWith(".js"));
                foreach (var file in files)
                {
                    FileLbx.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het lezen van de bestanden: {ex.Message}");
            }
        }

        private string GetMinifiedFilePath(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            return Path.Combine(directory, $"{fileNameWithoutExtension}.min{extension}");
        }

        private string Minify(string content)
        {

            return content;
        }
    

        private void FileLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFiles.Clear();
            foreach (var selectedItem in FileLbx.SelectedItems)
            {
                selectedFiles.Add(selectedItem.ToString());
            }
            ModifyBtn.IsEnabled = selectedFiles.Any();
            ModifyAlsBtn.IsEnabled = selectedFiles.Count == 1;
        }

        private void ModifyAlsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFiles.Count != 1)
            {
                MessageBox.Show("Selecteer één bestand om te minifyen.");
                return;
            }

            var dialog = new SaveFileDialog();
            dialog.FileName = "Selecteer opslaglocatie";
            dialog.DefaultExt = ".min.css";
            dialog.Filter = "Minified CSS Files|*.min.css|Minified HTML Files|*.min.html|Minified JavaScript Files|*.min.js";

            if (dialog.ShowDialog() == true)
            {
                string minifiedFilePath = dialog.FileName;
                string content = File.ReadAllText(selectedFiles[0]);
                string minifiedContent = Minify(content);
                File.WriteAllText(minifiedFilePath, minifiedContent);
                FileLbx.Items.Add(minifiedFilePath);
            }
        }

        private void ModifyBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var file in selectedFiles)
            {
                string minifiedFilePath = GetMinifiedFilePath(file);
                string content = File.ReadAllText(file);
                string minifiedContent = Minify(content);
                File.WriteAllText(minifiedFilePath, minifiedContent);
                FileLbx.Items.Add(minifiedFilePath);
            }
        }
    }
}
