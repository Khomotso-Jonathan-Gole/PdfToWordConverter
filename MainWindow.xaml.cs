/*
 * PDF to Word Converter
 * Developed by: Khomotso Jonathan Gole
 * Contact: 0783672051 / 0795886717
 * Email: khomotsojonathan2@gmail.com
 * LinkedIn: https://www.linkedin.com/in/khomotso-jonathan-gole
 * GitHub: https://github.com/Khomotso-Jonathan-Gole
 * 
 * Copyright (c) 2025 Khomotso Jonathan Gole. All rights reserved.
 * Licensed under the MIT License.
 */

using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace PdfToWordConverter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Select PDF File"
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                System.Windows.MessageBox.Show("Please select a PDF file first.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(txtFilePath.Text))
            {
                System.Windows.MessageBox.Show("Selected file does not exist.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Show converting message
            btnConvert.Content = "Converting...";
            btnConvert.IsEnabled = false;

            // Perform conversion in background to keep UI responsive
            System.Threading.Tasks.Task.Run(() =>
            {
                var result = PythonService.ConvertPdfToWord(txtFilePath.Text);

                // Update UI on main thread
                Dispatcher.Invoke(() =>
                {
                    btnConvert.Content = "Convert to Word";
                    btnConvert.IsEnabled = true;

                    if (result.Success)
                    {
                        System.Windows.MessageBox.Show(
                            $"Conversion successful!\n\nOutput file: {result.OutputPath}\nFile size: {result.FileSize} bytes",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(
                            $"Conversion failed!\n\nError: {result.Error}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                });
            });
        }

        // ▼▼▼ ADD THESE CLICK HANDLERS FOR SOCIAL LINKS ▼▼▼
        private void LinkedIn_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://www.linkedin.com/in/khomotso-jonathan-gole",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error opening LinkedIn: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GitHub_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://github.com/Khomotso-Jonathan-Gole",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error opening GitHub: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

