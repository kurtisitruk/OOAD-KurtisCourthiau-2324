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
using System.IO;

namespace WpfMinifier
{
    public partial class MainWindow : Window
    {
        public string FolderPath { get; set; }
        private List<string> FilePaths { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FilePaths = new List<string>();
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.SelectedPath = !string.IsNullOrEmpty(FolderPath) ? FolderPath : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = dialog.SelectedPath;
                LoadFiles();
            }
        }

        private void LoadFiles()
        {
            try
            {
                var files = Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
                                    .Where(s => s.EndsWith(".css") || s.EndsWith(".html") || s.EndsWith(".js"));
                FilePaths.Clear();
                FilePaths.AddRange(files);
                lstFiles.ItemsSource = FilePaths.Select(Path.GetFileName);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Minify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Minify selected file
                string selectedFile = FilePaths[lstFiles.SelectedIndex];
                // Call minify method
                // Example: MinifyCSS(selectedFile);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void MinifyAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Allow user to choose location to save minified file
                var saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string savePath = saveDialog.FileName;
                    // Minify selected file and save it to the chosen location
                    string selectedFile = FilePaths[lstFiles.SelectedIndex];
                    // Call minify method and save to savePath
                    // Example: MinifyCSS(selectedFile, savePath);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
