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

using Microsoft.Win32;

using NotepadSharp.FileHandlers;

namespace NotepadSharp {

    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
        }

        public void New(object sender, EventArgs e) {
            string messageBoxText = "Would you like to save before opening a new file?";
            string caption = "Save First?";
            MessageBoxButton mb = MessageBoxButton.YesNo;
            MessageBoxImage img = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, mb, img);
        }

        public void Button_Open(object sender, EventArgs e) {
            OpenFileDialog openFileWindow = new OpenFileDialog();
            openFileWindow.ShowDialog();
            KwextBox.Text = "";
            foreach (string line in FileManager.ReadFile(openFileWindow.FileName)) {
                KwextBox.Text += line + "\n";
            }
        }

        public void Button_Save(object sender, EventArgs e) { 
            FileManager.WriteFile(FileManager.WorkingPath, KwextBox.Text);
        }

        public void Button_SaveAs(object sender, EventArgs e) {
            SaveFileDialog saveFileWindow = new SaveFileDialog();
            saveFileWindow.ShowDialog();
            FileManager.WriteFile(saveFileWindow.FileName, KwextBox.Text);
        }

        public void Button_Exit(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        private void KwextBox_ZoomIn(object sender, RoutedEventArgs e) {
            if (KwextBox.FontSize < 30) {
                KwextBox.FontSize++;
            }
        }

        private void KwextBox_ZoomOut(object sender, RoutedEventArgs e) {
            if (KwextBox.FontSize > 1) {
                KwextBox.FontSize--;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            double percAmount = ScaleSlider.Value * 10;
            ScaleText.Text = ((int)percAmount).ToString() + "%";
            if (ScaleSlider.Value > 0) {
                KwextBox.FontSize = ScaleSlider.Value * 10;
            }
        }

        private void KwextBox_TextChanged(object sender, TextChangedEventArgs e) {
            //TextBlock_LineNumber.Text = KwextBox.
        }
    }
}
