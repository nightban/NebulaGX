using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace NebulaGX
{
    public partial class MainWindow : Window
    {
        string configPath = "config.txt";

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            await Browser.EnsureCoreWebView2Async();

            string theme = LoadTheme();

            if (theme == "red")
                this.Background = System.Windows.Media.Brushes.DarkRed;
            else
                this.Background = System.Windows.Media.Brushes.Black;

            Browser.Source = new Uri("https://google.com");
        }

        string LoadTheme()
        {
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, "purple");
                return "purple";
            }

            return File.ReadAllText(configPath);
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = SearchBox.Text;

                if (!text.StartsWith("http"))
                    text = "https://www.google.com/search?q=" + text;

                Browser.Source = new Uri(text);
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Browser.Source = new Uri("https://google.com");
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Browser.Reload();
        }
    }
}
