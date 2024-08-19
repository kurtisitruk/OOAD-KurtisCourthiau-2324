using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCostumer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Your custom startup logic (if any)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during application startup: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
