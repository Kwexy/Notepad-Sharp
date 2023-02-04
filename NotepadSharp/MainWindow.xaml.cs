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
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
        }

        public void Button_Open(object sender, EventArgs e) {
            OpenFileDialog openFileWindow = new OpenFileDialog();
            openFileWindow.ShowDialog();
            if (openFileWindow.FileName != "") {
                TextBox.Text = "";
                foreach (string line in FileManager.ReadFile(openFileWindow.FileName)) {
                    TextBox.Text += line + "\n";
                }
            }
            //TextBlock_FileName.Text = openFileWindow.FileName;
            UpdateFileNameText(openFileWindow.FileName);
        }

        public void Button_Save(object sender, EventArgs e) {
            if (FileManager.WorkingPath != null) {
                FileManager.WriteFile(FileManager.WorkingPath, TextBox.Text);
                //TextBlock_FileName.Text = FileManager.WorkingPath;
                UpdateFileNameText(FileManager.WorkingPath);
                TextBlock_Saved.Foreground = Brushes.Green;
                TextBlock_Saved.Text = "Saved!";
            } else {
                Button_SaveAs(sender, e);
            }
        }

        public void Button_SaveAs(object sender, EventArgs e) {
            SaveFileDialog saveFileWindow = new SaveFileDialog();
            saveFileWindow.ShowDialog();
            FileManager.WriteFile(saveFileWindow.FileName, TextBox.Text);
            //TextBlock_FileName.Text = FileManager.WorkingPath;
            UpdateFileNameText(FileManager.WorkingPath);
            TextBlock_Saved.Foreground = Brushes.Green;
            TextBlock_Saved.Text = "Saved!";
        }

        public void Button_Exit(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        public void UpdateFileNameText(string filePath) {
            for (int i = filePath.Length-1; i >= 0; i--) {
                if (filePath[i] == '\\') {
                    TextBlock_FileName.Text = filePath.Substring(i+1);
                    return;
                }
            }
        }

        private void TextBox_ZoomIn(object sender, RoutedEventArgs e) {
            if (TextBox.FontSize < 30) {
                TextBox.FontSize++;
            }
        }

        private void TextBox_ZoomOut(object sender, RoutedEventArgs e) {
            if (TextBox.FontSize > 1) {
                TextBox.FontSize--;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            double percAmount = ScaleSlider.Value * 10;
            ScaleText.Text = ((int)percAmount).ToString();
            if (ScaleSlider.Value > 0) {
                TextBox.FontSize = Math.Ceiling(percAmount);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            TextBlock_Saved.Foreground = Brushes.Red;
            TextBlock_Saved.Text = "Unsaved";
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e) {
            int lines = 0;
            int columns = 0;

            for (int i = 0; i < TextBox.CaretIndex; i++) {
                columns++;
                if (TextBox.Text[i] == '\n') {
                    lines++;
                    columns = 0;
                }
            }
            TextBlock_LineNumber.Text = lines.ToString();
            TextBlock_ColumnNumber.Text = columns.ToString();
        }
    }
}
